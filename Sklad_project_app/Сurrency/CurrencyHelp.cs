using System;
using System.Collections.Generic;
using System.Text;

namespace Sklad_project_app.Сurrency
{
    public static class CurrencyHelp
    {
        private static string _currentCurrency = "RUB";
        private static Dictionary<string, decimal> _allRates = new Dictionary<string, decimal>();

        public static void SetCurrency(string currency)
        {
            _currentCurrency = currency;
        }

        public static string GetCurrentCurrency()
        {
            return _currentCurrency;
        }

        public static void SetRates(Dictionary<string, decimal> rates)
        {
            _allRates = rates;
        }

        public static decimal Convert(decimal amountInRub)
        {
            if (_currentCurrency == "RUB") return amountInRub;

            decimal rate = _allRates.GetValueOrDefault(_currentCurrency, 1);
            return rate > 0 ? amountInRub / rate : amountInRub;
        }

        public static decimal GetCurrentRate()
        {
            if (_currentCurrency == "RUB") return 1;
            return _allRates.GetValueOrDefault(_currentCurrency, 1);
        }

        public static string Format(decimal amountInRub)
        {
            decimal converted = Convert(amountInRub);
            return $"{converted:F2} {_currentCurrency}";
        }
    }
}
