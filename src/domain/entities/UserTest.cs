using NUnit.Framework;
using Testes.src.domain.dto;

namespace Testes.src.domain.entities
{
  internal class UserTest()
  {
    [Test]
    [Category("unit")]
    [Description("It should be defined")]
    public void Test1()
    {
      // Arrange
      CreateUserDto dto = new CreateUserDto(1, "email", "password");

      // Act
      var Tool = new User(dto);

      // Assert
      Assert.Multiple(() =>
      {
        Assert.That(Tool.Id, Is.EqualTo(dto.Id));
        Assert.That(Tool.Email, Is.EqualTo(dto.Email));
        Assert.That(Tool.Password, Is.EqualTo(dto.Password));
      });
    }

    [Test]
    [Category("unit")]
    [Description("It return a new User with correct params")]
    public void Test2()
    {
      string email = "email";
      string password = "password";

      // Act
      var Tool = User.Create(email, password);

      // Assert
      Assert.Multiple(() =>
      {
        Assert.That(Tool.Id, Is.EqualTo(0));
        Assert.That(Tool.Email, Is.EqualTo(email));
        Assert.That(Tool.Password, Is.EqualTo(password));
      });
    }
  }
}