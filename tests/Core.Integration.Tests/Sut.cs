using Core.Infrastructure.Persistence;
using FastEndpoints.Testing;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace Core.Tests;

public class Sut : AppFixture<Program>
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithPassword("dbPassword")
        .Build();

    public ApplicationDbContext DbContext;

    protected override async ValueTask PreSetupAsync()
    {
        await _dbContainer.StartAsync();
    }

    protected override async ValueTask SetupAsync()
    {
        var scope = Services.CreateScope();
        DbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await DbContext.Database.MigrateAsync();
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        // Use Testcontainer DbContext
        var descriptor = services.SingleOrDefault(s =>
            s.ServiceType == typeof(DbContextOptions<ApplicationDbContext>)
        );

        services.Remove(descriptor!);

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(_dbContainer.GetConnectionString());
        });

        services
            .AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = "Test";
                o.DefaultChallengeScheme = "Test";
            })
            .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", _ => { });
    }
}
