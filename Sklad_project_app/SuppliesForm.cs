using Microsoft.EntityFrameworkCore;
using Sklad_project_app.Models;
using Sklad_project_app;


namespace Sklad_project_app
{
    public partial class SuppliesForm : Form
    {
        private PanelMode _currentMode = PanelMode.None;
        private Guid _selectedProductId = Guid.Empty;

        public SuppliesForm()
        {
            InitializeComponent();
            LoadProductsToComboBox();
            LoadArticlesToComboBox();
            LoadCategoriesToComboBox();
            LoadProducts();
            this.Text = AppResources.CatalogTitle;
            lblUserInfo.Text = AppResources.LblStorekeeper
                + CurrentUser.User.Surname + " " + CurrentUser.User.Name;
        }
        private void LoadProductsToComboBox()
        {
            using (var db = new SkladContext())
            {
                var products = db.Products.ToList();
                cmbProduct.DisplayMember = "Name";
                cmbProduct.ValueMember = "Id";
                cmbProduct.DataSource = products;
            }
        }
        private void LoadArticlesToComboBox()
        {
            using (var db = new SkladContext())
            {
                var products = db.Products
                    .Include(p => p.Category)
                    .ToList();

                cmbArtic.DisplayMember = "Article";
                cmbArtic.ValueMember = "Id";
                cmbArtic.DataSource = products;
            }
        }
        private void LoadCategoriesToComboBox()
        {
            using (var db = new SkladContext())
            {
                var categories = db.Categories.ToList();

                cmbCateg.DisplayMember = "Name";
                cmbCateg.ValueMember = "Id";
                cmbCateg.DataSource = categories;
            }
        }
        private void SuppliesCatalogForm_Load(object sender, EventArgs e)
        {
            LoadCategoriesToFilter();
            LoadProducts();
            panelView.Visible = false;
        }

        private void LoadCategoriesToFilter()
        {
            using (var db = new SkladContext())
            {
                var categories = db.Categories.ToList();
                cmbCategory.Items.Clear();
                cmbCategory.Items.Add(AppResources.FilterAll);
                foreach (var category in categories)
                {
                    cmbCategory.Items.Add(category.Name);
                }
                cmbCategory.SelectedIndex = 0;
                // ДАТЫ (как с категориями, но без Distinct)
                var allSupplies = db.Supplies.ToList();  // загружаем все поставки
                var uniqueDates = new List<DateTime>();

                foreach (var supply in allSupplies)
                {
                    DateTime dateOnly = supply.SuppliesDate.Date;
                    if (!uniqueDates.Contains(dateOnly))
                    {
                        uniqueDates.Add(dateOnly);
                    }
                }

                uniqueDates.Sort((a, b) => b.CompareTo(a));  // сортировка по убыванию

                cmbDate.Items.Clear();
                cmbDate.Items.Add("Все даты");

                foreach (var date in uniqueDates)
                {
                    cmbDate.Items.Add(date.ToString("dd.MM.yyyy"));
                }

                cmbDate.SelectedIndex = 0;
            }
        }

