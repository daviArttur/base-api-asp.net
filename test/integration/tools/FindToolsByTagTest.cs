using NUnit.Framework;
using System.Net;
using Testes.test.stub;
using Testes.src.infra.models;
using Testes.test.integration.config;
using Testes.test.integration.mock;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Testes.src.domain.dto;

namespace Testes.test.integration.tools
{

    [ExcludeFromCodeCoverage]
    internal class FindToolsTestByTagTest
    {
        private ApplicationSetup _app;
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            this._app = new ApplicationSetup();
            this._app.InitializeAsync().Wait();
            SeedHelper.ApplyMigrations(this._app).Wait();
            this._client = this._app.CreateClient();
        }

        [TearDown]
        public async Task TearDown()
        {
            await this._app.DisposeAsync();
        }

        [Test]
        [Category("integration")]
        [Description("It should return tools that has a tag")]
        public async Task Test1()
        {
            // Arrange
            var userId = await SeedHelper.CreateUser(this._app, new UserModel(CreateUserDtoStub.GetData()));
            ToolModel toolModel1 = await this.CreateToolInDatabase(userId);
            TagModel tagModel1 = await this.CreateTagsForToolInDatabase(toolModel1.Id, "tag");
            ToolModel toolModel2 = await this.CreateToolInDatabase(userId);
            TagModel tagModel2 = await this.CreateTagsForToolInDatabase(toolModel1.Id, "different_tag");
            // Act
            var response = await this._client.GetAsync("/api/tools/tags/tag");
            // Assert
            dynamic body = JsonConvert.DeserializeObject<object>(await response.Content.ReadAsStringAsync())!;
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(body.data, Has.Count.EqualTo(1));
            Assert.Multiple(() =>
            {
                // Tool
                Assert.That(toolModel1.Id, Is.EqualTo((int)body.data[0].id));
                Assert.That(toolModel1.Title, Is.EqualTo((string)body.data[0].title));
                Assert.That(toolModel1.Link, Is.EqualTo((string)body.data[0].link));
                Assert.That(toolModel1.Description, Is.EqualTo((string)body.data[0].description));
                Assert.That(toolModel1.UserId, Is.EqualTo((int)body.data[0].userId));
                // Tag
                Assert.That(tagModel1.Value, Is.EqualTo((string)body.data[0].tags[0]));
            });
        }
    
        private async Task<ToolModel> CreateToolInDatabase(int userId)
        {   
            CreateToolDto createToolDto = CreateToolDtoStub.GetData(userId);
            var toolModel1 = new ToolModel{Description = createToolDto.Description, Link = createToolDto.Link, Title = createToolDto.Title};
            toolModel1.UserId = userId;
            var toolId1 = await SeedHelper.CreateTool(this._app, toolModel1);
            toolModel1.Id = toolId1;
            return toolModel1;
        }

        private async Task<TagModel> CreateTagsForToolInDatabase(int toolId, string tagValue)
        {
            var tagModel = new TagModel{ Value = tagValue, ToolId = toolId }; 
            await SeedHelper.Create(this._app, tagModel);
            return tagModel;
        }
    }
}
