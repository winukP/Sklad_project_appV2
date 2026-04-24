namespace Sklad_project_app
{
    partial class MyShipmentsForm
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            lblTitle = new Label();
            dgvMyShipments = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvMyShipments).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Arial", 12F, FontStyle.Bold);
            lblTitle.Location = new Point(10, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(560, 25);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Мои отгрузки";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dgvMyShipments
            // 
            dgvMyShipments.AllowUserToAddRows = false;
            dgvMyShipments.AllowUserToDeleteRows = false;
            dgvMyShipments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMyShipments.BackgroundColor = Color.White;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(50, 50, 50);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvMyShipments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvMyShipments.ColumnHeadersHeight = 29;
            dgvMyShipments.EnableHeadersVisualStyles = false;
            dgvMyShipments.Location = new Point(10, 45);
            dgvMyShipments.Name = "dgvMyShipments";
            dgvMyShipments.ReadOnly = true;
            dgvMyShipments.RowHeadersVisible = false;
            dgvMyShipments.RowHeadersWidth = 51;
            dgvMyShipments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMyShipments.Size = new Size(560, 400);
            dgvMyShipments.TabIndex = 1;
            // 
            // MyShipmentsForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(580, 460);
            Controls.Add(lblTitle);
            Controls.Add(dgvMyShipments);
            MaximizeBox = false;
            Name = "MyShipmentsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Мои отгрузки";
            Load += MyShipmentsForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvMyShipments).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvMyShipments;
    }
}