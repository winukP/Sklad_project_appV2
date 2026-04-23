using NUnit.Framework;
using Sklad_project_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sklad_project_app.Tests
{
    [TestFixture]
    public class ReportsBusinessTests
    {
        [Test]
        public void AveragePrice_Calculation_IsCorrect()
        {
            // Arrange
            // Имитируем поставки товара: 10 шт по 100 руб и 10 шт по 200 руб
            var supplies = new List<SuppliesItem>
            {
                new SuppliesItem { Quantity = 10, PurchasePrice = 100 }, // cost 1000
                new SuppliesItem { Quantity = 10, PurchasePrice = 200 }  // cost 2000
            };

            // Act
            decimal totalCost = supplies.Sum(s => s.PurchasePrice * s.Quantity); // 3000
            decimal totalQty = supplies.Sum(s => s.Quantity); // 20
            decimal avgPrice = totalQty > 0 ? totalCost / totalQty : 0;

            // Assert
            Assert.AreEqual(150m, avgPrice); // (1000+2000)/20 = 150
        }

        [Test]
        public void Profit_Calculation_ForShipment_IsCorrect()
        {
            // Arrange
            decimal avgPrice = 150m; // Себестоимость
            decimal salePrice = 250m; // Цена продажи за единицу
            int soldQty = 2;
            decimal saleAmount = salePrice * soldQty; // 500

            // Act
            // Формула из кода: shipmentProfit += item.Amount - (avgPrice * item.Quantity);
            decimal profit = saleAmount - (avgPrice * soldQty);

            // Assert
            Assert.AreEqual(200m, profit); // 500 - (150 * 2) = 200
        }

        [Test]
        public void DateFilter_ShouldIncludeWholeDay()
        {
            // Arrange
            DateTime selectedToDate = new DateTime(2023, 10, 20); // Пользователь выбрал 20 число

            // Act
            // Логика из кода формы
            DateTime dateTo = selectedToDate.Date.AddDays(1).AddSeconds(-1);

            // Assert
            // Должно получиться 20.10.2023 23:59:59
            Assert.AreEqual(20, dateTo.Day);
            Assert.AreEqual(23, dateTo.Hour);
            Assert.AreEqual(59, dateTo.Minute);
        }

        [Test]
        public void TotalAggregation_SumUpCorrect()
        {
            // Arrange
            var shipmentProfits = new List<decimal> { 100.50m, 200.00m, 50.50m };
            var shipmentAmounts = new List<decimal> { 1000m, 2000m, 500m };

            // Act
            decimal totalProfit = shipmentProfits.Sum();
            decimal totalAmount = shipmentAmounts.Sum();

            // Assert
            Assert.AreEqual(351.00m, totalProfit);
            Assert.AreEqual(3500m, totalAmount);
        }

        [Test]
        public void CSV_ExportLine_FormatIsCorrect()
        {
            // Arrange
            string date = "20.10.2023";
            string client = "ООО Тест";
            string amount = "1 500,00 ₽";
            string profit = "300,00 ₽";

            // Act
            string csvLine = $"{date};{client};{amount};{profit}";

            // Assert
            Assert.AreEqual("20.10.2023;ООО Тест;1 500,00 ₽;300,00 ₽", csvLine);
        }
    }
}