using NUnit.Framework;
using Testes.src.infra.models;
using Testes.test.integration.config;
using Testes.test.integration.mock;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using Testes.test.stub;
using Testes.src.infra.services;
using Testes.test.integration.helper;

namespace Testes.test.integration.tools
{
    [ExcludeFromCodeCoverage]
    internal class SignInUserTest : TestHelper
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
        [Description("It should save user in db")]
        public async Task Test1()
        {
            // Stub
            string email = "test@mail.com";
            string rawPassword = "test@mail.com";
            var resquestBody = this.ConvertObjectToJson(new { email, password = rawPassword });
            // Arrange
            await this.CreateUser(rawPassword);
            HttpContent httpContent = new StringContent(resquestBody, Encoding.UTF8, "application/json");
            // Act
            var response = await this._client.PostAsync("/api/users/signin", httpContent);
            dynamic responseBody = this.ConvertJsonToObject(await response.Content.ReadAsStringAsync());
            // Assert
            Assert.That(responseBody.data.accessToken, Is.Not.Null);
        }

        private async Task CreateUser(string rawPassword)
        {
            string password = new HashService().Hash(rawPassword);
            UserModel userModel = new UserModel(CreateUserDtoStub.GetData());
            userModel.Password = password;
            await SeedHelper.Create(this._app, userModel);
        }
    }
}
