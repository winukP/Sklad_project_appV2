
namespace Sklad_project_app
{
    partial class ShipmentForm
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
            lblWarning = new Label();
            panelLeft = new Panel();
            lblShipTitle = new Label();
            lblClient = new Label();
            txtClientName = new TextBox();
            lblDate = new Label();
            dtpDate = new DateTimePicker();
            lblTotal = new Label();
            btnCancel = new Button();
            btnSubmit = new Button();
            panelActions = new Panel();
            btnRefresh = new Button();
            panelFilters = new Panel();
            lblSearch = new Label();
            txtSearch = new TextBox();
            lblCategory = new Label();
            cmbCategory = new ComboBox();
            lblAvailability = new Label();
            cmbAvailability = new ComboBox();
            lblFound = new Label();
            btnReset = new Button();
            dgvShipment = new DataGridView();
            panelTop.SuspendLayout();
            panelLeft.SuspendLayout();
            panelActions.SuspendLayout();
            panelFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvShipment).BeginInit();
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
            lblUserInfo.Location = new Point(608, 10);
            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Size = new Size(88, 17);
            lblUserInfo.TabIndex = 1;
            lblUserInfo.Text = "Кладовщик:";
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(210, 220, 235);
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Location = new Point(1018, 3);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(70, 30);
            btnLogout.TabIndex = 2;
            btnLogout.Text = "Выход";
            btnLogout.UseVisualStyleBackColor = false;
            // 
            // lblWarning
            // 
            lblWarning.AutoSize = true;
            lblWarning.Font = new Font("Arial", 9F, FontStyle.Bold);
            lblWarning.ForeColor = Color.Yellow;
            lblWarning.Location = new Point(289, 14);
            lblWarning.Name = "lblWarning";
            lblWarning.Size = new Size(409, 18);
            lblWarning.TabIndex = 3;
            lblWarning.Text = "Перед отгрузкой проверьте наличие товара на складе";
            // 
            // panelLeft
            // 
            panelLeft.BackColor = Color.FromArgb(50, 50, 50);
            panelLeft.Controls.Add(lblShipTitle);
            panelLeft.Controls.Add(lblClient);
            panelLeft.Controls.Add(txtClientName);
            panelLeft.Controls.Add(lblDate);
            panelLeft.Controls.Add(dtpDate);
            panelLeft.Controls.Add(lblTotal);
            panelLeft.Controls.Add(btnCancel);
            panelLeft.Controls.Add(btnSubmit);
            panelLeft.Location = new Point(0, 35);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(140, 565);
            panelLeft.TabIndex = 1;
            // 
            // lblShipTitle
            // 
            lblShipTitle.Font = new Font("Arial", 8F, FontStyle.Bold);
            lblShipTitle.ForeColor = Color.White;
            lblShipTitle.Location = new Point(5, 8);
            lblShipTitle.Name = "lblShipTitle";
            lblShipTitle.Size = new Size(130, 30);
            lblShipTitle.TabIndex = 0;
            lblShipTitle.Text = "ДАННЫЕ ОТГРУЗКИ";
            lblShipTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblClient
            // 
            lblClient.AutoSize = true;
            lblClient.Font = new Font("Arial", 8F);
            lblClient.ForeColor = Color.White;
            lblClient.Location = new Point(5, 45);
            lblClient.Name = "lblClient";
            lblClient.Size = new Size(133, 16);
            lblClient.TabIndex = 1;
            lblClient.Text = "Клиент (название):";
            // 
            // txtClientName
            // 
            txtClientName.BackColor = Color.FromArgb(80, 80, 80);
            txtClientName.BorderStyle = BorderStyle.FixedSingle;
            txtClientName.ForeColor = Color.White;
            txtClientName.Location = new Point(5, 62);
            txtClientName.Name = "txtClientName";
            txtClientName.Size = new Size(130, 27);
            txtClientName.TabIndex = 2;
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Arial", 8F);
            lblDate.ForeColor = Color.White;
            lblDate.Location = new Point(5, 92);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(102, 16);
            lblDate.TabIndex = 3;
            lblDate.Text = "Дата отгрузки:";
            // 
            // dtpDate
            // 
            dtpDate.Format = DateTimePickerFormat.Short;
            dtpDate.Location = new Point(5, 108);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(130, 27);
            dtpDate.TabIndex = 4;
            // 
            // lblTotal
            // 
            lblTotal.BorderStyle = BorderStyle.FixedSingle;
            lblTotal.Font = new Font("Arial", 9F, FontStyle.Bold);
            lblTotal.ForeColor = Color.White;
            lblTotal.Location = new Point(5, 145);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(130, 45);
            lblTotal.TabIndex = 5;
            lblTotal.Text = "ВЗЯТО ТОВАРОВ:\n0";
            lblTotal.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(80, 80, 80);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(5, 205);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(130, 30);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "ОТМЕНА";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSubmit
            // 
            btnSubmit.BackColor = Color.FromArgb(30, 100, 200);
            btnSubmit.FlatStyle = FlatStyle.Flat;
            btnSubmit.ForeColor = Color.White;
            btnSubmit.Location = new Point(5, 243);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(130, 30);
            btnSubmit.TabIndex = 7;
            btnSubmit.Text = "ОТПРАВИТЬ";
            btnSubmit.UseVisualStyleBackColor = false;
            btnSubmit.Click += btnSubmit_Click;
            // 
            // panelActions
            // 
            panelActions.BackColor = Color.FromArgb(50, 50, 50);
            panelActions.Controls.Add(btnRefresh);
            panelActions.Controls.Add(lblWarning);
            panelActions.Location = new Point(140, 35);
            panelActions.Name = "panelActions";
            panelActions.Size = new Size(960, 45);
            panelActions.TabIndex = 2;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(70, 70, 70);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(10, 8);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(100, 28);
            btnRefresh.TabIndex = 0;
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
            panelFilters.Controls.Add(lblFound);
            panelFilters.Controls.Add(btnReset);
            panelFilters.Location = new Point(140, 80);
            panelFilters.Name = "panelFilters";
            panelFilters.Size = new Size(960, 105);
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
            lblCategory.Location = new Point(185, 8);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(79, 16);
            lblCategory.TabIndex = 2;
            lblCategory.Text = "Категория:";
            // 
            // cmbCategory
            // 
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.Location = new Point(185, 25);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(160, 28);
            cmbCategory.TabIndex = 3;
            cmbCategory.SelectedIndexChanged += cmbCategory_SelectedIndexChanged;
            // 
            // lblAvailability
            // 
            lblAvailability.AutoSize = true;
            lblAvailability.Font = new Font("Arial", 8F);
            lblAvailability.Location = new Point(360, 8);
            lblAvailability.Name = "lblAvailability";
            lblAvailability.Size = new Size(67, 16);
            lblAvailability.TabIndex = 4;
            lblAvailability.Text = "Наличие:";
            // 
            // cmbAvailability
            // 
            cmbAvailability.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAvailability.Location = new Point(360, 25);
            cmbAvailability.Name = "cmbAvailability";
            cmbAvailability.Size = new Size(130, 28);
            cmbAvailability.TabIndex = 5;
            // 
            // lblFound
            // 
            lblFound.AutoSize = true;
            lblFound.Font = new Font("Arial", 9F, FontStyle.Bold);
            lblFound.Location = new Point(10, 68);
            lblFound.Name = "lblFound";
            lblFound.Size = new Size(121, 18);
            lblFound.TabIndex = 6;
            lblFound.Text = "Найдено: 0 из 0";
            // 
            // btnReset
            // 
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Location = new Point(137, 63);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(85, 31);
            btnReset.TabIndex = 7;
            btnReset.Text = "Сбросить";
            btnReset.Click += btnReset_Click;
            // 
            // dgvShipment
            // 
            dgvShipment.AllowUserToAddRows = false;
            dgvShipment.AllowUserToDeleteRows = false;
            dgvShipment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvShipment.BackgroundColor = Color.White;
            dgvShipment.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(50, 50, 50);
            dataGridViewCellStyle2.Font = new Font("Arial", 9F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvShipment.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvShipment.ColumnHeadersHeight = 29;
            dgvShipment.EnableHeadersVisualStyles = false;
            dgvShipment.Location = new Point(140, 155);
            dgvShipment.MultiSelect = false;
            dgvShipment.Name = "dgvShipment";
            dgvShipment.RowHeadersVisible = false;
            dgvShipment.RowHeadersWidth = 51;
            dgvShipment.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvShipment.Size = new Size(960, 445);
            dgvShipment.TabIndex = 4;
            dgvShipment.CellClick += dgvShipment_CellClick;
            // 
            // ShipmentForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(1100, 600);
            Controls.Add(panelTop);
            Controls.Add(panelLeft);
            Controls.Add(panelActions);
            Controls.Add(panelFilters);
            Controls.Add(dgvShipment);
            Name = "ShipmentForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Отгрузка - Кладовщик";
            Load += ShipmentForm_Load;
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelLeft.ResumeLayout(false);
            panelLeft.PerformLayout();
            panelActions.ResumeLayout(false);
            panelActions.PerformLayout();
            panelFilters.ResumeLayout(false);
            panelFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvShipment).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.Label lblUserInfo;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Label lblShipTitle;
        private System.Windows.Forms.Label lblClient;
        private System.Windows.Forms.TextBox txtClientName;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panelFilters;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblAvailability;
        private System.Windows.Forms.ComboBox cmbAvailability;
        private System.Windows.Forms.Label lblFound;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.DataGridView dgvShipment;
    }
}
