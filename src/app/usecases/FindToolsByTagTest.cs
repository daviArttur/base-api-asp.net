using NUnit.Framework;
using Testes.src.app.interfaces;
using Testes.src.app.usecases;
using Testes.src.domain.entities;
using Moq;
using Testes.src.domain.dto;

namespace Testes.src.app
{
    internal class FindToolsByTagTest()
    {
        private FindToolsByTagUseCase _usecase = null!;
        private Mock<IToolRepository> _repositoryMock = null!;

        [SetUp]
        public void SetUp()
        {
            this._repositoryMock = new Mock<IToolRepository>();
            this._usecase = new FindToolsByTagUseCase(_repositoryMock.Object);
        }

        [Test]
        [Category("unit")]
        public async Task ItShouldFindToolsAndReturn()
        {
            // Stub
            string tag = "tagStub";
            CreateToolDto dto = new CreateToolDto{
                Description = "Description",
                Id = 1,
                Link = "Link",
                Title = "Title",
                Tags = [],
                UserId = 2
            };
            var todoStub = new Tool(dto);

            // Arrange
            _repositoryMock.Setup(repo => repo.FindByTag(tag)).ReturnsAsync([todoStub]);

            // Act
            var result = await this._usecase.Perform(tag);

            // Assert
            List<Tool> expectedTools = [todoStub];
            _repositoryMock.Verify(repo => repo.FindByTag(tag), Times.Once());
            Assert.That(result, Is.EqualTo(expectedTools));
        }
    }
}
