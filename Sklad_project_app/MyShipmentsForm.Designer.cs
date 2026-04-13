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
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvMyShipments = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyShipments)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(10, 10);
            this.lblTitle.Size = new System.Drawing.Size(560, 25);
            this.lblTitle.Text = "Мои отгрузки";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // dgvMyShipments
            this.dgvMyShipments.Location = new System.Drawing.Point(10, 45);
            this.dgvMyShipments.Size = new System.Drawing.Size(560, 400);
            this.dgvMyShipments.BackgroundColor = System.Drawing.Color.White;
            this.dgvMyShipments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvMyShipments.RowHeadersVisible = false;
            this.dgvMyShipments.AllowUserToAddRows = false;
            this.dgvMyShipments.AllowUserToDeleteRows = false;
            this.dgvMyShipments.ReadOnly = true;
            this.dgvMyShipments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMyShipments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMyShipments.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.dgvMyShipments.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvMyShipments.EnableHeadersVisualStyles = false;

            // MyShipmentsForm
            this.ClientSize = new System.Drawing.Size(580, 460);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dgvMyShipments);
            this.Text = "Мои отгрузки";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.BackColor = System.Drawing.Color.White;
            this.Load += new System.EventHandler(this.MyShipmentsForm_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvMyShipments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvMyShipments;
    }
}