using NUnit.Framework;
using Testes.src.app.interfaces;
using Testes.src.domain.entities;
using Testes.src.domain.dto;
using Moq;
using Testes.test.stub;
using Testes.src.app.contracts.usecases;


namespace Testes.src.app.usecases
{
    internal class CreateTodoUseCaseTest()
    {
        private ISaveToolUseCase _usecase = null!;

        private Mock<IToolRepository> _toolRepositoryMock = null!;

        [SetUp]
        public void SetUp()
        {
            this._toolRepositoryMock = new Mock<IToolRepository>();
            this._usecase = new SaveToolUseCase(_toolRepositoryMock.Object);
        }

        [Test]
        [Category("unit")]
        public async Task ItShouldSaveToolInRepository()
        {
            // Stub
            CreateToolDto createToolDto = CreateToolDtoStub.GetData();
            // Arrange
            Tool capturedTool = null!;
            this._toolRepositoryMock.Setup(p => p.Save(It.IsAny<Tool>())).Callback<Tool>((tool) => {
                capturedTool = tool;
            });
            // Act
            await this._usecase.Perform(createToolDto);
            // Assert
            this._toolRepositoryMock.Verify(
                repositoryMock => repositoryMock.Save(It.IsAny<Tool>()),
                Times.Once
            );
            Assert.Multiple(() => {
                Assert.That(capturedTool.Id, Is.EqualTo(createToolDto.Id));
                Assert.That(capturedTool.Tags, Is.EqualTo(createToolDto.Tags));
                Assert.That(capturedTool.Title, Is.EqualTo(createToolDto.Title));
                Assert.That(capturedTool.Description, Is.EqualTo(createToolDto.Description));
                Assert.That(capturedTool.Link, Is.EqualTo(createToolDto.Link));
            });
        }
    }
}