using NUnit.Framework;
using System.Net;
using Testes.test.stub;
using Testes.src.infra.models;
using Testes.test.integration.config;
using Testes.test.integration.mock;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Testes.test.integration.helper;

namespace Testes.test.integration.tools
{

    [ExcludeFromCodeCoverage]
    internal class FindToolsTest : TestHelper
    {
        private ApplicationSetup _app;
        private HttpClient _client;

        [SetUp]
        public async Task Setup()
        {
            this._app = new ApplicationSetup();
            this._app.InitializeAsync().Wait();
            SeedHelper.ApplyMigrations(this._app).Wait();
            this._client = this._app.CreateClient();
            //
        }

        [TearDown]
        public async Task TearDown()
        {
            await this._app.DisposeAsync();
        }

        [Test]
        [Category("integration")]
        [Description("It should return tool")]
        public async Task Test1()
        {
            // Arrange
            var userId = await SeedHelper.CreateUser(this._app, new UserModel(CreateUserDtoStub.GetData()));
            var toolModel = new ToolModel(CreateToolDtoStub.GetData(userId));
            var toolId = await SeedHelper.CreateTool(this._app, toolModel);
            var tagModel = new TagModel{
                Value = "teste1",
                ToolId = toolId
            };
            await SeedHelper.Create(this._app, tagModel);
            // Act
            var response = await this._client.GetAsync("/api/tools");
            // Assert
            dynamic body = this.ConvertJsonToObject(await response.Content.ReadAsStringAsync());
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(body.data, Has.Count.EqualTo(1));
            Assert.Multiple(() =>
            {
                Assert.That(toolModel.Id, Is.EqualTo((int)body.data[0].id));
                Assert.That(toolModel.Title, Is.EqualTo((string)body.data[0].title));
                Assert.That(toolModel.Link, Is.EqualTo((string)body.data[0].link));
                Assert.That(toolModel.Description, Is.EqualTo((string)body.data[0].description));
                Assert.That(toolModel.UserId, Is.EqualTo((int)body.data[0].userId));
                Assert.That(tagModel.Value, Is.EqualTo((string)body.data[0].tags[0]));
            });
        }
    }
}
