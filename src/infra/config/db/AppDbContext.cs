using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Testes.src.domain.entities;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Testes.src.infra.models;
using System.Diagnostics.CodeAnalysis;

namespace Testes.src.infra.config.db
{

    // public interface IAppDbContext
    // {
    //     // Exemplo de métodos que o DbContext pode fornecer
    //     DbSet<ToolModel> Tools { get; set; }
    //     Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    //     Task<int> SaveChanges();
    //     // Outros métodos ou propriedades necessárias
    // }

    [ExcludeFromCodeCoverage]
    public class AppDbContext : DbContext
    {
        public virtual DbSet<ToolModel> Tools { get; set; }
        public virtual DbSet<TagModel> Tags { get; set; }
        public virtual DbSet<UserModel> Users { get; set; }

        public AppDbContext() 
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //    optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("DatabaseConnection") ?? "Data Source=localhost;Initial Catalog=personal;User ID=sa;Password=#dadospro3131;Encrypt=False"); // Substitua pela sua string de conexão
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToolModel>()
            .HasMany(e => e.Tags)
            .WithOne(e => e.Tool)
            .HasForeignKey(e => e.ToolId)
            .IsRequired();

            modelBuilder.Entity<UserModel>()
            .HasMany(e => e.Tools)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
        }
    }
}
