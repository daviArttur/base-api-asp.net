using Testes.src.infra.controllers;
using NUnit.Framework;
using Moq;
using Testes.src.domain.dto;
using Testes.src.infra.dto;
using Testes.src.app.interfaces;
using Testes.src.app.contracts.usecases;

internal class CreateTodoTest
{
    private SaveToolController Controller;
    private Mock<ISaveToolUseCase> _useCaseMock;
    private Mock<IAuthService> _authServiceMock;

    [SetUp]
    public void SetUp()
    {
        this._useCaseMock = new Mock<ISaveToolUseCase>();
        this._authServiceMock = new Mock<IAuthService>();
        this.Controller = new SaveToolController(this._useCaseMock.Object, this._authServiceMock.Object);
    }

    [Test]
    [Category("unit")]
    public async Task ItShouldCallUseCaseWithCorrectParams()
    {
        // Stub
        SaveToolDtoInfra body = new SaveToolDtoInfra
        {
            description = "Description",
            link = "Link",
            title = "Title",
        };
        int userId = 123;
        // Arrange
        CreateToolDto capturedCreateToolDto = null!;
        this._useCaseMock.Setup(mock => mock.Perform(It.IsAny<CreateToolDto>())).Callback<CreateToolDto>(dto => capturedCreateToolDto = dto);
        this._authServiceMock.Setup(mock => mock.RecoveryUserIdFromHttpContext(It.IsAny<HttpContext>())).Returns(userId);
        // Act
        await this.Controller.Handle(body);
        // Assert
        this._useCaseMock.Verify(t => t.Perform(It.IsAny<CreateToolDto>()), Times.Once);
        Assert.That(capturedCreateToolDto, Is.Not.Null);
        Assert.That(capturedCreateToolDto, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(capturedCreateToolDto.Id, Is.EqualTo(0));
            Assert.That(capturedCreateToolDto.Description, Is.EqualTo(body.description));
            Assert.That(capturedCreateToolDto.Link, Is.EqualTo(body.link));
            Assert.That(capturedCreateToolDto.Title, Is.EqualTo(body.title));
            Assert.That(capturedCreateToolDto.UserId, Is.EqualTo(userId));
        });
    }
}

