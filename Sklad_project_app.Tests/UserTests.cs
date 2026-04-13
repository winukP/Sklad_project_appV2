using Sklad_project_app.Models;


namespace Sklad_project_app.Tests
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void CanRegisterNewUser()
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                TestDbHelper.SeedTestData(db);

                var adminRole = db.Roles
                    .Where(role => role.RoleName == "Администратор")
                    .FirstOrDefault();

                Assert.IsNotNull(adminRole);

                var newUser = new User
                {
                    Id = Guid.NewGuid(),
                    Name = "Сергей",
                    Surname = "Иванов",
                    Patronymic = "Петрович",
                    Login = "Иванов Сергей Петрович",
                    Password = PasswordHasher.HashPassword("newpass"),
                    Role_id = adminRole.Id
                };

                // Act
                db.Users.Add(newUser);
                db.SaveChanges();

                var allUsers = db.Users.ToList();

                // Assert
                Assert.AreEqual(3, allUsers.Count);
            }
        }

        [Test]
        public void CanFindUserByLogin()
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                TestDbHelper.SeedTestData(db);

                // Act
                var allUsers = db.Users.ToList();
                User foundUser = null;

                foreach (var user in allUsers)
                {
                    var userLogin = "";
                    if (user.Login != null)
                    {
                        userLogin = user.Login.Trim();
                    }

                    if (userLogin == "Давыдов Иван Петрович")
                    {
                        foundUser = user;
                        break;
                    }
                }

                // Assert
                Assert.IsNotNull(foundUser);
                Assert.AreEqual("Иван", foundUser.Name);
            }
        }

        [Test]
        public void LoginWithCorrectPasswordSucceeds()
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                TestDbHelper.SeedTestData(db);

                var login = "Давыдов Иван Петрович";
                var password = "admin123";

                // Act
                var allUsers = db.Users.ToList();
                User foundUser = null;

                foreach (var user in allUsers)
                {
                    var userLogin = "";
                    if (user.Login != null)
                    {
                        userLogin = user.Login.Trim();
                    }

                    if (userLogin != login)
                    {
                        continue;
                    }

                    var storedHash = "";
                    if (user.Password != null)
                    {
                        storedHash = user.Password.Trim();
                    }

                    if (PasswordHasher.VerifyPassword(password, storedHash))
                    {
                        foundUser = user;
                        break;
                    }
                }

                // Assert
                Assert.IsNotNull(foundUser);
            }
        }

        [Test]
        public void LoginWithWrongPasswordFails()
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                TestDbHelper.SeedTestData(db);

                var login = "Давыдов Иван Петрович";
                var wrongPassword = "wrongpassword";

                // Act
                var allUsers = db.Users.ToList();
                User foundUser = null;

                foreach (var user in allUsers)
                {
                    var userLogin = "";
                    if (user.Login != null)
                    {
                        userLogin = user.Login.Trim();
                    }

                    if (userLogin != login)
                    {
                        continue;
                    }

                    var storedHash = "";
                    if (user.Password != null)
                    {
                        storedHash = user.Password.Trim();
                    }

                    if (PasswordHasher.VerifyPassword(wrongPassword, storedHash))
                    {
                        foundUser = user;
                        break;
                    }
                }

                // Assert
                Assert.IsNull(foundUser);
            }

        }

    }

    [TestFixture]
    public class ComplexNameRegistrationTests
    {
        [TestCase("Мамин-Сибиряк Дмитрий Наркисович", "Мамин-Сибиряк", "Дмитрий", "Наркисович")]
        [TestCase("Ли Кен Хи", "Ли", "Кен", "Хи")]
        [TestCase("Иванов", "Иванов", "", "")]
        [TestCase("Абу Али ибн Сина", "Абу", "Али", "ибн")] 
        [TestCase("Людвиг ван Бетховен", "Людвиг", "ван", "Бетховен")]
        
        
        public void CanRegisterUserWithComplexFio(string inputFio, string expectedSurname, string expectedName, string expectedPatronymic)
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                TestDbHelper.SeedTestData(db);

                var role = db.Roles.First(); 
                var parts = inputFio.Split(' ');
                var surname = "";
                var name = "";
                var patronymic = "";

                if (parts.Length > 0) surname = parts[0];
                if (parts.Length > 1) name = parts[1];
                if (parts.Length > 2) patronymic = parts[2];

                var newUser = new User
                {
                    Id = Guid.NewGuid(),
                    Surname = surname,
                    Name = name,
                    Patronymic = patronymic,
                    Login = inputFio, 
                    Password = PasswordHasher.HashPassword("pass123"),
                    Role_id = role.Id
                };

                // Act
                db.Users.Add(newUser);
                db.SaveChanges();

                User foundUser = null;
                var allUsers = db.Users.ToList();
                foreach (var u in allUsers)
                {
                    if (u.Login != null && u.Login.Trim() == inputFio)
                    {
                        foundUser = u;
                        break;
                    }
                }

                // Assert
                Assert.IsNotNull(foundUser, "Пользователь не найден в БД после сохранения");
                Assert.AreEqual(expectedSurname, foundUser.Surname, "Ошибка в фамилии");
                Assert.AreEqual(expectedName, foundUser.Name, "Ошибка в имени");
                Assert.AreEqual(expectedPatronymic, foundUser.Patronymic, "Ошибка в отчестве");
            }
        }

        [Test]
        public void Registration_WithExtraSpaces_ShouldHandleCorrectly()
        {
            // Arrange
            using (var db = TestDbHelper.CreateInMemoryContext())
            {
                TestDbHelper.SeedTestData(db);
                var role = db.Roles.First();

                string inputFio = "  Иванов   Петр  ";
                var trimmedFio = inputFio.Trim(); 

                var parts = trimmedFio.Split(' ',StringSplitOptions.RemoveEmptyEntries);

                // Act
                var newUser = new User
                {
                    Id = Guid.NewGuid(),
                    Surname = parts.Length > 0 ? parts[0] : "",
                    Name = parts.Length > 1 ? parts[1] : "",
                    Patronymic = parts.Length > 2 ? parts[2] : "",
                    Login = trimmedFio,
                    Password = PasswordHasher.HashPassword("123"),
                    Role_id = role.Id
                };
                db.Users.Add(newUser);
                db.SaveChanges();

                // Assert
                Assert.IsNotEmpty(newUser.Surname);
                Assert.IsNotNull(db.Users.FirstOrDefault(u => u.Login == "Иванов   Петр"));
                Assert.AreEqual("Иванов", newUser.Surname, "Фамилия определена неверно");
                Assert.AreEqual("Петр", newUser.Name, "Имя определено неверно");
            }
        }
    }

}