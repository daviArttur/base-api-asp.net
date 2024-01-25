using Moq;
using NUnit.Framework;
using Testes.src.app.interfaces;
using Testes.src.domain.entities;
using Testes.test.stub;

namespace Testes.src.app.usecases.user
{
  class SignInUserUseCaseTest()
  {
    private Mock<IUserRepository> _repository = null!;

    private Mock<IHashService> _hashService = null!;

    private Mock<IAuthService> _AuthService = null!;

    private SignInUserUseCase usecase = null!;

    [SetUp]
    public void SetUp()
    {
      this._hashService = new Mock<IHashService>();
      this._AuthService = new Mock<IAuthService>();
      this._repository = new Mock<IUserRepository>();
      this.usecase = new SignInUserUseCase(this._repository.Object, this._hashService.Object, this._AuthService.Object);
    }

    [Test]
    [Category("unit")]
    [Description("it should find user, compare password, generate token and return access token")]
    public async Task Test1()
    { 
      // Stub
      User user = new User(CreateUserDtoStub.GetData());
      string email = user.Email;
      string rawPassword = user.Password;
      string accessToken = "accessToken";
      // Arrange
      this._repository.Setup(repo => repo.FindOneByEmail(email)).ReturnsAsync(user);
      this._hashService.Setup(hashService => hashService.Compare(rawPassword, user.Password)).Returns(true);
      this._AuthService.Setup(AuthService => AuthService.GenerateToken(user.Id)).Returns(accessToken);
      // Act
      var result = await this.usecase.Perform(email, rawPassword);
      // Assert
      Assert.That(result.AccessToken, Is.EqualTo(accessToken));
    }

    [Test]
    [Category("unit")]
    [Description("it should throw exception because user does not exist")]
    public void Test2()
    { 
      // Stub
      User user = new User(CreateUserDtoStub.GetData());
      string email = user.Email;
      string rawPassword = user.Password;
      // Arrange
      this._repository.Setup(repo => repo.FindOneByEmail(email)).ReturnsAsync((User)null!);
      // Act & Assert
      Assert.ThrowsAsync<UserNotFoundException>(async () => {
        await this.usecase.Perform(email, rawPassword);
      });
    }

    [Test]
    [Category("unit")]
    [Description("it should throw exception because rawPassword is diferent to hashed password")]
    public void Test3()
    {
      // Stub
      User user = new User(CreateUserDtoStub.GetData());
      string email = user.Email;
      string rawPassword = user.Password;
      // Arrange
      this._repository.Setup(repo => repo.FindOneByEmail(email)).ReturnsAsync(user);
      this._hashService.Setup(hashService => hashService.Compare(rawPassword, user.Password)).Returns(false);
      // Act & Assert
      Assert.ThrowsAsync<UserNotFoundException>(async () => {
        await this.usecase.Perform(email, rawPassword);
      });
    }
  }
}
