namespace Sklad_project_app
{
    partial class CategoriesForm
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
            lblTitle = new Label();
            lstCategories = new ListBox();
            txtCategoryName = new TextBox();
            btnAddCat = new Button();
            btnEditCat = new Button();
            btnDeleteCat = new Button();
            lblInstruction = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Arial", 12F, FontStyle.Bold);
            lblTitle.Location = new Point(10, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(360, 25);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Управление категориями";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lstCategories
            // 
            lstCategories.Font = new Font("Arial", 10F);
            lstCategories.Location = new Point(10, 45);
            lstCategories.Name = "lstCategories";
            lstCategories.Size = new Size(360, 194);
            lstCategories.TabIndex = 1;
            lstCategories.SelectedIndexChanged += lstCategories_SelectedIndexChanged;
            // 
            // txtCategoryName
            // 
            txtCategoryName.Font = new Font("Arial", 10F);
            txtCategoryName.Location = new Point(10, 272);
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.Size = new Size(360, 27);
            txtCategoryName.TabIndex = 3;
            // 
            // btnAddCat
            // 
            btnAddCat.BackColor = Color.FromArgb(30, 100, 200);
            btnAddCat.FlatStyle = FlatStyle.Flat;
            btnAddCat.ForeColor = Color.White;
            btnAddCat.Location = new Point(10, 305);
            btnAddCat.Name = "btnAddCat";
            btnAddCat.Size = new Size(110, 30);
            btnAddCat.TabIndex = 4;
            btnAddCat.Text = "Добавить";
            btnAddCat.UseVisualStyleBackColor = false;
            btnAddCat.Click += btnAddCat_Click;
            // 
            // btnEditCat
            // 
            btnEditCat.FlatStyle = FlatStyle.Flat;
            btnEditCat.Location = new Point(130, 305);
            btnEditCat.Name = "btnEditCat";
            btnEditCat.Size = new Size(110, 30);
            btnEditCat.TabIndex = 5;
            btnEditCat.Text = "Изменить";
            btnEditCat.Click += btnEditCat_Click;
            // 
            // btnDeleteCat
            // 
            btnDeleteCat.BackColor = Color.FromArgb(200, 50, 50);
            btnDeleteCat.FlatStyle = FlatStyle.Flat;
            btnDeleteCat.ForeColor = Color.White;
            btnDeleteCat.Location = new Point(250, 305);
            btnDeleteCat.Name = "btnDeleteCat";
            btnDeleteCat.Size = new Size(120, 30);
            btnDeleteCat.TabIndex = 6;
            btnDeleteCat.Text = "Удалить";
            btnDeleteCat.UseVisualStyleBackColor = false;
            btnDeleteCat.Click += btnDeleteCat_Click;
            // 
            // lblInstruction
            // 
            lblInstruction.AutoSize = true;
            lblInstruction.Font = new Font("Arial", 8F);
            lblInstruction.Location = new Point(10, 255);
            lblInstruction.Name = "lblInstruction";
            lblInstruction.Size = new Size(143, 16);
            lblInstruction.TabIndex = 2;
            lblInstruction.Text = "Название категории:";
            // 
            // CategoriesForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(380, 350);
            Controls.Add(lblTitle);
            Controls.Add(lstCategories);
            Controls.Add(lblInstruction);
            Controls.Add(txtCategoryName);
            Controls.Add(btnAddCat);
            Controls.Add(btnEditCat);
            Controls.Add(btnDeleteCat);
            MaximizeBox = false;
            Name = "CategoriesForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Категории";
            Load += CategoriesForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ListBox lstCategories;
        private System.Windows.Forms.TextBox txtCategoryName;
        private System.Windows.Forms.Button btnAddCat;
        private System.Windows.Forms.Button btnEditCat;
        private System.Windows.Forms.Button btnDeleteCat;
        private System.Windows.Forms.Label lblInstruction;
    }
}