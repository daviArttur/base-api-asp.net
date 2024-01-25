using System.Diagnostics.CodeAnalysis;
using Testes.src.app.contracts.usecases;
using Testes.src.app.usecases;
using Testes.src.infra.interfaces;

namespace Testes.src.infra.modules
{
    [ExcludeFromCodeCoverage]
    public class TodoModule() : IModule
    {
        public static void Config(WebApplicationBuilder Builder)
        {
            Builder.Services.AddScoped<ISaveToolUseCase, SaveToolUseCase>();
            Builder.Services.AddScoped<IFindToolsUseCase, FindToolsUseCase>();
            Builder.Services.AddScoped<IFindToolsByTagUseCase, FindToolsByTagUseCase>();
        }
    }
}
