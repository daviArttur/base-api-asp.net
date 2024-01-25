using NUnit.Framework;
using Testes.src.app.interfaces;
using Testes.src.app.usecases;
using Testes.src.domain.entities;
using Moq;
using Testes.test.stub;

namespace Testes.src.app
{
    public class FindToolsUseCaseTest()
    {
        private FindToolsUseCase _useCase = null!;
        private Mock<IToolRepository> _repositoryMock = null!;

        [SetUp]
        public void SetUp()
        {
            this._repositoryMock = new Mock<IToolRepository>();
            this._useCase = new FindToolsUseCase(_repositoryMock.Object);
        }

        [Test]
        [Category("unit")]
        public async Task ItShouldFindToolsAndReturn()
        {
            // Stub
            var todoStub = new Tool(CreateToolDtoStub.GetData());
            // Arrange
            _repositoryMock.Setup(repo => repo.FindAll()).ReturnsAsync([todoStub]);
            // Act
            var result = await this._useCase.Perform();
            // Assert
            List<Tool> expectedTools = [todoStub];
            _repositoryMock.Verify(repo => repo.FindAll(), Times.Once());
            Assert.That(result, Is.EqualTo(expectedTools));
        }
    }
}
