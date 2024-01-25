using NUnit.Framework;
using Testes.src.infra.models;
using Testes.test.integration.config;
using Testes.test.integration.mock;
using Newtonsoft.Json;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Testes.test.integration.tools
{
    [ExcludeFromCodeCoverage]
    internal class CreateUserTest
    {
        private ApplicationSetup _app = null!;
        private HttpClient _client = null!;

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
            string password = "test123";
            // Arrange
            var resquestBody = JsonConvert.SerializeObject(new { email, password });
            HttpContent httpContent = new StringContent(resquestBody, Encoding.UTF8, "application/json");
            // Act
            var response = await this._client.PostAsync("/api/users", httpContent);
            // Assert
            var usersModel = await SeedHelper.ToListAsync<UserModel>(this._app);
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
                Assert.That(usersModel, Has.Count.EqualTo(1));
                Assert.That(usersModel[0].Id, Is.EqualTo(1));
                Assert.That(usersModel[0].Email, Is.EqualTo(email));
                Assert.That(usersModel[0].Password, Is.Not.EqualTo(password));
            });
        }
    }
}
