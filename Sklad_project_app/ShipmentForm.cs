using Microsoft.EntityFrameworkCore;
using Sklad_project_app.Models;
using Sklad_project_app;

namespace Sklad_project_app
{
    public partial class ShipmentForm : Form
    {
        private Dictionary<Guid, int> _shipmentItems = new Dictionary<Guid, int>();

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

        private void LoadProducts()
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
                dgvShipment.Columns.Add("colPrice", AppResources.ColPrice);
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
                    var rest = "0";
                    var categoryName = "";

                    if (product.Stock != null)
                    {
                        price = product.Stock.PurchasePrice.ToString("0") + " руб.";
                        rest = product.Stock.Rest.ToString();
                    }

                    if (product.Category != null)
                    {
                        categoryName = product.Category.Name;
                    }

                    int qty = 0;
                    if (_shipmentItems.ContainsKey(product.Id))
                    {
                        qty = _shipmentItems[product.Id];
                    }

                    dgvShipment.Rows.Add(product.Article, product.Name,
                        categoryName, price, rest, "-", qty.ToString(), "+", product.Id);
                }

                UpdateTotal();
            }
        }

        private void dgvShipment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            var idValue = dgvShipment.Rows[e.RowIndex].Cells["colId"].Value?.ToString();
            Guid productId;
            if (!Guid.TryParse(idValue, out productId))
            {
                return;
            }

            int rest;
            if (!int.TryParse(dgvShipment.Rows[e.RowIndex].Cells["colRest"].Value?.ToString(), out rest))
            {
                rest = 0;
            }

            int currentQty = 0;
            if (_shipmentItems.ContainsKey(productId))
            {
                currentQty = _shipmentItems[productId];
            }

            if (e.ColumnIndex == dgvShipment.Columns["colPlus"].Index)
            {
                if (currentQty < rest)
                {
                    _shipmentItems[productId] = currentQty + 1;
                    dgvShipment.Rows[e.RowIndex].Cells["colQty"].Value =
                        _shipmentItems[productId].ToString();
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
                    _shipmentItems[productId] = currentQty - 1;
                    dgvShipment.Rows[e.RowIndex].Cells["colQty"].Value =
                        _shipmentItems[productId].ToString();
                }
            }

            UpdateTotal();
        }

        private void UpdateTotal()
        {
            int total = 0;
            foreach (var item in _shipmentItems)
            {
                total += item.Value;
            }
            lblTotal.Text = AppResources.LblTotalItems + "\n" + total.ToString();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            var clientName = txtClientName.Text.Trim();
            if (string.IsNullOrEmpty(clientName))
            {
                MessageBox.Show(AppResources.MsgClientEmpty);
                return;
            }

            var itemsToShip = new Dictionary<Guid, int>();
            foreach (var item in _shipmentItems)
            {
                if (item.Value > 0)
                {
                    itemsToShip.Add(item.Key, item.Value);
                }
            }

            if (itemsToShip.Count == 0)
            {
                MessageBox.Show(AppResources.MsgNoItems);
                return;
            }

            using (var db = new SkladContext())
            {
                foreach (var item in itemsToShip)
                {
                    var foundStock = db.Stocks
                        .Where(stock => stock.ProductId == item.Key)
                        .FirstOrDefault();

                    if (foundStock == null || foundStock.Rest < item.Value)
                    {
                        var foundProduct = db.Products.Find(item.Key);
                        var productName = "";
                        if (foundProduct != null)
                        {
                            productName = foundProduct.Name;
                        }
                        MessageBox.Show(AppResources.MsgNotEnough + " " + productName);
                        return;
                    }
                }

                var foundClient = db.Clients
                    .Where(client => client.Name == clientName)
                    .FirstOrDefault();

                if (foundClient == null)
                {
                    foundClient = new Client
                    {
                        Id = Guid.NewGuid(),
                        Name = clientName
                    };
                    db.Clients.Add(foundClient);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(AppResources.MsgSaveError + ex.Message);
                        return;
                    }
                }

                var newShipment = new Shipment
                {
                    Id = Guid.NewGuid(),
                    ClientId = foundClient.Id,
                    UserId = CurrentUser.User.Id,
                    ShipmentDate = dtpDate.Value.ToUniversalTime()
                };
                db.Shipments.Add(newShipment);

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(AppResources.MsgSaveError + ex.Message);
                    return;
                }

                foreach (var item in itemsToShip)
                {
                    db.ShipmentItems.Add(new ShipmentItem
                    {
                        Id = Guid.NewGuid(),
                        ShipmentId = newShipment.Id,
                        ProductId = item.Key,
                        Quantity = item.Value
                    });

                    var foundStock = db.Stocks
                        .Where(stock => stock.ProductId == item.Key)
                        .FirstOrDefault();

                    if (foundStock != null)
                    {
                        foundStock.Rest -= item.Value;
                    }
                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(AppResources.MsgSaveError + ex.Message);
                    return;
                }
            }

            MessageBox.Show(AppResources.MsgShipSuccess);
            _shipmentItems.Clear();
            this.Close();
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
    }
}