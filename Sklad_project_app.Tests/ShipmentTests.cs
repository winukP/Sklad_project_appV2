using Sklad_project_app.Models;


namespace Sklad_project_app.Tests
{
    [TestFixture]
    public class ShipmentTests
    {
        [Test]
        public void CanCreateShipment()
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                TestDbHelper.SeedTestData(db);

                var client = db.Clients.FirstOrDefault();
                var user = db.Users.FirstOrDefault();
                var product = db.Products.FirstOrDefault();

                Assert.IsNotNull(client);
                Assert.IsNotNull(user);
                Assert.IsNotNull(product);

                var newShipment = new Shipment
                {
                    Id = Guid.NewGuid(),
                    ClientId = client.Id,
                    UserId = user.Id,
                    ShipmentDate = DateTime.Today
                };

                // Act
                db.Shipments.Add(newShipment);
                db.SaveChanges();

                var shipmentItem = new ShipmentItem
                {
                    Id = Guid.NewGuid(),
                    ShipmentId = newShipment.Id,
                    ProductId = product.Id,
                    Quantity = 2
                };

                db.ShipmentItems.Add(shipmentItem);

                var stock = db.Stocks
                    .Where(stockItem => stockItem.ProductId == product.Id)
                    .FirstOrDefault();

                if (stock != null)
                {
                    stock.Rest -= 2;
                }

                db.SaveChanges();

                var allShipments = db.Shipments.ToList();
                var allItems = db.ShipmentItems.ToList();
                var updatedStock = db.Stocks
                    .Where(stockItem => stockItem.ProductId == product.Id)
                    .FirstOrDefault();

                // Assert
                Assert.AreEqual(1, allShipments.Count);
                Assert.AreEqual(1, allItems.Count);
                Assert.AreEqual(12, updatedStock.Rest);
            }
        }

        [Test]
        public void CanGetShipmentsByUser()
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                TestDbHelper.SeedTestData(db);

                var client = db.Clients.FirstOrDefault();
                var user = db.Users.FirstOrDefault();

                var shipment1 = new Shipment
                {
                    Id = Guid.NewGuid(),
                    ClientId = client.Id,
                    UserId = user.Id,
                    ShipmentDate = DateTime.Today
                };

                var shipment2 = new Shipment
                {
                    Id = Guid.NewGuid(),
                    ClientId = client.Id,
                    UserId = user.Id,
                    ShipmentDate = DateTime.Today
                };

                db.Shipments.Add(shipment1);
                db.Shipments.Add(shipment2);
                db.SaveChanges();

                // Act
                var allShipments = db.Shipments.ToList();
                var userShipments = new System.Collections.Generic.List<Shipment>();

                foreach (var shipment in allShipments)
                {
                    if (shipment.UserId == user.Id)
                    {
                        userShipments.Add(shipment);
                    }
                }

                // Assert
                Assert.AreEqual(2, userShipments.Count);
            }
        }
    }
}