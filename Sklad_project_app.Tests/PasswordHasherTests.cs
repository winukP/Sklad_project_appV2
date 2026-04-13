using NUnit.Framework;
using SkladApp;

namespace SkladApp.Tests
{
    [TestFixture]
    public class PasswordHelperTests
    {
        [Test]
        public void HashPassword_ReturnsNonEmptyString()
        {
            // Arrange
            var password = "12345";

            // Act
            var hash = PasswordHasher.HashPassword(password);

            // Assert
            Assert.IsNotNull(hash);
            Assert.IsNotEmpty(hash);
        }

        [Test]
        public void HashPassword_CanBeVerified()
        {
            // Arrange
            var password = "mypassword";

            // Act
            var hashedPassword = PasswordHasher.HashPassword(password);

            // Assert
            Assert.IsTrue(PasswordHasher.VerifyPassword(password, hashedPassword));
        }

        [Test]
        public void HashPassword_DifferentPasswordsGiveDifferentHashes()
        {
            // Arrange
            var firstPassword = "password1";
            var secondPassword = "password2";

            // Act
            var firstHash = PasswordHasher.HashPassword(firstPassword);
            var secondHash = PasswordHasher.HashPassword(secondPassword);

            // Assert
            Assert.AreNotEqual(firstHash, secondHash);
        }

     
        [Test]
        public void VerifyPassword_CorrectPasswordReturnsTrue()
        {
            // Arrange
            var password = "mypassword";
            var hash = PasswordHasher.HashPassword(password);

            // Act
            var result = PasswordHasher.VerifyPassword(password, hash);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void VerifyPassword_WrongPasswordReturnsFalse()
        {
            // Arrange
            var correctPassword = "mypassword";
            var wrongPassword = "wrongpassword";
            var hash = PasswordHasher.HashPassword(correctPassword);

            // Act
            var result = PasswordHasher.VerifyPassword(wrongPassword, hash);

            // Assert
            Assert.IsFalse(result);
        }
    }
}