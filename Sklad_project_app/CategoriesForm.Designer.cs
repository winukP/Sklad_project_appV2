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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lstCategories = new System.Windows.Forms.ListBox();
            this.txtCategoryName = new System.Windows.Forms.TextBox();
            this.btnAddCat = new System.Windows.Forms.Button();
            this.btnEditCat = new System.Windows.Forms.Button();
            this.btnDeleteCat = new System.Windows.Forms.Button();
            this.lblInstruction = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(10, 10);
            this.lblTitle.Size = new System.Drawing.Size(360, 25);
            this.lblTitle.Text = "Управление категориями";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lstCategories
            this.lstCategories.Location = new System.Drawing.Point(10, 45);
            this.lstCategories.Size = new System.Drawing.Size(360, 200);
            this.lstCategories.Font = new System.Drawing.Font("Arial", 10F);
            this.lstCategories.SelectedIndexChanged += new System.EventHandler(this.lstCategories_SelectedIndexChanged);

            // lblInstruction
            this.lblInstruction.AutoSize = true;
            this.lblInstruction.Font = new System.Drawing.Font("Arial", 8F);
            this.lblInstruction.Location = new System.Drawing.Point(10, 255);
            this.lblInstruction.Text = "Название категории:";

            // txtCategoryName
            this.txtCategoryName.Location = new System.Drawing.Point(10, 272);
            this.txtCategoryName.Size = new System.Drawing.Size(360, 22);
            this.txtCategoryName.Font = new System.Drawing.Font("Arial", 10F);

            // btnAddCat
            this.btnAddCat.Location = new System.Drawing.Point(10, 305);
            this.btnAddCat.Size = new System.Drawing.Size(110, 30);
            this.btnAddCat.Text = "Добавить";
            this.btnAddCat.BackColor = System.Drawing.Color.FromArgb(30, 100, 200);
            this.btnAddCat.ForeColor = System.Drawing.Color.White;
            this.btnAddCat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCat.Click += new System.EventHandler(this.btnAddCat_Click);

            // btnEditCat
            this.btnEditCat.Location = new System.Drawing.Point(130, 305);
            this.btnEditCat.Size = new System.Drawing.Size(110, 30);
            this.btnEditCat.Text = "Изменить";
            this.btnEditCat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditCat.Click += new System.EventHandler(this.btnEditCat_Click);

            // btnDeleteCat
            this.btnDeleteCat.Location = new System.Drawing.Point(250, 305);
            this.btnDeleteCat.Size = new System.Drawing.Size(120, 30);
            this.btnDeleteCat.Text = "Удалить";
            this.btnDeleteCat.BackColor = System.Drawing.Color.FromArgb(200, 50, 50);
            this.btnDeleteCat.ForeColor = System.Drawing.Color.White;
            this.btnDeleteCat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteCat.Click += new System.EventHandler(this.btnDeleteCat_Click);

            // CategoriesForm
            this.ClientSize = new System.Drawing.Size(380, 350);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lstCategories);
            this.Controls.Add(this.lblInstruction);
            this.Controls.Add(this.txtCategoryName);
            this.Controls.Add(this.btnAddCat);
            this.Controls.Add(this.btnEditCat);
            this.Controls.Add(this.btnDeleteCat);
            this.Text = "Категории";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.BackColor = System.Drawing.Color.White;
            this.Load += new System.EventHandler(this.CategoriesForm_Load);

            this.ResumeLayout(false);
            this.PerformLayout();
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