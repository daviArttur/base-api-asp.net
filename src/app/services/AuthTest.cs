using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Primitives;
using NUnit.Framework;
using Testes.src.app.interfaces;

namespace Testes.src.app.services
{   

    [ExcludeFromCodeCoverage]
    public class ConfigurationMock : IConfiguration
    {
        public Dictionary<string, string> data = new Dictionary<string, string>();

        public Dictionary<string, int> calls = new Dictionary<string, int>();

        public virtual string? this[string key]
        {
            get
            {   
                this.calls[key] += 1;
                return this.data[key];
            }
            set
            {          
                this.calls[key] += 1;
            }
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            throw new NotImplementedException();
        }
    }

    class AuthServiceTest
    {   
        private ConfigurationMock _configuration;
        private JwtService _service;

        [SetUp]
        public void Setup() {
            var configurationMock = new ConfigurationMock();
            configurationMock.calls.Add("JwtSettings:SecretKey", 0);
            configurationMock.data.Add("JwtSettings:SecretKey", "Settings.Secretsdfasdfasdfasfasdfasdfasdfasdfasdf");
            this._configuration = configurationMock;
            this._service = new JwtService(new JwtSecurityTokenHandler(), configurationMock);
        }

        [Test]
        [Category("unit")]
        public void ItShouldReturnAcessToken()
        {
            // Stub
            DateTime currentDateTime = DateTime.UtcNow;
            int userId = new Random().Next();
            // Act
            string token = this._service.GenerateToken(userId);
            // Assert
            var sub = new JwtSecurityTokenHandler().ReadJwtToken(token).Payload.Where(s => s.Key == "sub").FirstOrDefault();
            var exp = new JwtSecurityTokenHandler().ReadJwtToken(token).Payload.Where(s => s.Key == "exp").FirstOrDefault();
            DateTime expDateTime = DateTimeOffset.FromUnixTimeSeconds((long)Convert.ToDouble(exp.Value)).UtcDateTime;
            Assert.That(sub.Value, Is.EqualTo(userId.ToString()));
            Assert.Multiple(() =>
            {
                TimeSpan expDifference = expDateTime - currentDateTime;
                bool tokenExpiresIn2Hours = expDifference.TotalHours >= 1.98 && expDifference.TotalHours <= 2;
                Assert.That(tokenExpiresIn2Hours, Is.True);
                Assert.That(this._configuration.calls["JwtSettings:SecretKey"], Is.EqualTo(1));
            });
        }

        [Test]
        [Category("unit")]
        public void ItShouldThrowExceptioBecauseJwtSecreKeyWasNotFound()
        {
            // Stub
            DateTime currentDateTime = DateTime.UtcNow;
            int userId = new Random().Next();
            // Arrange
            this._configuration.data["JwtSettings:SecretKey"] = null!;
            // Act & Assert
            Assert.Throws<JwtSecretKeyNotFound>(() => {
                this._service.GenerateToken(userId);
            });
        }
    }
}