        private void LoadProducts()
        {
            // Загружаем поставки (SupplyItems)
            using (var db = new SkladContext())
            {
                var allSupplies = db.SuppliesItems  // ← Исправь: SuppliesItems → SupplyItems
                    .Include(s => s.Product)
                    .ThenInclude(p => p.Category)
                    .Include(s => s.Product)
                    .Include(s => s.Supplies)  // ← Исправь: Supplies → Supply
                    .ToList();

                int totalCount = allSupplies.Count;

                // Поиск по тексту
                var searchText = txtSearch.Text.Trim().ToLower();
                var afterSearch = new List<SuppliesItem>();  // ← Исправь: SuppliesItem → SupplyItem

                if (string.IsNullOrEmpty(searchText))
                {
                    afterSearch = allSupplies;
                }
                else
                {
                    foreach (var supply in allSupplies)
                    {
                        var productName = supply.Product?.Name?.ToLower() ?? "";
                        var productArticle = supply.Product?.Article?.ToLower() ?? "";

                        if (productName.Contains(searchText) || productArticle.Contains(searchText))
                        {
                            afterSearch.Add(supply);
                        }
                    }
                }

                // Фильтр по категории
                var afterCategory = new List<SuppliesItem>();  // ← Исправь

                if (cmbCategory.SelectedIndex <= 0)
                {
                    afterCategory = afterSearch;
                }
                else
                {
                    var selectedCategoryName = cmbCategory.SelectedItem.ToString();
                    foreach (var supply in afterSearch)
                    {
                        if (supply.Product?.Category != null && supply.Product.Category.Name == selectedCategoryName)
                        {
                            afterCategory.Add(supply);
                        }
                    }
                }

                // Фильтр по дате
                var afterDate = new List<SuppliesItem>();

                if (cmbDate.SelectedIndex <= 0)  // если выбран "Все даты" (индекс 0)
                {
                    afterDate = afterCategory;
                }
                else
                {
                    string selectedDateStr = cmbDate.SelectedItem.ToString();
                    DateTime selectedDate = DateTime.ParseExact(selectedDateStr, "dd.MM.yyyy", null);

                    foreach (var supply in afterCategory)
                    {
                        var supplyDate = supply.Supplies?.SuppliesDate.Date ?? DateTime.MinValue;

                        if (supplyDate == selectedDate)
                        {
                            afterDate.Add(supply);
                        }
                    }
                }

                // Настройка таблицы
                dgvProducts.Rows.Clear();
                dgvProducts.Columns.Clear();

                // Колонки для отображения поставок
                dgvProducts.Columns.Add("colArticle", "Артикул");
                dgvProducts.Columns.Add("colProductName", "Товар");
                dgvProducts.Columns.Add("colCategory", "Категория");
                dgvProducts.Columns.Add("colQuantity", "Количество");
                dgvProducts.Columns.Add("colPrice", "Цена закупки");
                dgvProducts.Columns.Add("colDate", "Дата поставки");
                dgvProducts.Columns.Add("colSupplyId", "SupplyId");
                dgvProducts.Columns.Add("colProductId", "ProductId");
                dgvProducts.Columns.Add("colId", "ID");
                dgvProducts.Columns["colId"].Visible = false;

                // Скрываем ID колонки
                dgvProducts.Columns["colSupplyId"].Visible = false;
                dgvProducts.Columns["colProductId"].Visible = false;
                dgvProducts.Columns["colId"].Visible = false;

                // Заполняем таблицу
                foreach (var supply in afterDate)
                {
                    var productName = supply.Product?.Name ?? "—";
                    var categoryName = supply.Product?.Category?.Name ?? "—";
                    var article = supply.Product?.Article ?? "—";

                    // БЕРЁМ ДАТУ ИЗ ШАПКИ (Supplies)
                    var supplyDate = supply.Supplies?.SuppliesDate.ToString("dd.MM.yyyy") ?? "—";

                    dgvProducts.Rows.Add(
                        article,
                        productName,
                        categoryName,
                        supply.Quantity,
                        supply.PurchasePrice.ToString("0.00") + " руб.",
                        supplyDate,
                        supply.SuppliesId,
                        supply.ProductId,
                        supply.Id
                    );
                }
            } // ← ВСЕ ОПЕРАЦИИ ВНУТРИ using
        }


        private void btnView_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите поставку для просмотра");
                return;
            }

            var selectedRow = dgvProducts.SelectedRows[0];

            // Проверка - какие колонки есть в таблице
            string article = "";
            string productName = "";
            string category = "";
            string price = "";
            string quantity = "";
            string date = "";

            if (dgvProducts.Columns.Contains("colArticle"))
                article = selectedRow.Cells["colArticle"].Value?.ToString();

            if (dgvProducts.Columns.Contains("colProductName"))
                productName = selectedRow.Cells["colProductName"].Value?.ToString();

            if (dgvProducts.Columns.Contains("colCategory"))
                category = selectedRow.Cells["colCategory"].Value?.ToString();

            if (dgvProducts.Columns.Contains("colPrice"))
                price = selectedRow.Cells["colPrice"].Value?.ToString();

            if (dgvProducts.Columns.Contains("colQuantity"))
                quantity = selectedRow.Cells["colQuantity"].Value?.ToString();

            if (dgvProducts.Columns.Contains("colDate"))  // ← ДОБАВИТЬ
                date = selectedRow.Cells["colDate"].Value?.ToString();

            // Заполняем поля (используй свои имена TextBox)
            txtArticleView.Text = article;
            txtNameView.Text = productName;
            txtCategoryView.Text = category;
            txtPriceView.Text = price;
            txtRestView.Text = quantity;
            txtDateView.Text = date;

            // Делаем поля только для чтения
            txtArticleView.ReadOnly = true;
            txtNameView.ReadOnly = true;
            txtCategoryView.ReadOnly = true;
            txtPriceView.ReadOnly = true;
            txtRestView.ReadOnly = true;

