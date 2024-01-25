using NUnit.Framework;
using Testes.test.stub;
using Testes.src.infra.models;
using Testes.test.integration.config;
using Testes.test.integration.mock;
using Testes.src.domain.entities;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Testes.test.integration.helper;

namespace Testes.test.integration.tools
{
    [ExcludeFromCodeCoverage]
    internal class SaveToolTest : TestHelper
    {
        private ApplicationSetup _app;
        private HttpClient _client;
        private int userId; 

        [SetUp]
        public async Task Setup()
        {
            this._app = new ApplicationSetup();
            await this._app.InitializeAsync();
            SeedHelper.ApplyMigrations(this._app).Wait();
            this._client = this._app.CreateClient();
            userId = await SeedHelper.CreateUser(this._app, new UserModel(CreateUserDtoStub.GetData()));
        }

        [TearDown]
        public async Task TearDown()
        {
            await this._app.DisposeAsync();
        }

        [Test]
        [Category("integration")]
        [Description("It should save tool in db")]
        public async Task Test1()
        {
            // Stub
            Tool toolStub = new Tool(CreateToolDtoStub.GetData(this.userId));
            string accessToken = this.GetJwtToken(this.userId);
            // Arrange
            var resquestBody = this.ConvertObjectToJson(new { link = toolStub.Link, title = toolStub.Title, description = toolStub.Description, tags = toolStub.Tags, });
            HttpContent httpContent = this.GetDefaultHttpContent(resquestBody);
            this.setAccessTokenInHttpClient(this._client, accessToken);
            // Act
            var response = await this._client.PostAsync("/api/tools", httpContent);
            // Assert
            List<ToolModel> toolsModelInDB = await SeedHelper.ToListAsync<ToolModel>(this._app);
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
                Assert.That(toolsModelInDB, Has.Count.EqualTo(1));
                Assert.That(toolsModelInDB[0].Id, Is.EqualTo(1));
                Assert.That(toolsModelInDB[0].Title , Is.EqualTo(toolStub.Title));
                Assert.That(toolsModelInDB[0].Link, Is.EqualTo(toolStub.Link));
                Assert.That(toolsModelInDB[0].Description, Is.EqualTo(toolStub.Description));
                Assert.That(toolsModelInDB[0].UserId, Is.EqualTo(toolStub.UserId));
            });
        }
    }
}
