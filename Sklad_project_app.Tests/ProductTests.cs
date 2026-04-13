using Sklad_project_app.Models;


namespace Sklad_project_app.Tests
{
    [TestFixture]
    public class ProductTests
    {
        [Test]
        public void CanAddProductWithStock()
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                TestDbHelper.SeedTestData(db);

                var category = db.Categories.FirstOrDefault();
                var unit = db.Units.FirstOrDefault();

                Assert.IsNotNull(category);
                Assert.IsNotNull(unit);

                var newProduct = new Product
                {
                    Id = Guid.NewGuid(),
                    Article = "000002",
                    Name = "Румикуб",
                    CategoryId = category.Id,
                    UnitId = unit.Id
                };

                // Act
                db.Products.Add(newProduct);
                db.SaveChanges();

                var newStock = new Stock
                {
                    Id = Guid.NewGuid(),
                    ProductId = newProduct.Id,
                    PurchasePrice = 1350,
                    Rest = 11
                };

                db.Stocks.Add(newStock);
                db.SaveChanges();

                var allProducts = db.Products.ToList();
                var addedStock = db.Stocks
                    .Where(stock => stock.ProductId == newProduct.Id)
                    .FirstOrDefault();

                // Assert
                Assert.AreEqual(2, allProducts.Count);
                Assert.IsNotNull(addedStock);
                Assert.AreEqual(1350, addedStock.PurchasePrice);
                Assert.AreEqual(11, addedStock.Rest);
            }
        }

        [Test]
        public void StockReducesAfterShipment()
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                TestDbHelper.SeedTestData(db);

                var product = db.Products.FirstOrDefault();
                var stock = db.Stocks
                    .Where(stockItem => stockItem.ProductId == product.Id)
                    .FirstOrDefault();

                Assert.IsNotNull(stock);
                var initialRest = stock.Rest;
                int quantityToShip = 3;

                // Act
                stock.Rest -= quantityToShip;
                db.SaveChanges();

                var updatedStock = db.Stocks
                    .Where(stockItem => stockItem.ProductId == product.Id)
                    .FirstOrDefault();

                // Assert
                Assert.AreEqual(initialRest - quantityToShip, updatedStock.Rest);
            }
        }

        [Test]
        public void CannotShipMoreThanAvailable()
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                TestDbHelper.SeedTestData(db);

                var product = db.Products.FirstOrDefault();
                var stock = db.Stocks
                    .Where(stockItem => stockItem.ProductId == product.Id)
                    .FirstOrDefault();

                Assert.IsNotNull(stock);
                int quantityToShip = 999;

                // Act
                var canShip = stock.Rest >= quantityToShip;

                // Assert
                Assert.IsFalse(canShip);
            }
        }

        [Test]
        public void FilterByCategory_ReturnsCorrectProducts()
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                TestDbHelper.SeedTestData(db);

                var category = db.Categories.FirstOrDefault();
                Assert.IsNotNull(category);

                // Act
                var allProducts = db.Products.ToList();
                var filteredProducts = new System.Collections.Generic.List<Product>();

                foreach (var product in allProducts)
                {
                    if (product.CategoryId == category.Id)
                    {
                        filteredProducts.Add(product);
                    }
                }

                // Assert
                Assert.AreEqual(1, filteredProducts.Count);
                Assert.AreEqual("Каркассон", filteredProducts[0].Name);
            }
        }
    }
}
