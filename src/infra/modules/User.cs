using System.Diagnostics.CodeAnalysis;
using Testes.src.app.contracts.usecases;
using Testes.src.app.usecases.user;
using Testes.src.infra.interfaces;

namespace Testes.src.infra.modules
{
    [ExcludeFromCodeCoverage]
    public class UserModule() : IModule
    {
        public  static void Config(WebApplicationBuilder Builder)
        {
            Builder.Services.AddScoped<ISignInUserUseCase, SignInUserUseCase>();
            Builder.Services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
        }
    }
}
