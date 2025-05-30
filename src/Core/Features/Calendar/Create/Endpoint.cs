using Core.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Features.Calendar.Create;

[HttpPost("/api/calendar")]
public class Endpoint(ILogger<Endpoint> logger, ApplicationDbContext dbContext)
    : Endpoint<Request, int>
{
    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        logger.LogInformation("Creating calendar.");

        var calendars = await dbContext
            .Calendars.Include(calendar => calendar.Year)
            .ToListAsync(cancellationToken: ct);

        if (calendars.Find(x => x.Year.Id == req.YearId) != null)
        {
            logger.LogWarning("Calendar for year {YearId} already exists.", req.YearId);
            ThrowError($"Calendar for year {req.YearId} already exists.");
        }

        var newCalendar = new Domain.Entity.Calendar(req.YearId);

        dbContext.Calendars.Add(newCalendar);
        await dbContext.SaveChangesAsync(ct);

        await SendAsync(newCalendar.Id, cancellation: ct);
    }
}
