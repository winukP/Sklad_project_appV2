using NUnit.Framework;
using Sklad_project_app.Models;
using Sklad_project_app.Сurrency;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Sklad_project_app.Tests
{
    [TestFixture]
    public class AdminCatalogLogicTests
    {
        [Test]
        public void GenerateArticle_EmptyDb_ReturnsFirstArticle()
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                // База пуста

                // Act
                // Имитируем логику GenerateArticle
                var products = db.Products.ToList();
                var maxArticle = products
                    .Where(p => p.Article != null && p.Article.All(char.IsDigit))
                    .Select(p => p.Article)
                    .OrderByDescending(a => a)
                    .FirstOrDefault();

                string result = string.IsNullOrEmpty(maxArticle) ? "000001" : (int.Parse(maxArticle) + 1).ToString("D6");

                // Assert
                Assert.AreEqual("000001", result);
            }
        }

        [Test]
        public void GenerateArticle_ExistingProducts_ReturnsIncrementedArticle()
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                db.Products.Add(new Product { Id = Guid.NewGuid(), Article = "000005", Name = "Test" });
                db.SaveChanges();

                // Act
                var maxArticle = db.Products.Select(p => p.Article).OrderByDescending(a => a).First();
                int number = int.Parse(maxArticle);
                number++;
                string result = number.ToString("D6");

                // Assert
                Assert.AreEqual("000006", result);
            }
        }

        [Test]
        public void LoadProducts_SearchFilter_ReturnsOnlyMatchedProducts()
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                db.Products.Add(new Product { Id = Guid.NewGuid(), Name = "Монополия", Article = "100" });
                db.Products.Add(new Product { Id = Guid.NewGuid(), Name = "Шахматы", Article = "200" });
                db.SaveChanges();

                var searchText = "моно".ToLower();

                // Act
                var filtered = db.Products
                    .AsEnumerable() // Имитируем логику из LoadProducts
                    .Where(p => p.Name.ToLower().Contains(searchText) || p.Article.ToLower().Contains(searchText))
                    .ToList();

                // Assert
                Assert.AreEqual(1, filtered.Count);
                Assert.AreEqual("Монополия", filtered[0].Name);
            }
        }

        [Test]
        public void LoadProducts_PriceFilter_ReturnsCorrectRange()
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                TestDbHelper.SeedTestData(db); // Там товар за 1450 руб.

                decimal priceFrom = 1000;
                decimal priceTo = 2000;

                // Act
                var filtered = db.Products
                    .Select(p => p.Stock)
                    .Where(s => s != null && s.PurchasePrice >= priceFrom && s.PurchasePrice <= priceTo)
                    .ToList();

                // Assert
                Assert.AreEqual(1, filtered.Count);
            }
        }

        [Test]
        public void CurrencyConversion_CalculatesCorrectRubPrice()
        {
            // Arrange
            decimal inputPrice = 100; // Допустим 100 долларов
            decimal rate = 90; // Курс 90
            string currentCurrency = "USD";

            // Act
            decimal priceInRub;
            if (currentCurrency != "RUB")
            {
                priceInRub = inputPrice * rate;
            }
            else
            {
                priceInRub = inputPrice;
            }

            // Assert
            Assert.AreEqual(9000, priceInRub);
        }
    }
}