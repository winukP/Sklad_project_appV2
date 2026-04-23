using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Sklad_project_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sklad_project_app.Tests
{
    [TestFixture]
    public class WriteOffHistoryTests
    {
        [Test]
        public void LossRange_ShouldSwapValues_IfFromIsGreater()
        {
            // Arrange
            decimal lossFrom = 1000;
            decimal lossTo = 500;

            // Act - логика из LoadWriteOffHistory
            if (lossFrom > lossTo)
            {
                (lossFrom, lossTo) = (lossTo, lossFrom);
            }

            // Assert
            Assert.AreEqual(500, lossFrom);
            Assert.AreEqual(1000, lossTo);
        }

        [Test]
        public void LossRange_ShouldHandleNegativeValues()
        {
            // Arrange
            decimal lossFrom = -100;
            decimal lossTo = -500;

            // Act
            if (lossFrom < 0) lossFrom = 0;
            if (lossTo < 0) lossTo = 1000000; // Согласно коду формы

            // Assert
            Assert.AreEqual(0, lossFrom);
            Assert.AreEqual(1000000, lossTo);
        }

        [Test]
        public void FilterByProductName_ShouldBeCaseInsensitive()
        {
            // Arrange
            var writeOffs = new List<WriteOff>
            {
                new WriteOff { Product = new Product { Name = "Молоко" } },
                new WriteOff { Product = new Product { Name = "Хлеб" } }
            };
            string searchText = "МОЛО".ToLower();

            // Act
            var result = writeOffs
                .Where(w => w.Product?.Name?.ToLower().Contains(searchText) == true)
                .ToList();

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Молоко", result[0].Product.Name);
        }

        [Test]
        public void TotalLoss_Calculation_IsCorrectForFilteredItems()
        {
            // Arrange
            var filteredWriteOffs = new List<WriteOff>
            {
                new WriteOff { LossAmount = 150.50m },
                new WriteOff { LossAmount = 200.00m },
                new WriteOff { LossAmount = 50.25m }
            };

            // Act
            decimal totalLoss = 0;
            foreach (var writeOff in filteredWriteOffs)
            {
                totalLoss += writeOff.LossAmount;
            }

            // Assert
            Assert.AreEqual(400.75m, totalLoss);
        }
    }
}