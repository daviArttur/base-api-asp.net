using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Testes.src.infra.dto;
using Testes.src.app.contracts.usecases;

namespace Testes.src.infra.controllers
{
    public class SignInUserControllerTest
    {
        private SignInUserController _controler;
        private Mock<ISignInUserUseCase> _useCaseMock;

        [SetUp] 
        public void SetUp()
        {
            this._useCaseMock = new Mock<ISignInUserUseCase>();
            this._controler = new SignInUserController(_useCaseMock.Object);
        }

        [Test]
        [Category("unit")]
        public async Task ItShouldCallUseCaseWithCorrectParamsAndReturn()
        {
            // Stub
            CreateUserDtoInfra createUserDtoInfra = new CreateUserDtoInfra{ Email = "davi@mail.com", Password = "password@mail.com" };
            var outputStub = new SignInUserOutput{ AccessToken = "asd" };
            // Arrange
            this._useCaseMock.Setup(todo => todo.Perform(createUserDtoInfra.Email, createUserDtoInfra.Password)).ReturnsAsync(outputStub);
            // Act
            ObjectResult? result = await this._controler.Handle(createUserDtoInfra) as ObjectResult;
            // Assert
            this._useCaseMock.Verify(toolMock => toolMock.Perform(createUserDtoInfra.Email, createUserDtoInfra.Password), Times.Once);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo(new {data = outputStub}));
        }
    }
}
