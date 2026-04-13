using Microsoft.EntityFrameworkCore;

namespace Sklad_project_app
{
    public partial class MyShipmentsForm : Form
    {
        public MyShipmentsForm()
        {
            InitializeComponent();
        }

        private void MyShipmentsForm_Load(object sender, EventArgs e)
        {
            LoadMyShipments();
        }

        private void LoadMyShipments()
        {
            using (var db = new SkladContext())
            {
                var currentUserId = CurrentUser.User.Id;

                var allShipments = db.Shipments
                    .Include("Client")
                    .Include("ShipmentItems")
                    .ToList();

                dgvMyShipments.Rows.Clear();
                dgvMyShipments.Columns.Clear();
                dgvMyShipments.Columns.Add("colId", "№");
                dgvMyShipments.Columns.Add("colClient", "Клиент");
                dgvMyShipments.Columns.Add("colDate", "Дата");
                dgvMyShipments.Columns.Add("colItems", "Товаров");

                foreach (var shipment in allShipments)
                {
                    if (shipment.UserId != currentUserId)
                    {
                        continue;
                    }

                    var clientName = "—";
                    var date = "—";
                    int itemCount = 0;

                    if (shipment.Client != null)
                    {
                        clientName = shipment.Client.Name;
                    }
                    if (shipment.ShipmentDate != null)
                    {
                        date = shipment.ShipmentDate.Value.ToString("dd.MM.yyyy");
                    }
                    if (shipment.ShipmentItems != null)
                    {
                        itemCount = shipment.ShipmentItems.Count;
                    }

                    dgvMyShipments.Rows.Add(shipment.Id, clientName, date, itemCount);
                }
            }
        }
    }
}