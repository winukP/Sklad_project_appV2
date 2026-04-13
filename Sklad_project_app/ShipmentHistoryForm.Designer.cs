
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblShipments = new System.Windows.Forms.Label();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.lblItems = new System.Windows.Forms.Label();
            this.dgvHistoryItems = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistoryItems)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(10, 10);
            this.lblTitle.Size = new System.Drawing.Size(760, 25);
            this.lblTitle.Text = "История отгрузок";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblShipments
            this.lblShipments.AutoSize = true;
            this.lblShipments.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblShipments.Location = new System.Drawing.Point(10, 42);
            this.lblShipments.Text = "Список отгрузок:";

            // dgvHistory
            this.dgvHistory.Location = new System.Drawing.Point(10, 60);
            this.dgvHistory.Size = new System.Drawing.Size(760, 200);
            this.dgvHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvHistory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvHistory.RowHeadersVisible = false;
            this.dgvHistory.AllowUserToAddRows = false;
            this.dgvHistory.AllowUserToDeleteRows = false;
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistory.MultiSelect = false;
            this.dgvHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvHistory.EnableHeadersVisualStyles = false;
            this.dgvHistory.SelectionChanged += new System.EventHandler(this.dgvHistory_SelectionChanged);

            // lblItems
            this.lblItems.AutoSize = true;
            this.lblItems.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblItems.Location = new System.Drawing.Point(10, 272);
            this.lblItems.Text = "Товары в выбранной отгрузке:";

            // dgvHistoryItems
            this.dgvHistoryItems.Location = new System.Drawing.Point(10, 290);
            this.dgvHistoryItems.Size = new System.Drawing.Size(760, 200);
            this.dgvHistoryItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvHistoryItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvHistoryItems.RowHeadersVisible = false;
            this.dgvHistoryItems.AllowUserToAddRows = false;
            this.dgvHistoryItems.AllowUserToDeleteRows = false;
            this.dgvHistoryItems.ReadOnly = true;
            this.dgvHistoryItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistoryItems.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.dgvHistoryItems.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvHistoryItems.EnableHeadersVisualStyles = false;

            // ShipmentHistoryForm
            this.ClientSize = new System.Drawing.Size(780, 510);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblShipments);
            this.Controls.Add(this.dgvHistory);
            this.Controls.Add(this.lblItems);
            this.Controls.Add(this.dgvHistoryItems);
            this.Text = "История отгрузок";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.BackColor = System.Drawing.Color.White;
            this.Load += new System.EventHandler(this.ShipmentHistoryForm_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistoryItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblShipments;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.Label lblItems;
        private System.Windows.Forms.DataGridView dgvHistoryItems;
    }
}