            // Скрываем ненужное
            btnSave.Visible = false;
            dtpDate.Visible = false;
            cmbProduct.Visible = false;
            cmbArtic.Visible = false;
            cmbCateg.Visible = false;

            lblPanelTitle.Text = "ПРОСМОТР ПОСТАВКИ";
            panelView.Visible = true;
            panelView.BringToFront();
        }

        private void btnCloseView_Click(object sender, EventArgs e)
        {
            panelView.Visible = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            cmbCategory.SelectedIndex = 0;
            cmbDate.SelectedIndex = 0;
            txtPriceFrom.Text = "0";
            txtPriceTo.Text = "1000000";
            LoadProducts();
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
            LoadProducts();
        }

        private void btnMyShipments_Click(object sender, EventArgs e)
        {
            var form = new MyShipmentsForm();
            form.ShowDialog();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void cmbDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void btnSuplies_Click(object sender, EventArgs e)
        {

        }

        private void btnReports_Click(object sender, EventArgs e)
        {

        }

        private void btnExpirationDates_Click(object sender, EventArgs e)
        {

        }

        private void btnSettings_Click(object sender, EventArgs e)
        {

        }

        private void btnAddComing_Click(object sender, EventArgs e)
        {
            _currentMode = PanelMode.Add;
            txtArticleView.Visible = false;
            txtNameView.ReadOnly = false;
            txtCategoryView.ReadOnly = false;
            txtDateView.ReadOnly = false;
            txtPriceView.ReadOnly = false;
            txtRestView.ReadOnly = false;
            dtpDate.Visible = true;
            btnSave.Visible = true;
            cmbProduct.Visible = true;
            cmbArtic.Visible = true;
            cmbCateg.Visible = true;
            panelView.Visible = true;
            panelView.BringToFront();
            lblPanelTitle.Text = "Добавление прихода";
            txtArticleView.Text = "";
            txtNameView.Text = "";
            txtCategoryView.Text = "";
            txtDateView.Text = "";
            txtPriceView.Text = "";
            txtRestView.Text = "";
        }

        private void btnImport_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            decimal price;
            int quantity;

            if (!decimal.TryParse(txtPriceView.Text, out price))
            {
                MessageBox.Show("Цена должна быть числом");
                return;
            }

            if (!int.TryParse(txtRestView.Text, out quantity))
            {
                MessageBox.Show("Количество должно быть числом");
                return;
            }

            using (var db = new SkladContext())
            {
                if (cmbArtic.SelectedItem == null)
                {
                    MessageBox.Show("Выберите товар");
                    return;
                }

                var selectedProduct = (Product)cmbArtic.SelectedItem;
                var product = db.Products.Find(selectedProduct.Id);

                if (product == null)
                {
                    MessageBox.Show("Товар не найден");
                    return;
                }

                // 1. СОЗДАЁМ ШАПКУ
                var supply = new Supplies
                {
                    Id = Guid.NewGuid(),
                    SuppliesDate = dtpDate.Value.ToUniversalTime(),
                    UserId = CurrentUser.User.Id,
                };
                db.Supplies.Add(supply);
                db.SaveChanges();  // ← сохраняем шапку

                // 2. СОЗДАЁМ ДЕТАЛЬ
                var supplyItem = new SuppliesItem
                {
                    Id = Guid.NewGuid(),
                    SuppliesId = supply.Id,  // ← ссылка на шапку
                    ProductId = product.Id,
                    Quantity = quantity,
                    PurchasePrice = price,
                };
                db.SuppliesItems.Add(supplyItem);

                // 3. ОБНОВЛЯЕМ ОСТАТОК
                var stock = db.Stocks.FirstOrDefault(s => s.ProductId == product.Id);
                if (stock != null)
                {
                    stock.Rest += quantity;
                }
                else
                {
                    stock = new Stock
                    {
                        Id = Guid.NewGuid(),
                        ProductId = product.Id,
                        Rest = quantity,
                        PurchasePrice = price
                    };
                    db.Stocks.Add(stock);
                }

                db.SaveChanges();
            }
            // Очищаем поля
            txtArticleView.Text = "";
            txtNameView.Text = "";
            txtPriceView.Text = "";
            txtRestView.Text = "";

            panelView.Visible = false;
            LoadProducts();
            MessageBox.Show("Поставка сохранена!");
        }

        private void txtUnitView_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbArtic_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}

