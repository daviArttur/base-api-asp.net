using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Testes.src.app.contracts;
using Testes.src.infra.config.db;
using Testes.src.infra.modules;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    var a = Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]);
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = false,
        IssuerSigningKey = new SymmetricSecurityKey(a)
    };
});


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDbConnection"))
);

ServiceModule.Config(builder);
UserModule.Config(builder);
RepositoryModule.Config(builder);
TodoModule.Config(builder);


var app = builder.Build();
app.UseAuthorization();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        var error = context.Features.Get<IExceptionHandlerFeature>();
        if (error is null)
        {
            await context.Response.WriteAsJsonAsync(new { message = "Um erro inesperado aconteceu", statusCode = 500 });
            return;
        };
        AppException? appException = error.Error as AppException;
        if (appException is null)
        {
            await context.Response.WriteAsJsonAsync(new { message = "Um erro inesperado aconteceu", statusCode = 500 });
            return;
        }
        context.Response.StatusCode = appException.statusCode;
        await context.Response.WriteAsJsonAsync(new { appException.message, appException.statusCode });
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
