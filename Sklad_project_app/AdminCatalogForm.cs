using Microsoft.EntityFrameworkCore;
using Sklad_project_app.Models;


namespace Sklad_project_app
{
    public partial class AdminCatalogForm : Form
    {
        private PanelMode _currentMode = PanelMode.None;
        private Guid _selectedProductId = Guid.Empty;

        public AdminCatalogForm()
        {
            InitializeComponent();
            this.Text = AppResources.CatalogTitle;
            lblUserInfo.Text = AppResources.LblAdmin
                + CurrentUser.User.Surname + " " + CurrentUser.User.Name;
        }

        private void AdminCatalogForm_Load(object sender, EventArgs e)
        {
            LoadCategoriesToFilter();
            LoadProducts();
            panelEdit.Visible = false;
            panelEdit.BringToFront();
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
                    .Include("Unit")
                    .Include("Stock")
                    .ToList();

                int totalCount = allProducts.Count;

                //поиск
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

                //категория
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

                //наличие
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

                //цена
                decimal priceFrom = 0;
                decimal priceTo = 100000;
                decimal.TryParse(txtPriceFrom.Text, out priceFrom);
                decimal.TryParse(txtPriceTo.Text, out priceTo);

                if (priceFrom < 0 || priceTo < 0)
                {
                    MessageBox.Show(AppResources.MsgNegativePrice, AppResources.MsgInputError,
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    if (priceFrom < 0)
                    {
                        priceFrom = 0;
                    }
                    if (priceTo < 0)
                    {
                        priceTo = 100000;
                    }
                }

                var afterPrice = new List<Product>();
                foreach (var product in afterAvailability)
                {
                    if (product.Stock != null &&
                        product.Stock.PurchasePrice >= priceFrom &&
                        product.Stock.PurchasePrice <= priceTo)
                    {
                        afterPrice.Add(product);
                    }
                }

                lblFound.Text = AppResources.LblFoundFormat + afterPrice.Count
                    + " " + AppResources.LblFoundOf + " " + totalCount;

                dgvProducts.Rows.Clear();
                dgvProducts.Columns.Clear();
                dgvProducts.Columns.Add("colArticle", AppResources.ColArticle);
                dgvProducts.Columns.Add("colName", AppResources.ColName);
                dgvProducts.Columns.Add("colCategory", AppResources.ColCategory);
                dgvProducts.Columns.Add("colUnit", AppResources.ColUnit);
                dgvProducts.Columns.Add("colPrice", AppResources.ColPrice);
                dgvProducts.Columns.Add("colRest", AppResources.ColRest);
                dgvProducts.Columns.Add("colId", "ID");
                dgvProducts.Columns["colId"].Visible = false;

                foreach (var product in afterPrice)
                {
                    var price = "—";
                    var rest = "—";
                    var categoryName = "";
                    var unitName = "";

                    if (product.Stock != null)
                    {
                        price = product.Stock.PurchasePrice.ToString("0") + " руб.";
                        rest = product.Stock.Rest.ToString();
                    }

                    if (product.Category != null)
                    {
                        categoryName = product.Category.Name;
                    }
                    if (product.Unit != null)
                    {
                        unitName = product.Unit.Name;
                    }

                    dgvProducts.Rows.Add(product.Article, product.Name,
                        categoryName, unitName, price, rest, product.Id);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _currentMode = PanelMode.Add;
            _selectedProductId = Guid.Empty;
            ClearEditPanel();
            LoadCategoriesToEditPanel();
            LoadUnitsToCmbUnit();
            lblPanelTitle.Text = AppResources.PanelAdd;
            panelEdit.Visible = true;
            panelEdit.BringToFront();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show(AppResources.MsgSelectEdit);
                return;
            }

            var selectedRow = dgvProducts.SelectedRows[0];
            var idValue = selectedRow.Cells["colId"].Value?.ToString();

            Guid parsedId;
            if (!Guid.TryParse(idValue, out parsedId))
            {
                MessageBox.Show(AppResources.MsgProductError);
                return;
            }

            _selectedProductId = parsedId;
            _currentMode = PanelMode.Edit;
            LoadCategoriesToEditPanel();
            LoadUnitsToCmbUnit();
            LoadProductToEditPanel(_selectedProductId);
            lblPanelTitle.Text = AppResources.PanelEdit;
            panelEdit.Visible = true;
            panelEdit.BringToFront();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show(AppResources.MsgSelectDelete);
                return;
            }

            var selectedRow = dgvProducts.SelectedRows[0];
            var idValue = selectedRow.Cells["colId"].Value?.ToString();

            Guid productId;
            if (!Guid.TryParse(idValue, out productId))
            {
                MessageBox.Show(AppResources.MsgProductError);
                return;
            }

            var result = MessageBox.Show(
                AppResources.MsgDeleteConfirm,
                AppResources.MsgDeleteTitle,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            using (var db = new SkladContext())
            {
                var foundStock = db.Stocks
                    .Where(stock => stock.ProductId == productId)
                    .FirstOrDefault();

                if (foundStock != null)
                {
                    db.Stocks.Remove(foundStock);
                }

                var foundProduct = db.Products.Find(productId);
                if (foundProduct != null)
                {
                    db.Products.Remove(foundProduct);
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

            LoadProducts();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show(AppResources.MsgSelectView);
                return;
            }

            var selectedRow = dgvProducts.SelectedRows[0];
            var idValue = selectedRow.Cells["colId"].Value?.ToString();

            Guid productId;
            if (!Guid.TryParse(idValue, out productId))
            {
                return;
            }

            _currentMode = PanelMode.View;
            LoadCategoriesToEditPanel();
            LoadUnitsToCmbUnit();
            LoadProductToEditPanel(productId);
            lblPanelTitle.Text = AppResources.PanelView;

            txtArticleEdit.ReadOnly = true;
            txtNameEdit.ReadOnly = true;
            cmbCategoryEdit.Enabled = false;
            cmbUnitEdit.Enabled = false;
            txtPriceEdit.ReadOnly = true;
            txtRestEdit.ReadOnly = true;
            btnSaveEdit.Visible = false;

            panelEdit.Visible = true;
            panelEdit.BringToFront();
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

        private void LoadCategoriesToEditPanel()
        {
            using (var db = new SkladContext())
            {
                var categories = db.Categories.ToList();
                cmbCategoryEdit.Items.Clear();
                foreach (var category in categories)
                {
                    cmbCategoryEdit.Items.Add(category.Name);
                }
            }
        }

        private void LoadUnitsToCmbUnit()
        {
            using (var db = new SkladContext())
            {
                var units = db.Units.ToList();
                cmbUnitEdit.Items.Clear();
                foreach (var unit in units)
                {
                    cmbUnitEdit.Items.Add(unit.Name);
                }
            }
        }

        private void LoadProductToEditPanel(Guid productId)
        {
            using (var db = new SkladContext())
            {
                var allProducts = db.Products
                    .Include("Category")
                    .Include("Unit")
                    .Include("Stock")
                    .ToList();

                Product foundProduct = null;
                foreach (var product in allProducts)
                {
                    if (product.Id == productId)
                    {
                        foundProduct = product;
                        break;
                    }
                }

                if (foundProduct == null)
                {
                    return;
                }

                txtArticleEdit.ReadOnly = false;
                txtNameEdit.ReadOnly = false;
                cmbCategoryEdit.Enabled = true;
                cmbUnitEdit.Enabled = true;
                txtPriceEdit.ReadOnly = false;
                txtRestEdit.ReadOnly = false;
                btnSaveEdit.Visible = true;

                txtArticleEdit.Text = foundProduct.Article;
                txtNameEdit.Text = foundProduct.Name;

                if (foundProduct.Category != null)
                {
                    cmbCategoryEdit.SelectedItem = foundProduct.Category.Name;
                }

                if (foundProduct.Unit != null)
                {
                    cmbUnitEdit.SelectedItem = foundProduct.Unit.Name;
                }

                if (foundProduct.Stock != null)
                {
                    txtPriceEdit.Text = foundProduct.Stock.PurchasePrice.ToString("0");
                    txtRestEdit.Text = foundProduct.Stock.Rest.ToString();
                }
                else
                {
                    txtPriceEdit.Text = "0";
                    txtRestEdit.Text = "0";
                }
            }
        }

        private void ClearEditPanel()
        {
            txtArticleEdit.Text = null;
            txtNameEdit.Text = null;

            if (cmbCategoryEdit.Items.Count > 0)
            {
                cmbCategoryEdit.SelectedIndex = 0;
            }

            if (cmbUnitEdit.Items.Count > 0)
            {
                cmbUnitEdit.SelectedIndex = 0;
            }

            txtPriceEdit.Text = null;
            txtRestEdit.Text = null;

            txtArticleEdit.ReadOnly = false;
            txtNameEdit.ReadOnly = false;
            cmbCategoryEdit.Enabled = true;
            cmbUnitEdit.Enabled = true;
            txtPriceEdit.ReadOnly = false;
            txtRestEdit.ReadOnly = false;
            btnSaveEdit.Visible = true;
        }

        private void btnSaveEdit_Click(object sender, EventArgs e)
        {
            var article = txtArticleEdit.Text.Trim();
            var name = txtNameEdit.Text.Trim();
            var categoryName = "";
            var unitName = "";

            if (cmbCategoryEdit.SelectedItem != null)
            {
                categoryName = cmbCategoryEdit.SelectedItem.ToString();
            }
            if (cmbUnitEdit.SelectedItem != null)
            {
                unitName = cmbUnitEdit.SelectedItem.ToString();
            }

            decimal price;
            int rest;

            if (!decimal.TryParse(txtPriceEdit.Text, out price))
            {
                MessageBox.Show(AppResources.MsgPriceError);
                return;
            }

            if (price < 0)
            {
                MessageBox.Show(AppResources.MsgNegativePrice, AppResources.MsgInputError,
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtRestEdit.Text, out rest))
            {
                MessageBox.Show(AppResources.MsgRestError);
                return;
            }
            if (string.IsNullOrEmpty(article) || string.IsNullOrEmpty(name)
                || string.IsNullOrEmpty(categoryName) || string.IsNullOrEmpty(unitName))
            {
                MessageBox.Show(AppResources.MsgFillFields);
                return;
            }

            using (var db = new SkladContext())
            {
                var foundCategory = db.Categories
                    .Where(category => category.Name == categoryName)
                    .FirstOrDefault();

                var foundUnit = db.Units
                    .Where(unit => unit.Name == unitName)
                    .FirstOrDefault();

                if (foundCategory == null || foundUnit == null)
                {
                    MessageBox.Show(AppResources.MsgCatUnitNotFound);
                    return;
                }

                if (_currentMode == PanelMode.Add)
                {
                    var newProduct = new Product
                    {
                        Id = Guid.NewGuid(),
                        Article = article,
                        Name = name,
                        CategoryId = foundCategory.Id,
                        UnitId = foundUnit.Id
                    };
                    db.Products.Add(newProduct);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(AppResources.MsgSaveError + ex.Message);
                        return;
                    }

                    var newStock = new Stock
                    {
                        Id = Guid.NewGuid(),
                        ProductId = newProduct.Id,
                        PurchasePrice = price,
                        Rest = rest
                    };
                    db.Stocks.Add(newStock);

                    try
                    {
                        db.SaveChanges();
                        MessageBox.Show(AppResources.MsgProductAdded);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(AppResources.MsgSaveError + ex.Message);
                        return;
                    }
                }
                else if (_currentMode == PanelMode.Edit)
                {
                    var foundProduct = db.Products.Find(_selectedProductId);
                    if (foundProduct == null)
                    {
                        return;
                    }

                    foundProduct.Article = article;
                    foundProduct.Name = name;
                    foundProduct.CategoryId = foundCategory.Id;
                    foundProduct.UnitId = foundUnit.Id;

                    var foundStock = db.Stocks
                        .Where(stock => stock.ProductId == _selectedProductId)
                        .FirstOrDefault();

                    if (foundStock == null)
                    {
                        foundStock = new Stock
                        {
                            Id = Guid.NewGuid(),
                            ProductId = _selectedProductId
                        };
                        db.Stocks.Add(foundStock);
                    }

                    foundStock.PurchasePrice = price;
                    foundStock.Rest = rest;

                    try
                    {
                        db.SaveChanges();
                        MessageBox.Show(AppResources.MsgProductUpdated);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(AppResources.MsgSaveError + ex.Message);
                        return;
                    }
                }
            }

            panelEdit.Visible = false;
            LoadProducts();
        }

        private void btnCancelEdit_Click(object sender, EventArgs e)
        {
            panelEdit.Visible = false;
        }


        private void btnCatalog_Click(object sender, EventArgs e)
        {
            panelEdit.Visible = false;
            LoadProducts();
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            var form = new CategoriesForm();
            form.ShowDialog();
            LoadCategoriesToFilter();
            LoadProducts();
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            var form = new ShipmentHistoryForm();
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

        private void cmbAvailability_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProducts();
        }
    }
}