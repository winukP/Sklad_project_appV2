using NUnit.Framework;
using Sklad_project_app.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Sklad_project_app.Tests
{
    [TestFixture]
    public class ShipmentHistoryTests
    {
        [Test]
        public void ShipmentTotalAmount_CalculatesCorrectSum()
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                var shipmentId = Guid.NewGuid();
                db.Shipments.Add(new Shipment { Id = shipmentId, ShipmentDate = DateTime.Now });

                // Добавляем позиции с разной суммой
                db.ShipmentItems.Add(new ShipmentItem { Id = Guid.NewGuid(), ShipmentId = shipmentId, Amount = 100.50m });
                db.ShipmentItems.Add(new ShipmentItem { Id = Guid.NewGuid(), ShipmentId = shipmentId, Amount = 200.00m });
                db.ShipmentItems.Add(new ShipmentItem { Id = Guid.NewGuid(), ShipmentId = shipmentId, Amount = 50.25m });

                db.SaveChanges();

                // Act
                // Логика из метода LoadHistory
                decimal totalAmount = db.ShipmentItems
                    .Where(i => i.ShipmentId == shipmentId)
                    .Sum(i => i.Amount);

                // Assert
                Assert.AreEqual(350.75m, totalAmount);
            }
        }

        [Test]
        public void HistoryOrder_ShouldBeByDate()
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                var oldDate = new DateTime(2023, 1, 1);
                var newDate = new DateTime(2024, 1, 1);

                db.Shipments.Add(new Shipment { Id = Guid.NewGuid(), ShipmentDate = newDate });
                db.Shipments.Add(new Shipment { Id = Guid.NewGuid(), ShipmentDate = oldDate });
                db.SaveChanges();

                // Act
                // Логика OrderBy из LoadHistory
                var shipments = db.Shipments.OrderBy(s => s.ShipmentDate).ToList();

                // Assert
                Assert.AreEqual(oldDate, shipments[0].ShipmentDate);
                Assert.AreEqual(newDate, shipments[1].ShipmentDate);
            }
        }
    }
}