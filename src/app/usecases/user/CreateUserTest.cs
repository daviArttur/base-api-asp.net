using NUnit.Framework;
using Testes.src.app.interfaces;
using Testes.src.domain.entities;
using Moq;
using Testes.test.stub;

namespace Testes.src.app.usecases.user
{
  class CreateUserUseCaseTest()
  {
    private CreateUserUseCase _usecase = null!;
    private Mock<IUserRepository> _repositoryMock = null!;
    private Mock<IHashService> _hashService = null!;

    [SetUp]
    public void SetUp()
    {
      this._hashService = new Mock<IHashService>();
      this._repositoryMock = new Mock<IUserRepository>();
      this._usecase = new CreateUserUseCase(_repositoryMock.Object, _hashService.Object);
    }

    [Test]
    [Category("unit")]
    [Description("It should create a new user")]
    public async Task Test1()
    {
      // Stub
      string email = "test@mail.com";
      string password = "test@mail.com";
      string hashPassword = "test@mail.com";
      User expectedUser = null!;

      // Arrange
      _repositoryMock.Setup(repo => repo.FindOneByEmail(email)).ReturnsAsync((User)null!);
      _repositoryMock.Setup(repo => repo.Save(It.IsAny<User>())).Callback<User>(user => expectedUser = user);
      _hashService.Setup(repo => repo.Hash(password)).Returns(hashPassword);

      // Act
      await this._usecase.Perform(email, password);

      // Assert
      _repositoryMock.Verify(repo => repo.Save(It.IsAny<User>()), Times.Once());
      _hashService.Verify(hash => hash.Hash(password), Times.Once());
      Assert.Multiple(() =>
      {
        Assert.That(expectedUser.Id, Is.EqualTo(0));
        Assert.That(expectedUser.Email, Is.EqualTo(email));
        Assert.That(expectedUser.Password, Is.EqualTo(hashPassword));
      });
    }

    [Test]
    [Category("unit")]
    [Description("It should throw exception beucase user with email already exists")]
    public void Test2()
    {
      // Stub
      string email = "test@mail.com";
      string password = "test@mail.com";
      User userStub = new User(CreateUserDtoStub.GetData(true));
      // Arrange
      _repositoryMock.Setup(repo => repo.FindOneByEmail(email)).ReturnsAsync(userStub);
      // Act & Assert
      Assert.ThrowsAsync<UserEmailAlreadyUsedException>(async () =>
      {
        await this._usecase.Perform(email, password);
      });
    }
  }
}
