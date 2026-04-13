using Microsoft.EntityFrameworkCore;
using Sklad_project_app.Models;
using User = Sklad_project_app.Models.User;


namespace Sklad_project_app.Tests
{
    public static class TestDbHelper
    {
        public static SkladContext CreateInMemoryContext(string dbName = null)
        {
            if (dbName == null)
            {
                dbName = Guid.NewGuid().ToString();
            }

            var options = new DbContextOptionsBuilder<SkladContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            return new SkladContext(options);
        }

        public static void SeedTestData(SkladContext db)
        {
            var adminRole = new Role
            {
                Id = Guid.NewGuid(),
                RoleName = "Администратор"
            };

            var storekeeperRole = new Role
            {
                Id = Guid.NewGuid(),
                RoleName = "Кладовщик"
            };

            db.Roles.Add(adminRole);
            db.Roles.Add(storekeeperRole);

            var adminUser = new User
            {
                Id = Guid.NewGuid(),
                Name = "Иван",
                Surname = "Давыдов",
                Patronymic = "Петрович",
                Login = "Давыдов Иван Петрович",
                Password = PasswordHasher.HashPassword("admin123"),
                Role_id = adminRole.Id,
                Role = adminRole
            };

            var storekeeperUser = new User
            {
                Id = Guid.NewGuid(),
                Name = "Максим",
                Surname = "Фролов",
                Patronymic = "Сергеевич",
                Login = "Фролов Максим Сергеевич",
                Password = PasswordHasher.HashPassword("store123"),
                Role_id = storekeeperRole.Id,
                Role = storekeeperRole
            };

            db.Users.Add(adminUser);
            db.Users.Add(storekeeperUser);

            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Настольные игры"
            };

            db.Categories.Add(category);

            var unit = new Unit
            {
                Id = Guid.NewGuid(),
                Name = "шт"
            };

            db.Units.Add(unit);

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Article = "000001",
                Name = "Каркассон",
                CategoryId = category.Id,
                UnitId = unit.Id
            };

            db.Products.Add(product);

            var stock = new Stock
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                PurchasePrice = 1450,
                Rest = 14
            };

            db.Stocks.Add(stock);

            var client = new Client
            {
                Id = Guid.NewGuid(),
                Name = "ООО Ромашка"
            };

            db.Clients.Add(client);

            db.SaveChanges();
        }
    }
}