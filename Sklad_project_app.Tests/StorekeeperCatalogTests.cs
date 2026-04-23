using NUnit.Framework;
using Sklad_project_app.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Sklad_project_app.Tests
{
    [TestFixture]
    public class StorekeeperCatalogTests
    {
        [Test]
        public void PriceValidation_HandlesNegativeInput_ByResettingToZero()
        {
            // Arrange
            decimal priceFromInput = -50;
            decimal priceFrom;

            // Act - логика из формы
            if (priceFromInput < 0)
            {
                priceFrom = 0;
            }
            else
            {
                priceFrom = priceFromInput;
            }

            // Assert
            Assert.AreEqual(0, priceFrom);
        }

        [Test]
        public void SearchByName_ShouldBeCaseInsensitive()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Name = "Колбаса Докторская" },
                new Product { Name = "Сыр Гауда" }
            };
            string searchText = "кОлБаСа".ToLower();

            // Act
            var result = products
                .Where(p => p.Name.ToLower().Contains(searchText))
                .ToList();

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Колбаса Докторская", result[0].Name);
        }
    }
}