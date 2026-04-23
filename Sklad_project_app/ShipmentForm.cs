using Microsoft.EntityFrameworkCore;
using Sklad_project_app;
using Sklad_project_app.Models;
using Sklad_project_app.Сurrency;

namespace Sklad_project_app
{
    public partial class ShipmentForm : Form
    {
        private Dictionary<Guid, ShipmentItemInfo> _shipmentItems = new Dictionary<Guid, ShipmentItemInfo>();

        public ShipmentForm()
        {
            InitializeComponent();
            lblUserInfo.Text = AppResources.LblStorekeeper
                + CurrentUser.User.Surname + " " + CurrentUser.User.Name;
        }

        private void ShipmentForm_Load(object sender, EventArgs e)
        {
            LoadCategoriesToFilter();
            LoadProducts();
            dtpDate.Value = DateTime.Today;
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
            }

            cmbAvailability.Items.Clear();
            cmbAvailability.Items.Add(AppResources.FilterAvailAll);
            cmbAvailability.Items.Add(AppResources.FilterAvailIn);
            cmbAvailability.Items.Add(AppResources.FilterAvailOut);
            cmbAvailability.SelectedIndex = 0;
        }

        public void LoadProducts()
        {
            try
            {
                using (var db = new SkladContext())
                {
                    var allProducts = db.Products
                        .Include("Category")
                        .Include("Stock")
                        .ToList();

                    var searchText = txtSearch.Text.Trim().ToLower();
                    var afterSearch = new List<Product>();

                    if (string.IsNullOrEmpty(searchText))
                    {
                        afterSearch = allProducts;
                    }
                    else
                    {
                        foreach (var product in allProducts)
                        {
                            var productName = "";
                            var productArticle = "";

                            if (product.Name != null)
                            {
                                productName = product.Name.ToLower();
                            }
                            if (product.Article != null)
                            {
                                productArticle = product.Article.ToLower();
                            }

                            if (productName.Contains(searchText) || productArticle.Contains(searchText))
                            {
                                afterSearch.Add(product);
                            }
                        }
                    }

                    var afterCategory = new List<Product>();
                    if (cmbCategory.SelectedIndex <= 0)
                    {
                        afterCategory = afterSearch;
                    }
                    else
                    {
                        var selectedCategoryName = cmbCategory.SelectedItem.ToString();
                        foreach (var product in afterSearch)
                        {
                            if (product.Category != null && product.Category.Name == selectedCategoryName)
                            {
                                afterCategory.Add(product);
                            }
                        }
                    }

                    var afterAvailability = new List<Product>();
                    if (cmbAvailability.SelectedIndex == 1)
                    {
                        foreach (var product in afterCategory)
                        {
                            if (product.Stock != null && product.Stock.Rest > 0)
                            {
                                afterAvailability.Add(product);
                            }
                        }
                    }
                    else if (cmbAvailability.SelectedIndex == 2)
                    {
                        foreach (var product in afterCategory)
                        {
                            if (product.Stock == null || product.Stock.Rest == 0)
                            {
                                afterAvailability.Add(product);
                            }
                        }
                    }
                    else
                    {
                        afterAvailability = afterCategory;
                    }

                    dgvShipment.Rows.Clear();
                    dgvShipment.Columns.Clear();

                    dgvShipment.Columns.Add("colArticle", AppResources.ColArticle);
                    dgvShipment.Columns.Add("colName", AppResources.ColName);
                    dgvShipment.Columns.Add("colCategory", AppResources.ColCategory);
                    dgvShipment.Columns.Add("colPrice", "Цена");
                    dgvShipment.Columns.Add("colDiscount", "Скидка");
                    dgvShipment.Columns.Add("colRest", AppResources.ColRest);

                    var btnMinus = new DataGridViewButtonColumn();
                    btnMinus.Name = "colMinus";
                    btnMinus.HeaderText = "";
                    btnMinus.Text = "-";
                    btnMinus.UseColumnTextForButtonValue = true;
                    dgvShipment.Columns.Add(btnMinus);

                    dgvShipment.Columns.Add("colQty", "Взято");

                    var btnPlus = new DataGridViewButtonColumn();
                    btnPlus.Name = "colPlus";
                    btnPlus.HeaderText = "";
                    btnPlus.Text = "+";
                    btnPlus.UseColumnTextForButtonValue = true;
                    dgvShipment.Columns.Add(btnPlus);

                    dgvShipment.Columns.Add("colId", "ID");
                    dgvShipment.Columns["colId"].Visible = false;

                    foreach (var product in afterAvailability)
                    {
                        var price = "—";
                        var discount = "Нет";
                        var rest = "0";
                        var categoryName = "";

                        if (product.Stock != null)
                        {
                            // Получаем максимальную скидку для товара
                            var maxDiscount = db.StockBatches
                                .Where(b => b.ProductId == product.Id && !b.IsWrittenOff && b.Quantity > 0)
                                .OrderByDescending(b => b.DiscountPercent)
                                .Select(b => b.DiscountPercent)
                                .FirstOrDefault();

                            decimal originalPrice = product.Stock.PurchasePrice;

                            if (maxDiscount > 0)
                            {
                                decimal discountedPrice = originalPrice * (1 - maxDiscount / 100);
                                price = $"{CurrencyHelp.Format(originalPrice)} → {CurrencyHelp.Format(discountedPrice)}";
                                discount = $"{maxDiscount}%";
                            }
                            else
                            {
                                price = CurrencyHelp.Format(originalPrice);
                                discount = "Нет";
                            }

                            rest = product.Stock.Rest.ToString();
                        }

                        if (product.Category != null)
                        {
                            categoryName = product.Category.Name;
                        }

                        int qty = 0;
                        if (_shipmentItems.ContainsKey(product.Id))
                        {
                            qty = _shipmentItems[product.Id].Quantity;
                        }

                        int rowIndex = dgvShipment.Rows.Add(
                            product.Article,
                            product.Name,
                            categoryName,
                            price,
                            discount,
                            rest,
                            "-",
                            qty.ToString(),
                            "+",
                            product.Id
                        );

                        if (discount != "Нет")
                        {
                            dgvShipment.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightYellow;
                        }
                    }

                    UpdateTotal();
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

        private void dgvShipment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var idValue = dgvShipment.Rows[e.RowIndex].Cells["colId"].Value?.ToString();
            if (!Guid.TryParse(idValue, out Guid productId)) return;

            int rest = int.TryParse(dgvShipment.Rows[e.RowIndex].Cells["colRest"].Value?.ToString(), out int r) ? r : 0;
            int currentQty = _shipmentItems.ContainsKey(productId) ? _shipmentItems[productId].Quantity : 0;
            string productName = dgvShipment.Rows[e.RowIndex].Cells["colName"].Value?.ToString();

            if (e.ColumnIndex == dgvShipment.Columns["colPlus"].Index)
            {
                if (currentQty < rest)
                {
                    int newQty = currentQty + 1;

                    try
                    {
                        var batches = GetBatchesForShipment(productId, newQty);
                        decimal totalPrice = batches.Sum(b => b.batch.PurchasePrice * (1 - b.batch.DiscountPercent / 100) * b.take);

                        // ПРАВИЛЬНО: создаём объект ShipmentItemInfo
                        _shipmentItems[productId] = new ShipmentItemInfo
                        {
                            ProductId = productId,
                            ProductName = productName,
                            Quantity = newQty,
                            TotalPrice = totalPrice,
                            Batches = batches
                        };

                        dgvShipment.Rows[e.RowIndex].Cells["colQty"].Value = newQty.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show(AppResources.MsgNotEnough);
                }
            }
            else if (e.ColumnIndex == dgvShipment.Columns["colMinus"].Index)
            {
                if (currentQty > 0)
                {
                    int newQty = currentQty - 1;

                    if (newQty == 0)
                    {
                        _shipmentItems.Remove(productId);
                    }
                    else
                    {
                        var batches = GetBatchesForShipment(productId, newQty);
                        decimal totalPrice = batches.Sum(b => b.batch.PurchasePrice * (1 - b.batch.DiscountPercent / 100) * b.take);

                        // ПРАВИЛЬНО: создаём объект ShipmentItemInfo
                        _shipmentItems[productId] = new ShipmentItemInfo
                        {
                            ProductId = productId,
                            ProductName = productName,
                            Quantity = newQty,
                            TotalPrice = totalPrice,
                            Batches = batches
                        };
                    }

                    dgvShipment.Rows[e.RowIndex].Cells["colQty"].Value = newQty.ToString();
                }
            }

            UpdateTotal();
        }

        private void UpdateTotal()
        {
            int total = _shipmentItems.Values.Sum(i => i.Quantity);
            decimal totalAmount = _shipmentItems.Values.Sum(i => i.TotalPrice);

            lblTotal.Text = $"{AppResources.LblTotalItems}\n{total} шт.\nСумма: {CurrencyHelp.Format(totalAmount)}";
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            var clientName = txtClientName.Text.Trim();
            if (string.IsNullOrEmpty(clientName))
            {
                MessageBox.Show(AppResources.MsgClientEmpty);
                return;
            }

            if (_shipmentItems.Count == 0)
            {
                MessageBox.Show(AppResources.MsgNoItems);
                return;
            }

            using (var db = new SkladContext())
            {
                try
                {
                    decimal totalAmount = 0;
                    // Проверка остатков
                    foreach (var item in _shipmentItems.Values)
                    {
                        // Получаем актуальный остаток из БД
                        var available = db.StockBatches
                            .Where(b => b.ProductId == item.ProductId && !b.IsWrittenOff && b.Quantity > 0)
                            .Sum(b => b.Quantity);

                        if (available < item.Quantity)
                        {
                            // WARN-09: Финальная проверка остатков перед отгрузкой не пройдена
                            Logger.Warn($"WARN-09: Финальная проверка остатков перед отгрузкой не пройдена.\n" +
                                        $"Пользователь: {CurrentUser.User?.Login}\n" +
                                        $"ProductId: {item.ProductId} | Товар: {item.ProductName}\n" +
                                        $"Запрошено: {item.Quantity} | Актуальный остаток: {available}\n" +
                                        $"Отгрузка не создана.");
                            MessageBox.Show($"Недостаточно товара: {item.ProductName}\n" +
                                           $"Доступно: {available} шт., запрошено: {item.Quantity} шт.");
                            return;
                        }
                    }

                    // Создаём клиента
                    var foundClient = db.Clients.FirstOrDefault(c => c.Name == clientName);
                    if (foundClient == null)
                    {
                        foundClient = new Client { Id = Guid.NewGuid(), Name = clientName };
                        db.Clients.Add(foundClient);
                        db.SaveChanges();
                    }

                    // Создаём отгрузку
                    var newShipment = new Shipment
                    {
                        Id = Guid.NewGuid(),
                        ClientId = foundClient.Id,
                        UserId = CurrentUser.User.Id,
                        ShipmentDate = dtpDate.Value.ToUniversalTime()
                    };
                    db.Shipments.Add(newShipment);
                    db.SaveChanges();

                    Logger.Debug($"DEBUG-06: Отгрузка успешно создана.\n" +
                         $"Пользователь: {CurrentUser.User?.Login}\n" +
                         $"ShipmentId: {newShipment.Id} | Клиент: {clientName}\n" +
                         $"Дата: {dtpDate.Value:dd.MM.yyyy}\n" +
                         $"Позиций: {_shipmentItems.Count} | Сумма: {totalAmount:F2} руб.\n" +
                         $"Время: {DateTime.Now}");

                    // Сохраняем товары и списываем остатки
                    foreach (var item in _shipmentItems.Values)
                    {
                        foreach (var (batch, take) in item.Batches)
                        {
                            var currentBatch = db.StockBatches.Find(batch.Id);
                            currentBatch.Quantity -= take;

                            decimal priceWithDiscount = batch.PurchasePrice * (1 - batch.DiscountPercent / 100);

                            db.ShipmentItems.Add(new ShipmentItem
                            {
                                Id = Guid.NewGuid(),
                                ShipmentId = newShipment.Id,
                                ProductId = item.ProductId,
                                Quantity = take,
                                Amount = priceWithDiscount * take
                            });
                        }

                        // Обновляем общий остаток
                        var stock = db.Stocks.FirstOrDefault(s => s.ProductId == item.ProductId);
                        if (stock != null)
                        {
                            stock.Rest -= item.Quantity;
                        }
                    }

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger.Error($"ERROR-02: Не удалось сохранить отгрузку.\n" +
                                 $"Пользователь: {CurrentUser.User?.Login} | Роль: {CurrentUser.RoleName}\n" +
                                 $"Клиент: {clientName} | Дата: {dtpDate.Value:dd.MM.yyyy}\n" +
                                 $"Количество позиций: {_shipmentItems.Count}\n" +
                                 $"Исключение: {ex.GetType()} --- {ex.Message}\n" +
                                 $"Стек: {ex.StackTrace}", ex);
                    MessageBox.Show("Ошибка сохранения отгрузки", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            MessageBox.Show(AppResources.MsgShipSuccess);
            _shipmentItems.Clear();
            this.Close();
        }

        private List<(StockBatch batch, int take)> GetBatchesForShipment(Guid productId, int quantity)
        {
            using (var db = new SkladContext())
            {
                var batches = db.StockBatches
                    .Where(b => b.ProductId == productId && !b.IsWrittenOff && b.Quantity > 0)
                    .OrderBy(b => b.ExpiryDate)
                    .ToList();

                var result = new List<(StockBatch batch, int take)>();
                int remaining = quantity;

                foreach (var batch in batches)
                {
                    if (remaining <= 0) break;

                    int take = Math.Min(remaining, batch.Quantity);
                    result.Add((batch, take));
                    remaining -= take;
                }

                if (remaining > 0)
                {
                    throw new Exception($"Недостаточно товара. Не хватает {remaining} шт.");
                }

                return result;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _shipmentItems.Clear();
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            cmbCategory.SelectedIndex = 0;
            cmbAvailability.SelectedIndex = 0;
            LoadProducts();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void txtClientName_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvShipment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var idValue = dgvShipment.Rows[e.RowIndex].Cells["colId"].Value?.ToString();
            if (!Guid.TryParse(idValue, out Guid productId)) return;

            //Получаем остаток
            string restStr = dgvShipment.Rows[e.RowIndex].Cells["colRest"].Value?.ToString();
            int rest = int.TryParse(restStr, out int r) ? r : 0;

            string productName = dgvShipment.Rows[e.RowIndex].Cells["colName"].Value?.ToString();
            string article = dgvShipment.Rows[e.RowIndex].Cells["colArticle"].Value?.ToString();

            if (e.ColumnIndex == dgvShipment.Columns["colPlus"].Index)
            {
                //Получаем текущее количество
                int currentQty = _shipmentItems.ContainsKey(productId) ? _shipmentItems[productId].Quantity : 0;
                MessageBox.Show($"currentQty = {currentQty}, rest = {rest}");
                if (currentQty + 1 <= rest)
                {
                    int newQty = currentQty + 1;

                    try
                    {
                        //Получаем партии по FIFO
                        var batches = GetBatchesForShipment(productId, newQty);
                        decimal totalPrice = batches.Sum(b => b.batch.PurchasePrice * (1 - b.batch.DiscountPercent / 100) * b.take);

                        _shipmentItems[productId] = new ShipmentItemInfo
                        {
                            ProductId = productId,
                            ProductName = productName,
                            Quantity = newQty,
                            TotalPrice = totalPrice,
                            Batches = batches
                        };

                        dgvShipment.Rows[e.RowIndex].Cells["colQty"].Value = newQty.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    Logger.Warn($"WARN-03: Попытка отгрузить количество, превышающее остаток.\n" +
                        $"Пользователь: {CurrentUser.User?.Login}\n" +
                        $"ProductId: {productId} | Артикул: {article} | Товар: {productName}\n" +
                        $"Текущее количество: {currentQty} | Запрошено: {currentQty + 1} | Доступно: {rest}\n" +
                        $"Время: {DateTime.Now}");
                    MessageBox.Show(AppResources.MsgNotEnough);
                }
            }
            else if (e.ColumnIndex == dgvShipment.Columns["colMinus"].Index)
            {
                int currentQty = _shipmentItems.ContainsKey(productId) ? _shipmentItems[productId].Quantity : 0;

                if (currentQty > 0)
                {
                    int newQty = currentQty - 1;

                    if (newQty == 0)
                    {
                        _shipmentItems.Remove(productId);
                    }
                    else
                    {
                        try
                        {
                            var batches = GetBatchesForShipment(productId, newQty);
                            decimal totalPrice = batches.Sum(b => b.batch.PurchasePrice * (1 - b.batch.DiscountPercent / 100) * b.take);

                            _shipmentItems[productId] = new ShipmentItemInfo
                            {
                                ProductId = productId,
                                ProductName = productName,
                                Quantity = newQty,
                                TotalPrice = totalPrice,
                                Batches = batches
                            };
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                    dgvShipment.Rows[e.RowIndex].Cells["colQty"].Value = newQty.ToString();
                }
            }

            UpdateTotal();
        }
    }
}