using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Sklad_project_app.Сurrency;
using Sklad_project_app.Models;
using System;

namespace Sklad_project_app
{
    public partial class CurrencyForm : Form
    {
        private Dictionary<string, decimal> _allRates = new Dictionary<string, decimal>();
        private DateTime _lastUpdate = DateTime.Now;

        public CurrencyForm()
        {
            InitializeComponent();
            LoadCurrencies();
            LoadExchangeRates();
            LoadCurrentCurrency();
            this.Text = AppResources.CatalogTitle;
            lblUserInfo.Text = AppResources.LblStorekeeper
                + CurrentUser.User.Surname + " " + CurrentUser.User.Name;
        }

        private void LoadCurrencies()
        {
            dgvCurrencies.Rows.Clear();
            dgvCurrencies.Columns.Clear();

            dgvCurrencies.Columns.Add("colCurrency", "Валюта");
            dgvCurrencies.Columns.Add("colRate", "Курс к RUB");
            dgvCurrencies.Columns.Add("colDate", "Дата обновления");

            if (_allRates.Count == 0)
            {
                dgvCurrencies.Rows.Add("USD", "84.50", DateTime.Now.ToString("dd.MM.yyyy"));
                dgvCurrencies.Rows.Add("EUR", "97.00", DateTime.Now.ToString("dd.MM.yyyy"));
            }
            decimal priceFrom = 0;
            decimal priceTo = 1000000;
            decimal.TryParse(txtPriceFrom.Text, out priceFrom);
            decimal.TryParse(txtPriceTo.Text, out priceTo);

            if (priceFrom < 0) priceFrom = 0;
            if (priceTo < 0) priceTo = 1000000;

            if (priceFrom > priceTo)
            {
                (priceFrom, priceTo) = (priceTo, priceFrom);
            }
            int filteredCount = 0;
            foreach (var rate in _allRates.OrderBy(r => r.Key))
            {
                if (rate.Value >= priceFrom && rate.Value <= priceTo)
                {
                    dgvCurrencies.Rows.Add(
                        rate.Key,
                        rate.Value.ToString("F2"),
                        _lastUpdate.ToString("dd.MM.yyyy")
                    );
                    filteredCount++;
                }
            }
            lblFound.Text = $"Найдено: {filteredCount} из {_allRates.Count}";
        }
        private async void btnUpdateCurrency_Click(object sender, EventArgs e)
        {
            await LoadExchangeRates();
        }

        private void LoadCurrentCurrency()
        {
            string currency = CurrencyHelp.GetCurrentCurrency();
            lblCurrentCurrency.Text = $"Текущая валюта: {currency}";
        }

        public class ExchangeRateResponse
        {
            public Dictionary<string, double> rates { get; set; }
        }

        private async Task LoadExchangeRates()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = "https://api.exchangerate-api.com/v4/latest/RUB";
                    var response = await client.GetStringAsync(url);
                    var data = JsonSerializer.Deserialize<ExchangeRateResponse>(response);

                    if (data?.rates != null)
                    {
                        _allRates.Clear();

                        foreach (var rate in data.rates)
                        {
                            if (rate.Value > 0)
                            {
                                _allRates[rate.Key] = 1 / (decimal)rate.Value;
                            }
                        }

                        _lastUpdate = DateTime.Now;
                        CurrencyHelp.SetRates(_allRates);
                        LoadCurrencies();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
        private void btnCloseView_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _ = LoadExchangeRates();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            txtPriceFrom.Text = "0";
            txtPriceTo.Text = "1000000";
            LoadCurrencies();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            CurrentUser.User = null;
            CurrentUser.RoleName = null;
            var loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }

        private void btnCatalog_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnShipment_Click(object sender, EventArgs e)
        {
            var form = new ShipmentForm();
            form.ShowDialog();
        }

        private void btnMyShipments_Click(object sender, EventArgs e)
        {
            var form = new MyShipmentsForm();
            form.ShowDialog();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim().ToLower();

            dgvCurrencies.Rows.Clear();

            var filtered = _allRates
                .Where(r => r.Key.ToLower().Contains(search))
                .OrderBy(r => r.Key);

            int count = 0;
            foreach (var rate in filtered)
            {
                dgvCurrencies.Rows.Add(
                    rate.Key,
                    rate.Value.ToString("F2"),
                    _lastUpdate.ToString("dd.MM.yyyy")
                );
                count++;
            }

            if (string.IsNullOrEmpty(search))
            {
                lblFound.Text = $"Найдено: {_allRates.Count} из {_allRates.Count}";
            }
            else
            {
                lblFound.Text = $"Найдено: {count} из {_allRates.Count}";
            }
        }

        private void btnSuplies_Click(object sender, EventArgs e)
        {
            var suppliesForm = new SuppliesForm();
            suppliesForm.ShowDialog();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            var reportsForm = new ReportsForm();
            reportsForm.ShowDialog();
        }

        private void btnExpirationDates_Click(object sender, EventArgs e)
        {

        }

        private void btnSettings_Click(object sender, EventArgs e)
        {

        }

        private void txtPriceFrom_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dgvCurrencies.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите валюту из таблицы");
                return;
            }

            var selectedRow = dgvCurrencies.SelectedRows[0];
            string currencyCode = selectedRow.Cells["colCurrency"].Value?.ToString();

            if (string.IsNullOrEmpty(currencyCode)) return;

            CurrencyHelp.SetCurrency(currencyCode);
            LoadCurrentCurrency();

            foreach (Form form in Application.OpenForms)
            {
                if (form is StorekeeperCatalogForm catalog)
                    catalog.LoadProducts();
                else if (form is SuppliesForm supplies)
                    supplies.LoadSupplies();
                else if (form is ReportsForm reports)
                    reports.LoadReports();
                else if (form is MyShipmentsForm myShipments)
                    myShipments.LoadMyShipments();
                else if (form is AdminCatalogForm catalog2)
                    catalog2.LoadProducts();
            }

            MessageBox.Show($"Валюта изменена на {currencyCode}");
        }
    }
}
