using NUnit.Framework;
using Sklad_project_app.Сurrency;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sklad_project_app.Tests
{
    [TestFixture]
    public class CurrencyLogicTests
    {
        [Test]
        public void CalculateRate_FromRubToBase_ReturnsCorrectValue()
        {
            // Arrange
            // Предположим, API говорит: 1 RUB = 0.011 USD
            double apiValue = 0.011;

            // Act
            // Логика из LoadExchangeRates: _allRates[rate.Key] = 1 / (decimal)rate.Value;
            decimal result = 1 / (decimal)apiValue;

            // Assert
            // 1 / 0.011 ≈ 90.90
            Assert.AreEqual(90.909m, Math.Round(result, 3));
        }

        [Test]
        public void FilterRange_SwapPrices_IfFromIsGreaterThanTo()
        {
            // Arrange
            decimal priceFrom = 100;
            decimal priceTo = 50;

            // Act
            // Логика из LoadCurrencies
            if (priceFrom > priceTo)
            {
                (priceFrom, priceTo) = (priceTo, priceFrom);
            }

            // Assert
            Assert.AreEqual(50, priceFrom);
            Assert.AreEqual(100, priceTo);
        }

        [Test]
        public void SearchFilter_IsCaseInsensitive()
        {
            // Arrange
            var rates = new Dictionary<string, decimal>
            {
                { "USD", 85.0m },
                { "EUR", 95.0m },
                { "GBP", 110.0m }
            };
            string search = "us";

            // Act
            var filtered = rates
                .Where(r => r.Key.ToLower().Contains(search.ToLower()))
                .ToList();

            // Assert
            Assert.AreEqual(1, filtered.Count);
            Assert.AreEqual("USD", filtered[0].Key);
        }

        [Test]
        public void FilterByPrice_ReturnsOnlyCurrenciesInRange()
        {
            // Arrange
            var rates = new Dictionary<string, decimal>
            {
                { "USD", 85.0m },  // В диапазоне
                { "EUR", 95.0m },  // В диапазоне
                { "KZT", 0.2m }    // Вне диапазона
            };
            decimal min = 50;
            decimal max = 100;

            // Act
            var result = rates.Where(r => r.Value >= min && r.Value <= max).ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsFalse(result.Any(r => r.Key == "KZT"));
        }

        [Test]
        public void CurrencySelection_UpdatesGlobalHelper()
        {
            // Arrange
            string selectedCurrency = "EUR";

            // Act
            CurrencyHelp.SetCurrency(selectedCurrency);
            string current = CurrencyHelp.GetCurrentCurrency();

            // Assert
            Assert.AreEqual("EUR", current);
        }
    }
}