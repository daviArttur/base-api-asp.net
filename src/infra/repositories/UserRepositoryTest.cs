using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Testes.src.app.interfaces;
using Testes.src.domain.entities;
using Testes.src.infra.config.db;
using Testes.src.infra.models;
using Testes.test.stub;

namespace Testes.src.infra.repositories
{
  class UserRepositoryTest()
  {
    private AppDbContext _dbContext = null!;
    private IUserRepository _userRepository = null!;

    [SetUp]
    public void SetUp()
    {
      var builder = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
      this._dbContext = new AppDbContext(builder.Options);
      _userRepository = new UserRepository(this._dbContext);
    }

    [Test]
    [Category("unit")]
    [Description("Save()")]
    public async Task TaskItShouldSaveToolInDatabase()
    {
      // Stub
      User user = new User(CreateUserDtoStub.GetData(true));

      // Act
      await this._userRepository.Save(user);

      // Assert
      List<UserModel> toolsModelInDB = await this._dbContext.Users.ToListAsync();
      Assert.That(toolsModelInDB, Has.Count.EqualTo(1));
      Assert.Multiple(() =>
      {
        Assert.That(toolsModelInDB[0], Is.Not.Null);
        Assert.That(toolsModelInDB[0].Id, Is.EqualTo(1));
        Assert.That(toolsModelInDB[0].Email, Is.EqualTo(user.Email));
        Assert.That(toolsModelInDB[0].Password, Is.EqualTo(user.Password));
      });
    }

    [Test]
    [Category("unit")]
    [Description("Save()")]
    public void ItShouldThrowQueryExceptionBecauseAnErrorOcurredOnFindToolByTag()
    {
      User nullObjectToThrow = null!;
      // Act & Assert
      Assert.ThrowsAsync<QueryException>(async () =>
      {
        await this._userRepository.Save(nullObjectToThrow);
      });
    }

    [Test]
    [Category("unit")]
    public async Task ItShouldFindUserById()
    {
      // Stub
      var dtoStub = CreateUserDtoStub.GetData(true);
      var userModel = new UserModel(dtoStub);
      // Arrange
      this._dbContext.Users.Add(userModel);
      this._dbContext.SaveChanges();
      // Act
      var result = await this._userRepository.FindOneById(userModel.Id);
      //Assert
      Assert.That(result, Is.Not.Null);
      Assert.Multiple(() =>
      {
        Assert.That(result.Id, Is.EqualTo(userModel.Id));
        Assert.That(result.Email, Is.EqualTo(userModel.Email));
        Assert.That(result.Password, Is.EqualTo(userModel.Password));
      });
    }

    [Test]
    [Category("unit")]
    public async Task ItShouldReturnNullBecauseUserWithIdDoesNotExist()
    {
      // Stub
      int userId = 2;
      // Act
      var result = await this._userRepository.FindOneById(userId);
      //Assert
      Assert.That(result, Is.Null);
    }

    [Test]
    [Category("unit")]
    public async Task ItShouldFindUserByEmailAndReturn()
    {
      // Stub
      var dtoStub = CreateUserDtoStub.GetData(true);
      var userModel = new UserModel(dtoStub);
      // Arrange
      this._dbContext.Users.Add(userModel);
      this._dbContext.SaveChanges();
      // Act
      var result = await this._userRepository.FindOneByEmail(userModel.Email);
      //Assert
      Assert.That(result, Is.Not.Null);
      Assert.Multiple(() =>
      {
        Assert.That(result.Id, Is.EqualTo(userModel.Id));
        Assert.That(result.Email, Is.EqualTo(userModel.Email));
        Assert.That(result.Password, Is.EqualTo(userModel.Password));
      });
    }

    [Test]
    [Category("unit")]
    public async Task ItShouldReturnNullBecauseUserWithEmailDoesNotExist()
    {
      // Stub
      string email = "test@mail.com";
      // Act
      var result = await this._userRepository.FindOneByEmail(email);
      //Assert
      Assert.That(result, Is.Null);
    }
  }
}