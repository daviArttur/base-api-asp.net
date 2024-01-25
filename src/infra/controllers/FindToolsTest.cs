using Testes.src.infra.controllers;
using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Testes.src.domain.entities;
using Testes.src.app.contracts.usecases;

public class GetTodoByIdTest
{
    private FindToolsController _controler;

    private Mock<IFindToolsUseCase> _toolMock;

    [SetUp] 
    public void SetUp()
    {
        this._toolMock = new Mock<IFindToolsUseCase>();
        this._controler = new FindToolsController(_toolMock.Object);
    }

    [Test]
    [Category("unit")]
    public async Task ItShouldCallUseCaseWithCorrectParamsAndReturn()
    {
        // Stub
        List<Tool> tools = [];

        // Arrange
        this._toolMock.Setup(todo => todo.Perform()).ReturnsAsync(tools);

        // Act
        ObjectResult? result = await this._controler.Handle() as ObjectResult;
        
        // Assert
        this._toolMock.Verify(toolMock => toolMock.Perform(), Times.Once);
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.EqualTo(new { data = tools }));
        });
    }
}

