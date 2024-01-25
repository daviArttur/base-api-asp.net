using System.Diagnostics.CodeAnalysis;
using Testes.src.app.interfaces;
using Testes.src.infra.interfaces;
using Testes.src.infra.repositories;

namespace Testes.src.infra.modules
{
    [ExcludeFromCodeCoverage]
    public class RepositoryModule() : IModule
    {

        public  static void Config(WebApplicationBuilder Builder)
        {
            Builder.Services.AddScoped<IToolRepository, ToolRepository>();
            Builder.Services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
