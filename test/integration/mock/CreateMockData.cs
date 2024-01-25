using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Testes.src.domain.entities;
using Testes.src.infra.config.db;
using Testes.src.infra.models;
using Testes.test.integration.config;

namespace Testes.test.integration.mock
{
    [ExcludeFromCodeCoverage]
    internal class SeedHelper
    {
        public static async Task ApplyMigrations(ApplicationSetup application)
        {
            using var scope = application.Services.CreateScope();

            var provider = scope.ServiceProvider;

            using var DbContext = provider.GetRequiredService<AppDbContext>();

            await DbContext.Database.MigrateAsync();
        } 

        public static async Task Create<T>(ApplicationSetup application, T Data) where T : BaseModel
        {
            using var scope = application.Services.CreateScope();

            var provider = scope.ServiceProvider;

            using var DbContext = provider.GetRequiredService<AppDbContext>();

            await DbContext.Database.EnsureCreatedAsync();

            await DbContext.AddAsync<T>(Data);
    
            await DbContext.SaveChangesAsync();
        }

        public static async Task<int> CreateTool(ApplicationSetup application, ToolModel Data)
        {
            using var scope = application.Services.CreateScope();

            var provider = scope.ServiceProvider;

            using var DbContext = provider.GetRequiredService<AppDbContext>();

            await DbContext.Database.EnsureCreatedAsync();

            var tool = await DbContext.Tools.AddAsync(Data);
    
            await DbContext.SaveChangesAsync();

            return tool.Entity.Id;
        }

        public static async Task<int> CreateUser(ApplicationSetup application, UserModel Data)
        {
            using var scope = application.Services.CreateScope();

            var provider = scope.ServiceProvider;

            using var DbContext = provider.GetRequiredService<AppDbContext>();

            await DbContext.Database.EnsureCreatedAsync();

            var tool = await DbContext.Users.AddAsync(Data);
    
            await DbContext.SaveChangesAsync();

            return tool.Entity.Id;
        }


        public static async Task<List<T>> ToListAsync<T>(ApplicationSetup application) where T : BaseModel
        {
            using var scope = application.Services.CreateScope();

            var provider = scope.ServiceProvider;

            using var DbContext = provider.GetRequiredService<AppDbContext>();

            await DbContext.Database.EnsureCreatedAsync();

            return await DbContext.Set<T>().ToListAsync();
        }

        public static async Task<List<ToolModel>> ToListAsyncs(ApplicationSetup application)
        {
            using var scope = application.Services.CreateScope();

            var provider = scope.ServiceProvider;

            using var DbContext = provider.GetRequiredService<AppDbContext>();

            await DbContext.Database.EnsureCreatedAsync();

            return await DbContext.Tools.Include(tt => tt.Tags).ToListAsync();
        }
    }
}