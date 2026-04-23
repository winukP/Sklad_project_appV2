using Microsoft.EntityFrameworkCore;
using Sklad_project_app.Models;
using Sklad_project_app.Сurrency;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Sklad_project_app
{
    public partial class ReportsForm : Form
    {
        private Guid _selectedProductId = Guid.Empty;

        public ReportsForm()
        {
            InitializeComponent();
            this.Text = AppResources.CatalogTitle;
            lblUserInfo.Text = AppResources.LblStorekeeper
                + CurrentUser.User.Surname + " " + CurrentUser.User.Name;
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            LoadClientsToFilter();
            LoadReports();
            panelView.Visible = false;
        }

        private void LoadClientsToFilter()
        {
            using (var db = new SkladContext())
            {
                var clients = db.Clients.ToList();
                cmbClient.Items.Clear();
                cmbClient.Items.Add("Все");
                foreach (var client in clients)
                {
                    cmbClient.Items.Add(client.Name);
                }
                cmbClient.SelectedIndex = 0;
            }
        }

        public void LoadReports()
        {
            try
            {
                using (var db = new SkladContext())
                {
                    var allShipments = db.Shipments
                        .Include(s => s.Client)
                        .Include(s => s.ShipmentItems)
                        .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.Stock)
                        .OrderBy(s => s.ShipmentDate)
                        .ToList();

                    int totalCount = allShipments.Count;

                    DateTime dateFrom = dtpDateFrom.Value.Date;
                    DateTime dateTo = dtpDateTo.Value.Date.AddDays(1).AddSeconds(-1);

                    var afterDate = new List<Shipment>();
                    foreach (var shipment in allShipments)
                    {
                        if (shipment.ShipmentDate >= dateFrom && shipment.ShipmentDate <= dateTo)
                        {
                            afterDate.Add(shipment);
                        }
                    }

                    var afterClient = new List<Shipment>();

                    if (cmbClient.SelectedIndex <= 0 || cmbClient.SelectedItem.ToString() == "Все")
                    {
                        afterClient = afterDate;
                    }
                    else
                    {
                        string selectedClientName = cmbClient.SelectedItem.ToString();
                        foreach (var shipment in afterDate)
                        {
                            if (shipment.Client != null && shipment.Client.Name == selectedClientName)
                            {
                                afterClient.Add(shipment);
                            }
                        }
                    }

                    decimal profitFrom = 0;
                    decimal profitTo = 1000000;

                    decimal.TryParse(txtProfitFrom.Text, out profitFrom);
                    decimal.TryParse(txtProfitTo.Text, out profitTo);

                    if (profitFrom > profitTo)
                    {
                        (profitFrom, profitTo) = (profitTo, profitFrom);
                    }
                    var afterProfit = new List<Shipment>();
                    foreach (var shipment in afterClient)
                    {
                        decimal shipmentProfit = 0;
                        foreach (var item in shipment.ShipmentItems)
                        {
                            var supplies = db.SuppliesItems
                                .Where(s => s.ProductId == item.ProductId)
                                .ToList();

                            decimal totalCost = supplies.Sum(s => s.PurchasePrice * s.Quantity);
                            decimal totalQty = supplies.Sum(s => s.Quantity);
                            decimal avgPrice = totalQty > 0 ? totalCost / totalQty : 0;

                            shipmentProfit += item.Amount - (avgPrice * item.Quantity);
                        }

                        if (shipmentProfit >= profitFrom && shipmentProfit <= profitTo)
                        {
                            afterProfit.Add(shipment);
                        }
                    }

                    lblFound.Text = $"Найдено: {afterProfit.Count} из {totalCount}";

                    dgvReports.Rows.Clear();
                    dgvReports.Columns.Clear();
                    dgvReports.Columns.Add("colDate", "Дата");
                    dgvReports.Columns.Add("colClient", "Покупатель");
                    dgvReports.Columns.Add("colAmount", "Сумма");
                    dgvReports.Columns.Add("colProfit", "Прибыль");

                    decimal totalAmount = 0;
                    decimal totalProfit = 0;

                    foreach (var shipment in afterProfit)
                    {
                        decimal shipmentAmount = 0;
                        decimal shipmentProfit = 0;

                        foreach (var item in shipment.ShipmentItems)
                        {
                            shipmentAmount += item.Amount;

                            var productId = item.ProductId;

                            var supplies = db.SuppliesItems
                                .Where(s => s.ProductId == productId)
                                .ToList();

                            decimal totalCost = supplies.Sum(s => s.PurchasePrice * s.Quantity);
                            decimal totalQty = supplies.Sum(s => s.Quantity);
                            decimal avgPrice = totalQty > 0 ? totalCost / totalQty : 0;

                            decimal cost = avgPrice * item.Quantity;
                            decimal profit = item.Amount - cost;

                            shipmentProfit += profit;
                        }

                        totalAmount += shipmentAmount;
                        totalProfit += shipmentProfit;

                        dgvReports.Rows.Add(
                            shipment.ShipmentDate?.ToString("dd.MM.yyyy") ?? "—",
                            shipment.Client?.Name ?? "—",
                            CurrencyHelp.Format(shipmentAmount),
                            CurrencyHelp.Format(shipmentProfit)
                        );
                    }

                    dgvReports.Rows.Add("ИТОГО:", "", CurrencyHelp.Format(totalAmount), CurrencyHelp.Format(totalProfit));
                }
            }
            catch (Exception ex)
            {
                Logger.Fatal($"FATAL-04: Соединение с базой данных утеряно и не восстановлено.\n" +
                             $"Активный пользователь: {CurrentUser.User?.Login} | Роль: {CurrentUser.RoleName}\n" +
                             $"Текущая операция: Загрузка каталога товаров\n" +
                             $"Исключение: {ex.GetType()} --- {ex.Message}\n" +
                             $"Приложение будет завершено.", ex);
                MessageBox.Show("Потеряно соединение с базой данных.\nПриложение будет закрыто.",
                    "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                string dateFrom = dtpDateFrom.Value.ToString("dd.MM.yyyy");
                string dateTo = dtpDateTo.Value.ToString("dd.MM.yyyy");

                // Формируем имя файла
                saveDialog.FileName = $"Отчет_{dateFrom}-{dateTo}.csv";
                saveDialog.Filter = "CSV files (*.csv)|*.csv";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportToCSV(saveDialog.FileName);
                }
            }
        }

        private void ExportToCSV(string filePath)
        {
            try
            {
                var sb = new StringBuilder();

                // Заголовки
                sb.AppendLine("Дата;Покупатель;Сумма, руб.;Прибыль, руб.");

                // Данные из DataGridView (кроме последней строки "ИТОГО")
                for (int i = 0; i < dgvReports.Rows.Count - 1; i++)
                {
                    var row = dgvReports.Rows[i];
                    if (row.IsNewRow) continue;

                    var date = row.Cells["colDate"].Value?.ToString() ?? "";
                    var client = row.Cells["colClient"].Value?.ToString() ?? "";
                    var amount = row.Cells["colAmount"].Value?.ToString() ?? "";
                    var profit = row.Cells["colProfit"].Value?.ToString() ?? "";

                    sb.AppendLine($"{date};{client};{amount};{profit}");
                }

                // Итоговая строка
                var lastRow = dgvReports.Rows[dgvReports.Rows.Count - 1];
                sb.AppendLine($"ИТОГО;;{lastRow.Cells["colAmount"].Value?.ToString() ?? ""};{lastRow.Cells["colProfit"].Value?.ToString() ?? ""}");

                // Сохраняем файл
                File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);

                MessageBox.Show($"Отчёт сохранён:\n{filePath}", "Экспорт выполнен",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Logger.Error($"ERROR-08: Не удалось экспортировать отчёт в CSV.\n" +
                             $"Пользователь: {CurrentUser.User?.Login}\n" +
                             $"Путь сохранения: {filePath}\n" +
                             $"Период отчёта: {dtpDateFrom.Value:dd.MM.yyyy} - {dtpDateTo.Value:dd.MM.yyyy}\n" +
                             $"Исключение: {ex.GetType()} --- {ex.Message}\n" +
                             $"Стек: {ex.StackTrace}", ex);
                MessageBox.Show($"Ошибка при экспорте отчёта: {ex.Message}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCloseView_Click(object sender, EventArgs e)
        {
            panelView.Visible = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadReports();
            LoadClientsToFilter();

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cmbClient.SelectedIndex = 0;
            txtProfitFrom.Text = "0";
            txtProfitTo.Text = "1000000";
            LoadReports();
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
            LoadReports();
        }

        private void btnMyShipments_Click(object sender, EventArgs e)
        {
            var form = new MyShipmentsForm();
            form.ShowDialog();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadReports();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadReports();
        }

        private void cmbAvailability_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadReports();
        }

        private void btnSuplies_Click(object sender, EventArgs e)
        {
            var suppliesForm = new SuppliesForm();
            suppliesForm.ShowDialog();
            this.Close();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {

        }

        private void btnExpirationDates_Click(object sender, EventArgs e)
        {

        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            var settingsForm = new CurrencyForm();
            settingsForm.ShowDialog();
            this.Close();
        }

        private void lblSearch_Click(object sender, EventArgs e)
        {

        }

        private void dtpDateFrom_ValueChanged(object sender, EventArgs e)
        {
            LoadReports();
        }

        private void dtpDateTo_ValueChanged(object sender, EventArgs e)
        {
            LoadReports();
        }

        private void txtProfitFrom_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

