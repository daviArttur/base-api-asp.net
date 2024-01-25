using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Testes.src.domain.entities;
using Testes.src.app.contracts.usecases;

namespace Testes.src.infra.controllers
{
    public class FindToolsByTagControllerTest
    {
        private FindToolsByTagController _controler;
        private Mock<IFindToolsByTagUseCase> _useCaseMock;

        [SetUp] 
        public void SetUp()
        {
            this._useCaseMock = new Mock<IFindToolsByTagUseCase>();
            this._controler = new FindToolsByTagController(_useCaseMock.Object);
        }

        [Test]
        [Category("unit")]
        public async Task ItShouldCallUseCaseWithCorrectParamsAndReturn()
        {
            // Stub
            List<Tool> tools = [];
            string tag = "123";
            // Arrange
            this._useCaseMock.Setup(todo => todo.Perform(tag)).ReturnsAsync(tools);
            // Act
            ObjectResult? result = await this._controler.Handle(tag) as ObjectResult;
            // Assert
            this._useCaseMock.Verify(toolMock => toolMock.Perform(tag), Times.Once);
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(200));
                Assert.That(result.Value, Is.EqualTo(new { data = tools }));
            });
        }
    }
}
