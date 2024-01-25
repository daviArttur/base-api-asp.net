using NUnit.Framework;

namespace Testes.src.infra.services
{
    public class BcryptServiceTest
    {
        [Test]
        [TestCase("rwoeEgwgioq2")]
        [TestCase("3#g9101487124")]
        [TestCase("5!#=423582234")]
        [Category("unit")]
        [Description("It should return true because rawPassword is equivalent the hashPassword")]
        public void Test(string rawPassword) {
          // Arrange
          string hashedPassword = new HashService().Hash(rawPassword);
          // Act
          bool passwordAreEqual = new HashService().Compare(rawPassword, hashedPassword);
          // Assert
          Assert.That(passwordAreEqual, Is.True);
        }
    }
}