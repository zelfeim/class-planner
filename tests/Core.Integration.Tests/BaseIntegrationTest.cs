using System.Text.Json;
using Core.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Tests;

public class BaseIntegrationTest : IClassFixture<IntegrationTestWebApplicationFactory>, IDisposable
{
    private readonly IServiceScope _scope;

    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;

    protected BaseIntegrationTest(IntegrationTestWebApplicationFactory factory)
    {
        _scope = factory.Services.CreateScope();

        Client = factory.CreateClient();
        DbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        DbContext.Database.Migrate();
    }

    public void Dispose()
    {
        _scope.Dispose();
        Client.Dispose();
        DbContext.Dispose();

        GC.SuppressFinalize(this);
    }
}
