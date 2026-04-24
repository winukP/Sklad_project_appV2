
namespace Sklad_project_app
{
    partial class ShipmentHistoryForm
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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            lblTitle = new Label();
            lblShipments = new Label();
            dgvHistory = new DataGridView();
            lblItems = new Label();
            dgvHistoryItems = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvHistoryItems).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Arial", 12F, FontStyle.Bold);
            lblTitle.Location = new Point(10, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(760, 25);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "История отгрузок";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblShipments
            // 
            lblShipments.AutoSize = true;
            lblShipments.Font = new Font("Arial", 9F, FontStyle.Bold);
            lblShipments.Location = new Point(10, 42);
            lblShipments.Name = "lblShipments";
            lblShipments.Size = new Size(131, 18);
            lblShipments.TabIndex = 1;
            lblShipments.Text = "Список отгрузок:";
            // 
            // dgvHistory
            // 
            dgvHistory.AllowUserToAddRows = false;
            dgvHistory.AllowUserToDeleteRows = false;
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistory.BackgroundColor = Color.White;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(50, 50, 50);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvHistory.ColumnHeadersHeight = 29;
            dgvHistory.EnableHeadersVisualStyles = false;
            dgvHistory.Location = new Point(10, 60);
            dgvHistory.MultiSelect = false;
            dgvHistory.Name = "dgvHistory";
            dgvHistory.ReadOnly = true;
            dgvHistory.RowHeadersVisible = false;
            dgvHistory.RowHeadersWidth = 51;
            dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistory.Size = new Size(760, 200);
            dgvHistory.TabIndex = 2;
            dgvHistory.SelectionChanged += dgvHistory_SelectionChanged;
            // 
            // lblItems
            // 
            lblItems.AutoSize = true;
            lblItems.Font = new Font("Arial", 9F, FontStyle.Bold);
            lblItems.Location = new Point(10, 272);
            lblItems.Name = "lblItems";
            lblItems.Size = new Size(235, 18);
            lblItems.TabIndex = 3;
            lblItems.Text = "Товары в выбранной отгрузке:";
            // 
            // dgvHistoryItems
            // 
            dgvHistoryItems.AllowUserToAddRows = false;
            dgvHistoryItems.AllowUserToDeleteRows = false;
            dgvHistoryItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistoryItems.BackgroundColor = Color.White;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(50, 50, 50);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvHistoryItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvHistoryItems.ColumnHeadersHeight = 29;
            dgvHistoryItems.EnableHeadersVisualStyles = false;
            dgvHistoryItems.Location = new Point(10, 290);
            dgvHistoryItems.Name = "dgvHistoryItems";
            dgvHistoryItems.ReadOnly = true;
            dgvHistoryItems.RowHeadersVisible = false;
            dgvHistoryItems.RowHeadersWidth = 51;
            dgvHistoryItems.Size = new Size(760, 200);
            dgvHistoryItems.TabIndex = 4;
            // 
            // ShipmentHistoryForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(780, 510);
            Controls.Add(lblTitle);
            Controls.Add(lblShipments);
            Controls.Add(dgvHistory);
            Controls.Add(lblItems);
            Controls.Add(dgvHistoryItems);
            MaximizeBox = false;
            Name = "ShipmentHistoryForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "История отгрузок";
            Load += ShipmentHistoryForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvHistoryItems).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblShipments;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.Label lblItems;
        private System.Windows.Forms.DataGridView dgvHistoryItems;
    }
}
