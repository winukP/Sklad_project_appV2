using Sklad_project_app.Models;

namespace Sklad_project_app
{
    partial class AdminCatalogForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            panelTop = new Panel();
            lblCompany = new Label();
            lblUserInfo = new Label();
            btnLogout = new Button();
            panelLeft = new Panel();
            btnCatalog = new Button();
            btnCategories = new Button();
            btnHistory = new Button();
            panelActions = new Panel();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnView = new Button();
            btnRefresh = new Button();
            panelFilters = new Panel();
            lblSearch = new Label();
            txtSearch = new TextBox();
            lblCategory = new Label();
            cmbCategory = new ComboBox();
            lblAvailability = new Label();
            cmbAvailability = new ComboBox();
            lblPrice = new Label();
            txtPriceFrom = new TextBox();
            txtPriceTo = new TextBox();
            lblFound = new Label();
            btnReset = new Button();
            dgvProducts = new DataGridView();
            panelEdit = new Panel();
            lblPanelTitle = new Label();
            lblArticleEdit = new Label();
            txtArticleEdit = new TextBox();
            lblNameEdit = new Label();
            txtNameEdit = new TextBox();
            lblCategoryEdit = new Label();
            cmbCategoryEdit = new ComboBox();
            lblUnitEdit = new Label();
            cmbUnitEdit = new ComboBox();
            lblPriceEdit = new Label();
            txtPriceEdit = new TextBox();
            lblRestEdit = new Label();
            txtRestEdit = new TextBox();
            btnCancelEdit = new Button();
            btnSaveEdit = new Button();
            panelTop.SuspendLayout();
            panelLeft.SuspendLayout();
            panelActions.SuspendLayout();
            panelFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            panelEdit.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(30, 100, 200);
            panelTop.Controls.Add(lblCompany);
            panelTop.Controls.Add(lblUserInfo);
            panelTop.Controls.Add(btnLogout);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1100, 35);
            panelTop.TabIndex = 0;
            // 
            // lblCompany
            // 
            lblCompany.AutoSize = true;
            lblCompany.Font = new Font("Arial", 10F, FontStyle.Bold);
            lblCompany.ForeColor = Color.White;
            lblCompany.Location = new Point(10, 8);
            lblCompany.Name = "lblCompany";
            lblCompany.Size = new Size(463, 19);
            lblCompany.TabIndex = 0;
            lblCompany.Text = "ООО \"Птички-тупички\" - система управления складом";
            // 
            // lblUserInfo
            // 
            lblUserInfo.AutoSize = true;
            lblUserInfo.Font = new Font("Arial", 9F);
            lblUserInfo.ForeColor = Color.White;
            lblUserInfo.Location = new Point(700, 9);
            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Size = new Size(223, 17);
            lblUserInfo.TabIndex = 1;
            lblUserInfo.Text = "Администратор: Иван Давыдов";
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(210, 220, 235);
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Location = new Point(1018, 2);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(70, 30);
            btnLogout.TabIndex = 2;
            btnLogout.Text = "Выход";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // panelLeft
            // 
            panelLeft.BackColor = Color.FromArgb(240, 240, 240);
            panelLeft.BorderStyle = BorderStyle.FixedSingle;
            panelLeft.Controls.Add(btnCatalog);
            panelLeft.Controls.Add(btnCategories);
            panelLeft.Controls.Add(btnHistory);
            panelLeft.Location = new Point(0, 35);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(140, 565);
            panelLeft.TabIndex = 1;
            // 
            // btnCatalog
            // 
            btnCatalog.BackColor = Color.FromArgb(30, 100, 200);
            btnCatalog.FlatStyle = FlatStyle.Flat;
            btnCatalog.ForeColor = Color.White;
            btnCatalog.Location = new Point(5, 10);
            btnCatalog.Name = "btnCatalog";
            btnCatalog.Size = new Size(128, 30);
            btnCatalog.TabIndex = 0;
            btnCatalog.Text = "Каталог товаров";
            btnCatalog.UseVisualStyleBackColor = false;
            btnCatalog.Click += btnCatalog_Click;
            // 
            // btnCategories
            // 
            btnCategories.FlatStyle = FlatStyle.Flat;
            btnCategories.Location = new Point(5, 48);
            btnCategories.Name = "btnCategories";
            btnCategories.Size = new Size(128, 30);
            btnCategories.TabIndex = 1;
            btnCategories.Text = "Категории";
            btnCategories.Click += btnCategories_Click;
            // 
            // btnHistory
            // 
            btnHistory.FlatStyle = FlatStyle.Flat;
            btnHistory.Location = new Point(5, 86);
            btnHistory.Name = "btnHistory";
            btnHistory.Size = new Size(128, 30);
            btnHistory.TabIndex = 2;
            btnHistory.Text = "История отгрузок";
            btnHistory.Click += btnHistory_Click;
            // 
            // panelActions
            // 
            panelActions.BackColor = Color.FromArgb(50, 50, 50);
            panelActions.Controls.Add(btnAdd);
            panelActions.Controls.Add(btnEdit);
            panelActions.Controls.Add(btnDelete);
            panelActions.Controls.Add(btnView);
            panelActions.Controls.Add(btnRefresh);
            panelActions.Location = new Point(140, 35);
            panelActions.Name = "panelActions";
            panelActions.Size = new Size(960, 45);
            panelActions.TabIndex = 2;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(70, 70, 70);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(10, 8);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(100, 28);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.FromArgb(70, 70, 70);
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.ForeColor = Color.White;
            btnEdit.Location = new Point(118, 8);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(100, 28);
            btnEdit.TabIndex = 1;
            btnEdit.Text = "Изменить";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(70, 70, 70);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(226, 8);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 28);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnView
            // 
            btnView.BackColor = Color.FromArgb(70, 70, 70);
            btnView.FlatStyle = FlatStyle.Flat;
            btnView.ForeColor = Color.White;
            btnView.Location = new Point(334, 8);
            btnView.Name = "btnView";
            btnView.Size = new Size(128, 28);
            btnView.TabIndex = 3;
            btnView.Text = "Просмотреть";
            btnView.UseVisualStyleBackColor = false;
            btnView.Click += btnView_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(70, 70, 70);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(468, 8);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(100, 28);
            btnRefresh.TabIndex = 4;
            btnRefresh.Text = "Обновить";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // panelFilters
            // 
            panelFilters.BackColor = Color.FromArgb(230, 235, 245);
            panelFilters.Controls.Add(lblSearch);
            panelFilters.Controls.Add(txtSearch);
            panelFilters.Controls.Add(lblCategory);
            panelFilters.Controls.Add(cmbCategory);
            panelFilters.Controls.Add(lblAvailability);
            panelFilters.Controls.Add(cmbAvailability);
            panelFilters.Controls.Add(lblPrice);
            panelFilters.Controls.Add(txtPriceFrom);
            panelFilters.Controls.Add(txtPriceTo);
            panelFilters.Controls.Add(lblFound);
            panelFilters.Controls.Add(btnReset);
            panelFilters.Location = new Point(140, 80);
            panelFilters.Name = "panelFilters";
            panelFilters.Size = new Size(960, 113);
            panelFilters.TabIndex = 3;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Arial", 8F);
            lblSearch.Location = new Point(10, 8);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(188, 16);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Поиск (название / артикул):";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(10, 25);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(160, 27);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Font = new Font("Arial", 8F);
            lblCategory.Location = new Point(203, 6);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(79, 16);
            lblCategory.TabIndex = 2;
            lblCategory.Text = "Категория:";
            lblCategory.Click += btnCategories_Click;
            // 
            // cmbCategory
            // 
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.Location = new Point(203, 25);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(160, 28);
            cmbCategory.TabIndex = 3;
            cmbCategory.SelectedIndexChanged += cmbCategory_SelectedIndexChanged;
            // 
            // lblAvailability
            // 
            lblAvailability.AutoSize = true;
            lblAvailability.Font = new Font("Arial", 8F);
            lblAvailability.Location = new Point(378, 8);
            lblAvailability.Name = "lblAvailability";
            lblAvailability.Size = new Size(67, 16);
            lblAvailability.TabIndex = 4;
            lblAvailability.Text = "Наличие:";
            // 
            // cmbAvailability
            // 
            cmbAvailability.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAvailability.Location = new Point(378, 25);
            cmbAvailability.Name = "cmbAvailability";
            cmbAvailability.Size = new Size(130, 28);
            cmbAvailability.TabIndex = 5;
            cmbAvailability.SelectedIndexChanged += cmbAvailability_SelectedIndexChanged;
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Font = new Font("Arial", 8F);
            lblPrice.Location = new Point(523, 8);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(45, 16);
            lblPrice.TabIndex = 6;
            lblPrice.Text = "Цена:";
            // 
            // txtPriceFrom
            // 
            txtPriceFrom.Location = new Point(523, 25);
            txtPriceFrom.Name = "txtPriceFrom";
            txtPriceFrom.Size = new Size(70, 27);
            txtPriceFrom.TabIndex = 7;
            txtPriceFrom.Text = "0";
            // 
            // txtPriceTo
            // 
            txtPriceTo.Location = new Point(603, 25);
            txtPriceTo.Name = "txtPriceTo";
            txtPriceTo.Size = new Size(70, 27);
            txtPriceTo.TabIndex = 8;
            txtPriceTo.Text = "1000000";
            // 
            // lblFound
            // 
            lblFound.AutoSize = true;
            lblFound.Font = new Font("Arial", 9F, FontStyle.Bold);
            lblFound.Location = new Point(10, 75);
            lblFound.Name = "lblFound";
            lblFound.Size = new Size(121, 18);
            lblFound.TabIndex = 9;
            lblFound.Text = "Найдено: 0 из 0";
            // 
            // btnReset
            // 
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Location = new Point(137, 69);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(92, 29);
            btnReset.TabIndex = 10;
            btnReset.Text = "Сбросить";
            btnReset.Click += btnReset_Click;
            // 
            // dgvProducts
            // 
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.BackgroundColor = Color.White;
            dgvProducts.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(50, 50, 50);
            dataGridViewCellStyle2.Font = new Font("Arial", 9F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvProducts.ColumnHeadersHeight = 29;
            dgvProducts.EnableHeadersVisualStyles = false;
            dgvProducts.Location = new Point(140, 197);
            dgvProducts.MultiSelect = false;
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.RowHeadersVisible = false;
            dgvProducts.RowHeadersWidth = 51;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(960, 411);
            dgvProducts.TabIndex = 5;
            // 
            // panelEdit
            // 
            panelEdit.BackColor = Color.FromArgb(50, 50, 50);
            panelEdit.Controls.Add(lblPanelTitle);
            panelEdit.Controls.Add(lblArticleEdit);
            panelEdit.Controls.Add(txtArticleEdit);
            panelEdit.Controls.Add(lblNameEdit);
            panelEdit.Controls.Add(txtNameEdit);
            panelEdit.Controls.Add(lblCategoryEdit);
            panelEdit.Controls.Add(cmbCategoryEdit);
            panelEdit.Controls.Add(lblUnitEdit);
            panelEdit.Controls.Add(cmbUnitEdit);
            panelEdit.Controls.Add(lblPriceEdit);
            panelEdit.Controls.Add(txtPriceEdit);
            panelEdit.Controls.Add(lblRestEdit);
            panelEdit.Controls.Add(txtRestEdit);
            panelEdit.Controls.Add(btnCancelEdit);
            panelEdit.Controls.Add(btnSaveEdit);
            panelEdit.Location = new Point(0, 155);
            panelEdit.Name = "panelEdit";
            panelEdit.Size = new Size(140, 445);
            panelEdit.TabIndex = 4;
            panelEdit.Visible = false;
            // 
            // lblPanelTitle
            // 
            lblPanelTitle.Font = new Font("Arial", 8F, FontStyle.Bold);
            lblPanelTitle.ForeColor = Color.White;
            lblPanelTitle.Location = new Point(5, 8);
            lblPanelTitle.Name = "lblPanelTitle";
            lblPanelTitle.Size = new Size(130, 30);
            lblPanelTitle.TabIndex = 0;
            lblPanelTitle.Text = "ДОБАВЛЕНИЕ ТОВАРА";
            lblPanelTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblArticleEdit
            // 
            lblArticleEdit.AutoSize = true;
            lblArticleEdit.Font = new Font("Arial", 8F);
            lblArticleEdit.ForeColor = Color.White;
            lblArticleEdit.Location = new Point(5, 42);
            lblArticleEdit.Name = "lblArticleEdit";
            lblArticleEdit.Size = new Size(64, 16);
            lblArticleEdit.TabIndex = 1;
            lblArticleEdit.Text = "Артикул:";
            // 
            // txtArticleEdit
            // 
            txtArticleEdit.BackColor = Color.FromArgb(80, 80, 80);
            txtArticleEdit.BorderStyle = BorderStyle.FixedSingle;
            txtArticleEdit.ForeColor = Color.White;
            txtArticleEdit.Location = new Point(5, 58);
            txtArticleEdit.Name = "txtArticleEdit";
            txtArticleEdit.Size = new Size(130, 27);
            txtArticleEdit.TabIndex = 2;
            // 
            // lblNameEdit
            // 
            lblNameEdit.AutoSize = true;
            lblNameEdit.Font = new Font("Arial", 8F);
            lblNameEdit.ForeColor = Color.White;
            lblNameEdit.Location = new Point(5, 86);
            lblNameEdit.Name = "lblNameEdit";
            lblNameEdit.Size = new Size(109, 16);
            lblNameEdit.TabIndex = 3;
            lblNameEdit.Text = "Наименование:";
            // 
            // txtNameEdit
            // 
            txtNameEdit.BackColor = Color.FromArgb(80, 80, 80);
            txtNameEdit.BorderStyle = BorderStyle.FixedSingle;
            txtNameEdit.ForeColor = Color.White;
            txtNameEdit.Location = new Point(5, 102);
            txtNameEdit.Name = "txtNameEdit";
            txtNameEdit.Size = new Size(130, 27);
            txtNameEdit.TabIndex = 4;
            // 
            // lblCategoryEdit
            // 
            lblCategoryEdit.AutoSize = true;
            lblCategoryEdit.Font = new Font("Arial", 8F);
            lblCategoryEdit.ForeColor = Color.White;
            lblCategoryEdit.Location = new Point(5, 130);
            lblCategoryEdit.Name = "lblCategoryEdit";
            lblCategoryEdit.Size = new Size(79, 16);
            lblCategoryEdit.TabIndex = 5;
            lblCategoryEdit.Text = "Категория:";
            // 
            // cmbCategoryEdit
            // 
            cmbCategoryEdit.BackColor = Color.FromArgb(80, 80, 80);
            cmbCategoryEdit.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoryEdit.Location = new Point(5, 146);
            cmbCategoryEdit.Name = "cmbCategoryEdit";
            cmbCategoryEdit.Size = new Size(130, 28);
            cmbCategoryEdit.TabIndex = 6;
            // 
            // lblUnitEdit
            // 
            lblUnitEdit.AutoSize = true;
            lblUnitEdit.Font = new Font("Arial", 8F);
            lblUnitEdit.ForeColor = Color.White;
            lblUnitEdit.Location = new Point(5, 174);
            lblUnitEdit.Name = "lblUnitEdit";
            lblUnitEdit.Size = new Size(108, 16);
            lblUnitEdit.TabIndex = 7;
            lblUnitEdit.Text = "Ед. измерения:";
            // 
            // cmbUnitEdit
            // 
            cmbUnitEdit.BackColor = Color.FromArgb(80, 80, 80);
            cmbUnitEdit.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUnitEdit.Location = new Point(5, 190);
            cmbUnitEdit.Name = "cmbUnitEdit";
            cmbUnitEdit.Size = new Size(130, 28);
            cmbUnitEdit.TabIndex = 8;
            // 
            // lblPriceEdit
            // 
            lblPriceEdit.AutoSize = true;
            lblPriceEdit.Font = new Font("Arial", 8F);
            lblPriceEdit.ForeColor = Color.White;
            lblPriceEdit.Location = new Point(5, 218);
            lblPriceEdit.Name = "lblPriceEdit";
            lblPriceEdit.Size = new Size(98, 16);
            lblPriceEdit.TabIndex = 9;
            lblPriceEdit.Text = "Цена закупки:";
            // 
            // txtPriceEdit
            // 
            txtPriceEdit.BackColor = Color.FromArgb(80, 80, 80);
            txtPriceEdit.BorderStyle = BorderStyle.FixedSingle;
            txtPriceEdit.ForeColor = Color.White;
            txtPriceEdit.Location = new Point(5, 234);
            txtPriceEdit.Name = "txtPriceEdit";
            txtPriceEdit.Size = new Size(130, 27);
            txtPriceEdit.TabIndex = 10;
            // 
            // lblRestEdit
            // 
            lblRestEdit.AutoSize = true;
            lblRestEdit.Font = new Font("Arial", 8F);
            lblRestEdit.ForeColor = Color.White;
            lblRestEdit.Location = new Point(5, 262);
            lblRestEdit.Name = "lblRestEdit";
            lblRestEdit.Size = new Size(65, 16);
            lblRestEdit.TabIndex = 11;
            lblRestEdit.Text = "Остаток:";
            // 
            // txtRestEdit
            // 
            txtRestEdit.BackColor = Color.FromArgb(80, 80, 80);
            txtRestEdit.BorderStyle = BorderStyle.FixedSingle;
            txtRestEdit.ForeColor = Color.White;
            txtRestEdit.Location = new Point(5, 278);
            txtRestEdit.Name = "txtRestEdit";
            txtRestEdit.Size = new Size(130, 27);
            txtRestEdit.TabIndex = 12;
            // 
            // btnCancelEdit
            // 
            btnCancelEdit.BackColor = Color.FromArgb(80, 80, 80);
            btnCancelEdit.FlatStyle = FlatStyle.Flat;
            btnCancelEdit.ForeColor = Color.White;
            btnCancelEdit.Location = new Point(5, 315);
            btnCancelEdit.Name = "btnCancelEdit";
            btnCancelEdit.Size = new Size(130, 28);
            btnCancelEdit.TabIndex = 13;
            btnCancelEdit.Text = "ОТМЕНА";
            btnCancelEdit.UseVisualStyleBackColor = false;
            btnCancelEdit.Click += btnCancelEdit_Click;
            // 
            // btnSaveEdit
            // 
            btnSaveEdit.BackColor = Color.FromArgb(80, 80, 80);
            btnSaveEdit.FlatStyle = FlatStyle.Flat;
            btnSaveEdit.ForeColor = Color.White;
            btnSaveEdit.Location = new Point(5, 351);
            btnSaveEdit.Name = "btnSaveEdit";
            btnSaveEdit.Size = new Size(130, 28);
            btnSaveEdit.TabIndex = 14;
            btnSaveEdit.Text = "СОХРАНИТЬ";
            btnSaveEdit.UseVisualStyleBackColor = false;
            btnSaveEdit.Click += btnSaveEdit_Click;
            // 
            // AdminCatalogForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(1100, 600);
            Controls.Add(panelTop);
            Controls.Add(panelLeft);
            Controls.Add(panelActions);
            Controls.Add(panelFilters);
            Controls.Add(panelEdit);
            Controls.Add(dgvProducts);
            Name = "AdminCatalogForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Каталог товаров - Администратор";
            Load += AdminCatalogForm_Load;
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelLeft.ResumeLayout(false);
            panelActions.ResumeLayout(false);
            panelFilters.ResumeLayout(false);
            panelFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            panelEdit.ResumeLayout(false);
            panelEdit.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.Label lblUserInfo;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Button btnCatalog;
        private System.Windows.Forms.Button btnCategories;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panelFilters;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblAvailability;
        private System.Windows.Forms.ComboBox cmbAvailability;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtPriceFrom;
        private System.Windows.Forms.TextBox txtPriceTo;
        private System.Windows.Forms.Label lblFound;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.Panel panelEdit;
        private System.Windows.Forms.Label lblPanelTitle;
        private System.Windows.Forms.Label lblArticleEdit;
        private System.Windows.Forms.TextBox txtArticleEdit;
        private System.Windows.Forms.Label lblNameEdit;
        private System.Windows.Forms.TextBox txtNameEdit;
        private System.Windows.Forms.Label lblCategoryEdit;
        private System.Windows.Forms.ComboBox cmbCategoryEdit;
        private System.Windows.Forms.Label lblUnitEdit;
        private System.Windows.Forms.ComboBox cmbUnitEdit;
        private System.Windows.Forms.Label lblPriceEdit;
        private System.Windows.Forms.TextBox txtPriceEdit;
        private System.Windows.Forms.Label lblRestEdit;
        private System.Windows.Forms.TextBox txtRestEdit;
        private System.Windows.Forms.Button btnCancelEdit;
        private System.Windows.Forms.Button btnSaveEdit;

    }
}
