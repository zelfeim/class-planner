using Core.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Features.Calendar.Get;

[HttpGet("/api/calendar")]
public class Endpoint(ILogger<Endpoint> logger, ApplicationDbContext dbContext) : EndpointWithoutRequest<List<Domain.Entity.Calendar>>
{
    public override async Task HandleAsync(CancellationToken ct)
    {
        logger.LogInformation("Getting all calendars.");

        var calendars = await dbContext.Calendars.ToListAsync(cancellationToken: ct);
        
        await SendAsync(calendars, cancellation: ct);
    } 
}