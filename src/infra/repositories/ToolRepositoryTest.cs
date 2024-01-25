using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Testes.src.app.interfaces;
using Testes.src.domain.entities;
using Testes.src.infra.config.db;
using Testes.src.infra.models;
using Testes.test.stub;

namespace Testes.src.infra.repositories
{
    internal class ToolRepositoryTest()
    {
        private AppDbContext _dbContext = null!;
        private IToolRepository _toolRepository = null!;

        [SetUp]
        public void SetUp()
        {
            
            var builder  = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            this._dbContext = new AppDbContext(builder.Options);
            _toolRepository = new ToolRepository(this._dbContext);
        }

        [Test]
        [Category("unit")]
        public async Task Test1()
        {
            // Stub
            Tool tool = new Tool(CreateToolDtoStub.GetData(2, true));
            // Act
            await this._toolRepository.Save(tool);
            // Assert
            List<ToolModel> toolsModelInDB = await this._dbContext.Tools.ToListAsync();
            Assert.That(toolsModelInDB, Has.Count.EqualTo(1));  
            Assert.Multiple(() => {
                Assert.That(toolsModelInDB[0], Is.Not.Null);
                Assert.That(toolsModelInDB[0].Id, Is.EqualTo(tool.Id));
                Assert.That(toolsModelInDB[0].Title, Is.EqualTo(tool.Title));
                Assert.That(toolsModelInDB[0].Link, Is.EqualTo(tool.Link));
                Assert.That(toolsModelInDB[0].Description, Is.EqualTo(tool.Description));
            });
        }

        [Test]
        [Category("unit")]
        public void ItShouldThrowQueryExceptionBecauseAnErrorOcurredOnSaveTool()
        {
            Tool nullEntityToTrow = null!;
            // Act & Assert
            Assert.ThrowsAsync<QueryException>(async () => {
                await this._toolRepository.Save(nullEntityToTrow);
            });
        }

        [Test]
        [Category("unit")]
        public async Task ItShouldFindAllToolsConvertToolModelAndReturn()
        {
            // Stub
            List<ToolModel> toolsModel = [];
            int toolsModelCounter = new Random().Next(1, 5);
            // Arrange
            for (int i = 0; i < toolsModelCounter; i++)
            {
                var dtoStub = CreateToolDtoStub.GetData(2);
                var toolModel = new ToolModel(dtoStub); 
                this._dbContext.Tools.Add(toolModel);
                toolsModel.Add(toolModel);
            }
            this._dbContext.SaveChanges();
            // Act
            var result = await this._toolRepository.FindAll();
            // Assert
            Assert.That(result, Has.Count.EqualTo(toolsModelCounter));
            for (int i = 0; i < toolsModelCounter; i++)
            {
                Assert.Multiple(() => {
                    Assert.That(result[i], Is.Not.Null);
                    Assert.That(result[i].Id, Is.EqualTo(toolsModel[i].Id));
                    Assert.That(result[i].Title, Is.EqualTo(toolsModel[i].Title));
                    Assert.That(result[i].Link, Is.EqualTo(toolsModel[i].Link));
                    Assert.That(result[i].Description, Is.EqualTo(toolsModel[i].Description));
                    Assert.That(result[i].Tags, Is.EqualTo(toolsModel[i].Tags));
                });
            }
        }

        [Test]
        [Category("unit")]
        public async Task ItShouldFindToolsThatHasASpecificTagAndReturn()
        {
            // Stub
            var dtoStub = CreateToolDtoStub.GetData(2, true);
            var toolModel = new ToolModel(dtoStub); 
            var tag = new TagModel{
                ToolId = toolModel.Id,
                Value = "123",
            };
            var tag2 = new TagModel{
                ToolId = toolModel.Id,
                Value = "123",
            };
            toolModel.Tags = [tag, tag2];
            // Arrange
            this._dbContext.Tools.Add(toolModel);
            this._dbContext.SaveChanges();
            // Act
            var result = await this._toolRepository.FindByTag("123");
            //Assert
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Tags, Has.Count.EqualTo(2));
            Assert.Multiple(() => {
                Assert.That(result[0].Id, Is.EqualTo(toolModel.Id));
                Assert.That(result[0].Title, Is.EqualTo(toolModel.Title));
                Assert.That(result[0].Link, Is.EqualTo(toolModel.Link));
                Assert.That(result[0].Description, Is.EqualTo(toolModel.Description));
            });
            for (int i = 0; i < result[0].Tags.Count; i++)
            {
                Assert.Multiple(() => {
                    Assert.That(result[0], Is.Not.Null);
                    Assert.That(result[0].Tags[i], Is.EqualTo(toolModel.Tags.ElementAt(i).Value));
                });
            }
        }
    }
}