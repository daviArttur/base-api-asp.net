using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using Testes.src.app.interfaces;
using Testes.src.app.services;
using Testes.src.infra.interfaces;
using Testes.src.infra.services;

namespace Testes.src.infra.modules
{
    [ExcludeFromCodeCoverage]
    public class ServiceModule() : IModule
    {

        public  static void Config(WebApplicationBuilder Builder)
        {
            Builder.Services.AddSingleton(new JwtSecurityTokenHandler());
            Builder.Services.AddScoped<IAuthService, JwtService>();
            Builder.Services.AddScoped<IHashService, HashService>();
        }
    }
}
