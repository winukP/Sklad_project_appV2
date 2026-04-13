using Sklad_project_app.Models;


namespace Sklad_project_app.Tests
{
    [TestFixture]
    public class CategoryTests
    {
        [Test]
        public void CanAddCategory()
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                var newCategory = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Настольные игры"
                };

                // Act
                db.Categories.Add(newCategory);
                db.SaveChanges();

                var allCategories = db.Categories.ToList();

                // Assert
                Assert.AreEqual(1, allCategories.Count);
                Assert.AreEqual("Настольные игры", allCategories[0].Name);
            }
        }

        [Test]
        public void CanEditCategory()
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                TestDbHelper.SeedTestData(db);

                var categoryToEdit = db.Categories.FirstOrDefault();
                Assert.IsNotNull(categoryToEdit);

                // Act
                categoryToEdit.Name = "Карточные игры";
                db.SaveChanges();

                var updatedCategory = db.Categories.FirstOrDefault();

                // Assert
                Assert.AreEqual("Карточные игры", updatedCategory.Name);
            }
        }

        [Test]
        public void CannotDeleteCategoryWithProducts()
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                TestDbHelper.SeedTestData(db);

                var category = db.Categories.FirstOrDefault();
                Assert.IsNotNull(category);

                // Act
                var hasProducts = false;
                var allProducts = db.Products.ToList();

                foreach (var product in allProducts)
                {
                    if (product.CategoryId == category.Id)
                    {
                        hasProducts = true;
                        break;
                    }
                }

                // Assert
                Assert.IsTrue(hasProducts);
            }
        }

        [Test]
        public void CanDeleteEmptyCategory()
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                var emptyCategory = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Пустая категория"
                };

                db.Categories.Add(emptyCategory);
                db.SaveChanges();

                // Act
                var hasProducts = false;
                var allProducts = db.Products.ToList();

                foreach (var product in allProducts)
                {
                    if (product.CategoryId == emptyCategory.Id)
                    {
                        hasProducts = true;
                        break;
                    }
                }

                if (!hasProducts)
                {
                    db.Categories.Remove(emptyCategory);
                    db.SaveChanges();
                }

                var remainingCategories = db.Categories.ToList();

                // Assert
                Assert.AreEqual(0, remainingCategories.Count);
            }
        }
    }
}
