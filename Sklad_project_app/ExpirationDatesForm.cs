using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sklad_project_app;
using Sklad_project_app.Import;
using Sklad_project_app.Models;
using Sklad_project_app.Сurrency;


namespace Sklad_project_app
{
    public partial class ExpirationDatesForm : Form
    {
        private PanelMode _currentMode = PanelMode.None;
        private Guid _selectedProductId = Guid.Empty;

        public ExpirationDatesForm()
        {
            InitializeComponent();
            LoadExpiringProducts();
            this.Text = AppResources.CatalogTitle;
            lblUserInfo.Text = AppResources.LblStorekeeper
                + CurrentUser.User.Surname + " " + CurrentUser.User.Name;
        }
        private void SuppliesCatalogForm_Load(object sender, EventArgs e)
        {
            LoadToFilter();
            LoadExpiringProducts();
        }

        private void LoadToFilter()
        {
            cmbDiscount.Items.Clear();
            cmbDiscount.Items.Add("Все");
            cmbDiscount.Items.Add("Норма");
            cmbDiscount.Items.Add("Скидка 50%");
            cmbDiscount.Items.Add("Просрочено");
            cmbDiscount.SelectedIndex = 0;
        }

        public void LoadExpiringProducts()
        {
            using (var db = new SkladContext())
            {
                var today = DateTime.Now.Date;

                var batches = db.StockBatches
                    .Include(b => b.Product)
                    .ThenInclude(p => p.Category)
                    .Where(b => b.IsWrittenOff == false && b.Quantity > 0 && b.ExpiryDate != null)
                    .ToList();

                dgvExpiry.Rows.Clear();
                dgvExpiry.Columns.Clear();

                dgvExpiry.Columns.Add("colId", "ID");
                dgvExpiry.Columns.Add("colProduct", "Товар");
                dgvExpiry.Columns.Add("colCategory", "Категория");
                dgvExpiry.Columns.Add("colQuantity", "Остаток");
                dgvExpiry.Columns.Add("colExpiry", "Годен до");
                dgvExpiry.Columns.Add("colDaysLeft", "Дней осталось");
                dgvExpiry.Columns.Add("colDiscount", "Скидка");
                dgvExpiry.Columns.Add("colStatus", "Статус");
                dgvExpiry.Columns["colId"].Visible = false;

                string searchText = txtSearch.Text.Trim().ToLower();
                string selectedStatus = cmbDiscount.SelectedItem?.ToString();

                foreach (var batch in batches)
                {
                    string productName = batch.Product?.Name ?? "";
                    if (!string.IsNullOrEmpty(searchText) && !productName.ToLower().Contains(searchText))
                    {
                        continue;
                    }
                    int daysLeft = (batch.ExpiryDate.Value.Date - today).Days;
                    int totalDays = batch.TotalDays;
                    decimal discount = 0;
                    string status = "";

                    if (daysLeft <= 0)
                    {
                        discount = 100;
                        status = "Просрочен";
                        daysLeft = 0;
                    }
                    else if (totalDays > 0)
                    {
                        double percentLeft = (double)daysLeft / totalDays * 100;

                        if (percentLeft <= 20)
                        {
                            discount = 50;
                            status = "Скидка есть";
                        }
                        else
                        {
                            discount = 0;
                            status = "Норма";
                        }
                    }
                    else
                    {
                        discount = 0;
                        status = "Норма";
                    }

                    if (selectedStatus != "Все" && status != selectedStatus)
                        continue;

                    if (batch.DiscountPercent != discount)
                    {
                        batch.DiscountPercent = discount;
                        db.SaveChanges();
                    }

                    dgvExpiry.Rows.Add(
                        batch.Id,
                        batch.Product?.Name ?? "—",
                        batch.Product?.Category?.Name ?? "—",
                        batch.Quantity,
                        batch.ExpiryDate.Value.ToString("dd.MM.yyyy"),
                        daysLeft,
                        $"{discount}%",
                        status
                    );

                    var row = dgvExpiry.Rows[dgvExpiry.Rows.Count - 1];
                    if (status == "Просрочен")
                    {
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                    }
                    else if (discount > 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.Orange;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                }
                lblFound.Text = $"Найдено: {dgvExpiry.Rows.Count} из {batches.Count}";
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadExpiringProducts();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            cmbDiscount.SelectedIndex = 0;
            LoadExpiringProducts();
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
            LoadExpiringProducts();
        }

        private void btnMyShipments_Click(object sender, EventArgs e)
        {
            var form = new MyShipmentsForm();
            form.ShowDialog();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadExpiringProducts();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadExpiringProducts();
        }

        private void cmbDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadExpiringProducts();
        }

        private void btnSuplies_Click(object sender, EventArgs e)
        {

        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            var reportsForm = new ReportsForm();
            reportsForm.ShowDialog();
            this.Close();
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

        private void txtPriceFrom_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPriceTo_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnWriteOff_Click(object sender, EventArgs e)
        {
            if (dgvExpiry.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите товар для списания");
                return;
            }

            var selectedRow = dgvExpiry.SelectedRows[0];
            var batchId = (Guid)selectedRow.Cells["colId"].Value;
            var status = selectedRow.Cells["colStatus"].Value?.ToString();
            var productName = selectedRow.Cells["colProduct"].Value?.ToString();
            var quantity = selectedRow.Cells["colQuantity"].Value?.ToString();

            if (status != "Просрочен")
            {
                MessageBox.Show("Можно списывать только просроченные товары");
                return;
            }

            var result = MessageBox.Show(
                $"Списать товар '{productName}' в количестве {quantity} шт.?",
                "Подтверждение списания",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (var db = new SkladContext())
                {
                    var batch = db.StockBatches.Find(batchId);
                    if (batch != null)
                    {
                        var writeOff = new WriteOff
                        {
                            Id = Guid.NewGuid(),
                            ProductId = batch.ProductId,
                            BatchId = batch.Id,
                            WriteOffDate = DateTime.Now.ToUniversalTime(),
                            Quantity = batch.Quantity,
                            LossAmount = batch.Quantity * batch.PurchasePrice,
                            Reason = "Истёк срок годности"
                        };
                        db.WriteOffs.Add(writeOff);

                        batch.IsWrittenOff = true;
                        batch.Quantity = 0;
                        batch.DiscountPercent = 0;

                        var stock = db.Stocks.FirstOrDefault(s => s.ProductId == batch.ProductId);
                        if (stock != null)
                        {
                            stock.Rest -= batch.Quantity;
                        }

                        db.SaveChanges();

                        MessageBox.Show($"Товар '{productName}' списан");
                        LoadExpiringProducts();
                    }
                }
            }
        }
    }
}


