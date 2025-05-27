using Core.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Features.Year.GetYearCalendar;

[HttpGet("api/year/{Id}/calendar")]
[AllowAnonymous]
public class GetYearCalendarEndpoint(
    ILogger<GetYearCalendarEndpoint> logger,
    ApplicationDbContext dbContext
) : Endpoint<GetYearCalendarRequest, GetYearCalendarResponse, GetYearCalendarMapper>
{
    public override async Task HandleAsync(GetYearCalendarRequest req, CancellationToken ct)
    {
        var year = await dbContext
            .Years.Include(x => x.Calendar)
            .FirstOrDefaultAsync(x => x.Id == req.Id, ct);

        if (year == null)
        {
            logger.LogWarning("Year with Id {Id} not found.", req.Id);
            await SendNotFoundAsync(ct);
            return;
        }

        if (year.Calendar == null)
        {
            logger.LogError("Year with Id {Id} has no calendar!", req.Id);
            ThrowError($"Year with Id {req.Id} has no calendar!");
        }

        await SendMapped(year.Calendar, ct: ct);
    }
}
