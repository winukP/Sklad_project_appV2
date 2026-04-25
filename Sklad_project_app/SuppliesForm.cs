using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sklad_project_app;
using Sklad_project_app.Import;
using Sklad_project_app.Models;
using Sklad_project_app.Сurrency;


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
            ConfigureAccessByRole();
            LoadSupplies();
            this.Text = AppResources.CatalogTitle;
            lblUserInfo.Text = AppResources.LblStorekeeper
                + CurrentUser.User.Surname + " " + CurrentUser.User.Name;
        }
        private void ConfigureAccessByRole()
        {
            bool isAdmin = CurrentUser.RoleName == "Администратор";
            bool isStorekeeper = CurrentUser.RoleName == "Кладовщик";
            btnReports.Visible = isAdmin;
            btnExpirationDates.Visible = isAdmin;
            btnCurrency.Visible = isAdmin;
            btnHistory.Visible = isAdmin;
            btnSuplies.Visible = true;
            btnShipment.Visible = isStorekeeper;
            btnMyShipments.Visible = isStorekeeper;
            btnWrittenOff.Visible = true;
            if (isAdmin)
            {
                btnWrittenOff.Location = new Point(5, 194);
                btnHistory.Location = new Point(5, 48);
                btnSuplies.Location = new Point(5, 86);
                btnReports.Location = new Point(5, 122);
                btnExpirationDates.Location = new Point(5, 158);
                btnCurrency.Location = new Point(5, 229);
            }
            else if (isStorekeeper)
            {
                btnWrittenOff.Location = new Point(5, 158);
                btnSuplies.Location = new Point(5, 122);
            }
            btnWrittenOff.Parent?.PerformLayout();
        }
        private void LoadProductsToComboBox()
        {
            using (var db = new SkladContext())
            {
                var products = db.Products
                .Include(p => p.Category)
                .ToList();
                cmbProduct.DisplayMember = "Name";
                cmbProduct.ValueMember = "Id";
                cmbProduct.DataSource = products;
                cmbProduct.SelectedIndex = -1;
            }
        }
        private void SuppliesCatalogForm_Load(object sender, EventArgs e)
        {
            LoadCategoriesToFilter();
            LoadSupplies();
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

                var allSupplies = db.Supplies.ToList();
                var uniqueDates = new List<DateTime>();

                foreach (var supply in allSupplies)
                {
                    DateTime dateOnly = supply.SuppliesDate.Date;
                    if (!uniqueDates.Contains(dateOnly))
                    {
                        uniqueDates.Add(dateOnly);
                    }
                }

                uniqueDates.Sort((a, b) => b.CompareTo(a));

                cmbDate.Items.Clear();
                cmbDate.Items.Add("Все даты");

                foreach (var date in uniqueDates)
                {
                    cmbDate.Items.Add(date.ToString("dd.MM.yyyy"));
                }

                cmbDate.SelectedIndex = 0;
            }
        }
        private void UpdateDiscountsFromExpiry()
        {
            using (var db = new SkladContext())
            {
                var today = DateTime.Now.Date;

                var batches = db.StockBatches
                    .Include(b => b.Product)
                    .Where(b => !b.IsWrittenOff && b.Quantity > 0 && b.ExpiryDate != null)
                    .ToList();

                bool changed = false;

                foreach (var batch in batches)
                {
                    int daysLeft = (batch.ExpiryDate.Value.Date - today).Days;
                    int totalDays = batch.TotalDays;

                    decimal newDiscount = 0;

                    if (daysLeft <= 0)
                    {
                        newDiscount = 100;
                    }
                    else if (totalDays > 0)
                    {
                        double percentLeft = (double)daysLeft / totalDays * 100;
                        if (percentLeft <= 20)
                        {
                            newDiscount = 50;
                        }
                    }

                    if (batch.DiscountPercent != newDiscount)
                    {
                        batch.DiscountPercent = newDiscount;
                        changed = true;
                    }
                }

                if (changed)
                    db.SaveChanges();
            }
        }

        public void LoadSupplies()
        {
            try
            {
                UpdateDiscountsFromExpiry();
                using (var db = new SkladContext())
                {
                    var allSupplies = db.SuppliesItems
                        .Include(s => s.Product)
                        .ThenInclude(p => p.Category)
                        .Include(s => s.Product)
                        .Include(s => s.Supplies)
                        .OrderBy(s => s.Supplies.SuppliesDate)
                        .ToList();

                    int totalCount = allSupplies.Count;
                    var searchText = txtSearch.Text.Trim().ToLower();
                    var afterSearch = new List<SuppliesItem>();

                    if (string.IsNullOrEmpty(searchText))
                    {
                        afterSearch = allSupplies;
                    }
                    else
                    {
                        foreach (var supply in allSupplies)
                        {
                            var productName = supply.Product.Name.ToLower() ?? "";
                            var productArticle = supply.Product.Article.ToLower() ?? "";

                            if (productName.Contains(searchText) || productArticle.Contains(searchText))
                            {
                                afterSearch.Add(supply);
                            }
                        }
                    }

                    var afterCategory = new List<SuppliesItem>();

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

                    var afterDate = new List<SuppliesItem>();

                    if (cmbDate.SelectedIndex <= 0)
                    {
                        afterDate = afterCategory;
                    }
                    else
                    {
                        string selectedDateStr = cmbDate.SelectedItem.ToString();
                        DateTime selectedDate = DateTime.ParseExact(selectedDateStr, "dd.MM.yyyy", null);

                        foreach (var supply in afterCategory)
                        {
                            var supplyDate = supply.Supplies?.SuppliesDate.Date;

                            if (supplyDate == selectedDate)
                            {
                                afterDate.Add(supply);
                            }
                        }
                    }
                    decimal priceFrom = 0;
                    decimal priceTo = 1000000;
                    decimal.TryParse(txtPriceFrom.Text, out priceFrom);
                    decimal.TryParse(txtPriceTo.Text, out priceTo);

                    if (priceFrom < 0 || priceTo < 0)
                    {
                        MessageBox.Show("Цена не может быть отрицательной", "Ошибка",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (priceFrom < 0) priceFrom = 0;
                        if (priceTo < 0) priceTo = 1000000;
                    }

                    if (priceFrom > priceTo)
                    {
                        (priceFrom, priceTo) = (priceTo, priceFrom);
                    }

                    var afterPrice = new List<SuppliesItem>();
                    foreach (var supply in afterDate)
                    {
                        if (supply.PurchasePrice >= priceFrom && supply.PurchasePrice <= priceTo)
                        {
                            afterPrice.Add(supply);
                        }
                    }

                    lblFound.Text = $"Найдено: {afterPrice.Count} из {totalCount}";

                    dgvProducts.Rows.Clear();
                    dgvProducts.Columns.Clear();

                    dgvProducts.Columns.Add("colArticle", "Артикул");
                    dgvProducts.Columns.Add("colProductName", "Товар");
                    dgvProducts.Columns.Add("colCategory", "Категория");
                    dgvProducts.Columns.Add("colQuantity", "Количество");
                    dgvProducts.Columns.Add("colPrice", "Цена закупки");
                    dgvProducts.Columns.Add("colExpiryDate", "Годен до");
                    dgvProducts.Columns.Add("colDate", "Дата поставки");
                    dgvProducts.Columns.Add("colSupplyId", "SupplyId");
                    dgvProducts.Columns.Add("colProductId", "ProductId");
                    dgvProducts.Columns.Add("colId", "ID");
                    dgvProducts.Columns["colId"].Visible = false;

                    dgvProducts.Columns["colSupplyId"].Visible = false;
                    dgvProducts.Columns["colProductId"].Visible = false;
                    dgvProducts.Columns["colId"].Visible = false;

                    foreach (var supply in afterPrice)
                    {
                        var productName = supply.Product.Name ?? "—";
                        var categoryName = supply.Product.Category?.Name ?? "—";
                        var article = supply.Product.Article ?? "—";
                        var supplyDate = supply.Supplies.SuppliesDate.ToString("dd.MM.yyyy") ?? "—";
                        var expiryDate = db.StockBatches
                            .Where(b => b.SuppliesId == supply.SuppliesId && b.ProductId == supply.ProductId)
                            .Select(b => b.ExpiryDate)
                            .FirstOrDefault();
                        var expiryDateStr = expiryDate?.ToString("dd.MM.yyyy") ?? "—";



                        dgvProducts.Rows.Add(
                            article,
                            productName,
                            categoryName,
                            supply.Quantity,
                            CurrencyHelp.Format(supply.PurchasePrice),
                            expiryDateStr,
                            supplyDate,
                            supply.SuppliesId,
                            supply.ProductId,
                            supply.Id
                        );
                    }
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

        private void btnView_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите поставку для просмотра");
                return;
            }

            var selectedRow = dgvProducts.SelectedRows[0];
            string article = "";
            string productName = "";
            string category = "";
            string price = "";
            string quantity = "";
            string date = "";
            string expiryDate = "";

            if (dgvProducts.Columns.Contains("colArticle"))
                article = selectedRow.Cells["colArticle"].Value.ToString();

            if (dgvProducts.Columns.Contains("colProductName"))
                productName = selectedRow.Cells["colProductName"].Value.ToString();

            if (dgvProducts.Columns.Contains("colCategory"))
                category = selectedRow.Cells["colCategory"].Value.ToString();

            if (dgvProducts.Columns.Contains("colPrice"))
                price = selectedRow.Cells["colPrice"].Value.ToString();

            if (dgvProducts.Columns.Contains("colQuantity"))
                quantity = selectedRow.Cells["colQuantity"].Value.ToString();

            if (dgvProducts.Columns.Contains("colDate"))
                date = selectedRow.Cells["colDate"].Value.ToString();

            if (dgvProducts.Columns.Contains("colExpiryDate"))
                expiryDate = selectedRow.Cells["colExpiryDate"].Value.ToString();

            txtArticleView.Text = article;
            txtNameView.Text = productName;
            txtCategoryView.Text = category;
            txtPriceView.Text = price;
            txtRestView.Text = quantity;
            txtDateView.Text = date;
            txtExpirationDate.Text = expiryDate;

            txtArticleView.ReadOnly = true;
            txtNameView.ReadOnly = true;
            txtCategoryView.ReadOnly = true;
            txtPriceView.ReadOnly = true;
            txtRestView.ReadOnly = true;
            txtExpirationDate.ReadOnly = true;

            btnSave.Visible = false;
            dtpDate.Visible = false;
            cmbProduct.Visible = false;
            txtExpirationDate.Visible = true;
            lblExpirationDate.Text = "Годен до:";

            lblPanelTitle.Text = "Просмотр";
            panelView.Visible = true;
            panelView.BringToFront();

            cmbProduct.SelectedIndex = -1;
        }

        private void btnCloseView_Click(object sender, EventArgs e)
        {
            panelView.Visible = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSupplies();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            cmbCategory.SelectedIndex = 0;
            cmbDate.SelectedIndex = 0;
            txtPriceFrom.Text = "0";
            txtPriceTo.Text = "1000000";
            LoadSupplies();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            CurrentUser.User = null;
            CurrentUser.RoleName = null;
            var loginForm = Application.OpenForms.OfType<LoginForm>().FirstOrDefault();
            loginForm.ClearFields();
            loginForm.Show();
            loginForm.BringToFront();
            foreach (Form form in Application.OpenForms.Cast<Form>().ToList())
            {
                if (form != loginForm)
                    form.Close();
            }
        }

        private void btnCatalog_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnShipment_Click(object sender, EventArgs e)
        {
            var form = new ShipmentForm();
            form.ShowDialog();
            LoadSupplies();
        }

        private void btnMyShipments_Click(object sender, EventArgs e)
        {
            var form = new MyShipmentsForm();
            form.ShowDialog();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadSupplies();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSupplies();
        }

        private void cmbDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSupplies();
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
            var expirationdates = new ExpirationDatesForm();
            expirationdates.ShowDialog();
            this.Close();
        }

        private void btnAddComing_Click(object sender, EventArgs e)
        {
            _currentMode = PanelMode.Add;
            txtNameView.ReadOnly = false;
            txtDateView.ReadOnly = false;
            txtPriceView.ReadOnly = false;
            txtRestView.ReadOnly = false;
            txtArticleView.ReadOnly = true;

            dtpDate.Visible = true;
            btnSave.Visible = true;
            cmbProduct.Visible = true;

            txtExpirationDate.Visible = true;
            txtExpirationDate.ReadOnly = false;
            txtExpirationDate.Text = "";
            lblExpirationDate.Visible = true;

            panelView.Visible = true;
            panelView.BringToFront();
            lblPanelTitle.Text = "Добавление";

            txtArticleView.Text = "";
            txtNameView.Text = "";
            txtCategoryView.Text = "";
            txtDateView.Text = "";
            txtPriceView.Text = "";
            txtRestView.Text = "";

            cmbProduct.SelectedIndex = -1;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"..\..\..\Files";
            ofd.Title = "Выберите JSON файл с поставками";
            ofd.Filter = "JSON файлы (*.json)|*.json";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                int importedCount = 0;
                int skippedCount = 0;
                var skippedReasons = new List<string>();
                try
                {
                    string json = File.ReadAllText(ofd.FileName);
                    var supplies = JsonConvert.DeserializeObject<List<ImportSupplies>>(json);

                    if (supplies == null || supplies.Count == 0)
                    {
                        MessageBox.Show("Файл пуст");
                        return;
                    }

                    DialogResult result = MessageBox.Show(
                        $"Найдено {supplies.Count} позиций. Импортировать?",
                        "Подтверждение",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        using (var db = new SkladContext())
                        {
                            foreach (var item in supplies)
                            {
                                //Поиск
                                var product = db.Products.FirstOrDefault(p =>
                                    p.Article == item.Article || p.Name == item.ProductName);

                                if (product == null)
                                {
                                    skippedCount++;
                                    skippedReasons.Add($"Товар '{item.ProductName}' (арт. {item.Article}) не найден");
                                    continue;
                                }
                                var supply = new Supplies
                                {
                                    Id = Guid.NewGuid(),
                                    SuppliesDate = item.Date.ToUniversalTime(),
                                    UserId = CurrentUser.User.Id,
                                };
                                db.Supplies.Add(supply);

                                // Создание позиции поставки
                                var supplyItem = new SuppliesItem
                                {
                                    Id = Guid.NewGuid(),
                                    SuppliesId = supply.Id,
                                    ProductId = product.Id,
                                    Quantity = item.Quantity,
                                    PurchasePrice = item.Price
                                };
                                db.SuppliesItems.Add(supplyItem);

                                int totalDays = item.ExpiryDays > 0 ? item.ExpiryDays : 365; 
                                DateTime expiryDate = item.Date.Date.AddDays(totalDays).ToUniversalTime();

                                var stockBatch = new StockBatch
                                {
                                    Id = Guid.NewGuid(),
                                    ProductId = product.Id,
                                    SuppliesId = supply.Id,
                                    Quantity = item.Quantity,
                                    PurchasePrice = item.Price,
                                    ExpiryDate = expiryDate,
                                    TotalDays = totalDays,
                                    DiscountPercent = 0,
                                    IsWrittenOff = false
                                };
                                db.StockBatches.Add(stockBatch);

                                // Обновление остатка
                                var stock = db.Stocks.FirstOrDefault(s => s.ProductId == product.Id);
                                if (stock != null)
                                    stock.Rest += item.Quantity;
                                else
                                {
                                    stock = new Stock
                                    {
                                        Id = Guid.NewGuid(),
                                        ProductId = product.Id,
                                        Rest = item.Quantity,
                                        PurchasePrice = item.Price
                                    };
                                    db.Stocks.Add(stock);
                                }

                                importedCount++;
                            }
                            db.SaveChanges();
                        }
                        Logger.Debug($"DEBUG-08: JSON-файл поставки успешно импортирован.\n" +
                             $"Пользователь: {CurrentUser.User?.Login}\n" +
                             $"Файл: {ofd.FileName}\n" +
                             $"Всего строк: {supplies.Count} | Импортировано: {importedCount} | Пропущено: {skippedCount}\n" +
                             $"Время: {DateTime.Now}");
                        // WARN-04: Пропущенные строки при импорте
                        if (skippedCount > 0)
                        {
                            Logger.Warn($"WARN-04: Пропущенные строки при импорте JSON.\n" +
                                        $"Пользователь: {CurrentUser.User?.Login}\n" +
                                        $"Файл: {ofd.FileName}\n" +
                                        $"Всего строк: {supplies.Count} | Импортировано: {importedCount} | Пропущено: {skippedCount}\n" +
                                        $"Причины: {string.Join("; ", skippedReasons.Take(5))}");
                        }
                        else
                        {
                            MessageBox.Show($"Импортировано {importedCount} поставок!");
                        }
                        LoadSupplies();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"ERROR-07: Общая ошибка при импорте JSON-файла поставки.\n" +
                                 $"Пользователь: {CurrentUser.User?.Login}\n" +
                                 $"Файл: {ofd.FileName}\n" +
                                 $"Исключение: {ex.GetType()} --- {ex.Message}\n" +
                                 $"Стек: {ex.StackTrace}", ex);
                    MessageBox.Show($"Ошибка при импорте: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

            decimal priceInRub;
            if (CurrencyHelp.GetCurrentCurrency() != "RUB")
            {
                priceInRub = price * CurrencyHelp.GetCurrentRate();
            }
            else
            {
                priceInRub = price;
            }

            if (string.IsNullOrWhiteSpace(txtExpirationDate.Text))
            {
                MessageBox.Show("Введите срок годности (количество дней)", "Ошибка");
                return;
            }

            if (!int.TryParse(txtExpirationDate.Text, out int days) || days <= 0)
            {
                MessageBox.Show("Введите корректное количество дней (целое число больше 0)", "Ошибка");
                return;
            }

            DateTime expiryDate = dtpDate.Value.Date.AddDays(days).ToUniversalTime();
            int totalDays = days;

            var today = DateTime.Now.Date;
            int daysLeft = (expiryDate.Date - today).Days;
            decimal discount = 0;

            if (daysLeft <= 0)
            {
                discount = 100;
            }
            else if (totalDays > 0)
            {
                double percentLeft = (double)daysLeft / totalDays * 100;
                if (percentLeft <= 20)
                {
                    discount = 50;
                }
            }

            using (var db = new SkladContext())
            {
                if (cmbProduct.SelectedItem == null)
                {
                    MessageBox.Show("Выберите товар");
                    return;
                }

                var selectedProduct = (Product)cmbProduct.SelectedItem;
                var product = db.Products.Find(selectedProduct.Id);

                if (product == null)
                {
                    MessageBox.Show("Товар не найден");
                    return;
                }
                try
                {
                    var supply = new Supplies
                    {
                        Id = Guid.NewGuid(),
                        SuppliesDate = dtpDate.Value.ToUniversalTime(),
                        UserId = CurrentUser.User.Id,
                    };
                    db.Supplies.Add(supply);
                    db.SaveChanges();

                    var supplyItem = new SuppliesItem
                    {
                        Id = Guid.NewGuid(),
                        SuppliesId = supply.Id,
                        ProductId = product.Id,
                        Quantity = quantity,
                        PurchasePrice = priceInRub,
                    };
                    db.SuppliesItems.Add(supplyItem);

                    var stockBatch = new StockBatch
                    {
                        Id = Guid.NewGuid(),
                        ProductId = product.Id,
                        SuppliesId = supply.Id,
                        Quantity = quantity,
                        PurchasePrice = priceInRub,
                        ExpiryDate = expiryDate,
                        TotalDays = totalDays,
                        DiscountPercent = discount,
                        IsWrittenOff = false
                    };
                    db.StockBatches.Add(stockBatch);
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
                            PurchasePrice = priceInRub
                        };
                        db.Stocks.Add(stock);
                    }

                    db.SaveChanges();
                    Logger.Debug($"DEBUG-07: Поставка успешно сохранена.\n" +
                         $"Пользователь: {CurrentUser.User?.Login}\n" +
                         $"SupplyId: {supply.Id} | Дата: {dtpDate.Value:dd.MM.yyyy}\n" +
                         $"Товар: {product.Name} | Артикул: {product.Article}\n" +
                         $"Количество: {quantity} | Цена: {priceInRub} руб.\n" +
                         $"Срок годности: {expiryDate:dd.MM.yyyy} | Осталось дней: {daysLeft} | Скидка: {discount}%\n" +
                         $"Время: {DateTime.Now}");

                }
                catch (Exception ex)
                {
                    Logger.Error($"ERROR-03: Не удалось сохранить поставку.\n" +
                                 $"Пользователь: {CurrentUser.User?.Login}\n" +
                                 $"Товар: {cmbProduct.Text} | Количество: {quantity} | Цена: {price}\n" +
                                 $"Срок годности: {days} дней\n" +
                                 $"Исключение: {ex.GetType()} --- {ex.Message}\n" +
                                 $"Стек: {ex.StackTrace}", ex);
                    MessageBox.Show("Ошибка сохранения поставки", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            txtArticleView.Text = "";
            txtNameView.Text = "";
            txtPriceView.Text = "";
            txtRestView.Text = "";

            panelView.Visible = false;
            LoadSupplies();
            MessageBox.Show("Поставка сохранена!");
        }

        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedIndex == -1) return;

            var product = (Product)cmbProduct.SelectedItem;
            txtNameView.Text = product.Name;
            txtCategoryView.Text = product.Category?.Name ?? "";
            txtArticleView.Text = product.Article;
        }

        private void cmbCateg_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtPriceFrom_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPriceTo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtExpirationDate_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnWrittenOff_Click(object sender, EventArgs e)
        {
            var writeoffhistory = new WriteOffHistoryForm();
            writeoffhistory.ShowDialog();
            this.Close();
        }

        private void btnCurrency_Click(object sender, EventArgs e)
        {
            var currencyform = new CurrencyForm();
            currencyform.ShowDialog();
            this.Close();
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            var form = new ShipmentHistoryForm();
            form.ShowDialog();
            this.Close();
        }
    }

}

