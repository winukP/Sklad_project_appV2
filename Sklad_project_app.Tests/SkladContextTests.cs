using Sklad_project_app.Models;


namespace Sklad_project_app.Tests
{
    [TestFixture]
    public class SkladContextTests
    {
        [Test]
        public void CanAddAndGetRole()
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                var newRole = new Role
                {
                    Id = Guid.NewGuid(),
                    RoleName = "Администратор"
                };

                // Act
                db.Roles.Add(newRole);
                db.SaveChanges();

                var allRoles = db.Roles.ToList();

                // Assert
                Assert.AreEqual(1, allRoles.Count);
                Assert.AreEqual("Администратор", allRoles[0].RoleName);
            }
        }

        [Test]
        public void CanAddAndGetUser()
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                TestDbHelper.SeedTestData(db);

                // Act
                var allUsers = db.Users.ToList();

                // Assert
                Assert.AreEqual(2, allUsers.Count);
            }
        }

        [Test]
        public void CanAddAndGetProduct()
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                TestDbHelper.SeedTestData(db);

                // Act
                var allProducts = db.Products.ToList();

                // Assert
                Assert.AreEqual(1, allProducts.Count);
                Assert.AreEqual("Каркассон", allProducts[0].Name);
            }
        }

        [Test]
        public void CanDeleteProduct()
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                TestDbHelper.SeedTestData(db);

                var productToDelete = db.Products.FirstOrDefault();
                Assert.IsNotNull(productToDelete);

                // Act
                var stockToDelete = db.Stocks
                    .Where(stock => stock.ProductId == productToDelete.Id)
                    .FirstOrDefault();

                if (stockToDelete != null)
                {
                    db.Stocks.Remove(stockToDelete);
                }

                db.Products.Remove(productToDelete);
                db.SaveChanges();

                var remainingProducts = db.Products.ToList();

                // Assert
                Assert.AreEqual(0, remainingProducts.Count);
            }
        }

        [Test]
        public void CanUpdateProduct()
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                TestDbHelper.SeedTestData(db);

                var productToUpdate = db.Products.FirstOrDefault();
                Assert.IsNotNull(productToUpdate);

                // Act
                productToUpdate.Name = "Новое название";
                db.SaveChanges();

                var updatedProduct = db.Products.FirstOrDefault();

                // Assert
                Assert.AreEqual("Новое название", updatedProduct.Name);
            }
        }
    }
}