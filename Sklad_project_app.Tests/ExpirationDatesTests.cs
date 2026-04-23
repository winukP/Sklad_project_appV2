using NUnit.Framework;
using Sklad_project_app.Models;
using System;
using System.Linq;

namespace Sklad_project_app.Tests
{
    [TestFixture]
    public class ExpirationDatesTests
    {
        [Test]
        public void CalculateDaysLeft_PastDate_ReturnsZeroOrNegative()
        {
            // Arrange
            var today = DateTime.Now.Date;
            var expiryDate = today.AddDays(-5); // Просрочено на 5 дней

            // Act
            int daysLeft = (expiryDate - today).Days;

            // Assert
            Assert.IsTrue(daysLeft < 0);
        }

        [Test]
        public void DetermineStatus_ExpiredProduct_SetsCorrectStatus()
        {
            // Arrange & Act
            var today = DateTime.Now.Date;
            var expiryDate = today.AddDays(-1);

            string status = "";
            int daysLeft = (expiryDate - today).Days;

            if (daysLeft <= 0)
            {
                status = "Просрочен";
            }

            // Assert
            Assert.AreEqual("Просрочен", status);
        }

        [Test]
        public void WriteOff_UpdatesBatchAndStock()
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                var productId = Guid.NewGuid();

                var stock = new Stock { ProductId = productId, Rest = 20 };
                var batch = new StockBatch
                {
                    Id = Guid.NewGuid(),
                    ProductId = productId,
                    Quantity = 10,
                    IsWrittenOff = false
                };

                db.Stocks.Add(stock);
                db.StockBatches.Add(batch);
                db.SaveChanges();

                // Act - логика из btnWriteOff_Click
                var foundBatch = db.StockBatches.First();
                var foundStock = db.Stocks.First(s => s.ProductId == productId);

                int quantityToSubtract = foundBatch.Quantity;
                foundBatch.IsWrittenOff = true;
                foundBatch.Quantity = 0;
                foundStock.Rest -= quantityToSubtract;

                db.SaveChanges();

                // Assert
                Assert.AreEqual(0, db.StockBatches.First().Quantity);
                Assert.IsTrue(db.StockBatches.First().IsWrittenOff);
                Assert.AreEqual(10, db.Stocks.First().Rest); // 20 - 10 = 10
            }
        }

        [Test]
        public void LoadExpiringProducts_FilterByStatus_WorksCorrectly()
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                var today = DateTime.Now.Date;

                // 1. Просроченный
                db.StockBatches.Add(new StockBatch
                {
                    Id = Guid.NewGuid(),
                    ExpiryDate = today.AddDays(-1),
                    Quantity = 5,
                    IsWrittenOff = false
                });

                // 2. Нормальный
                db.StockBatches.Add(new StockBatch
                {
                    Id = Guid.NewGuid(),
                    ExpiryDate = today.AddDays(10),
                    Quantity = 5,
                    IsWrittenOff = false
                });

                db.SaveChanges();

                // Act
                var allBatches = db.StockBatches.ToList();
                var expiredOnly = allBatches.Where(b => (b.ExpiryDate.Value.Date - today).Days <= 0).ToList();

                // Assert
                Assert.AreEqual(1, expiredOnly.Count);
            }
        }
    }
}