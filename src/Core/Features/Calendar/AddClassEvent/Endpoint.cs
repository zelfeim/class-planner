using Core.Domain.Entity;
using Core.Features.Calendar.Services;
using Core.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Features.Calendar.AddClassEvent;

[HttpPost("api/calendar/{calendarId}/add-class/{classId}")]
public class Endpoint(
    Logger<Endpoint> logger,
    CalendarService calendarService
    ) : Endpoint<Request, int>
{
    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        logger.LogInformation("Add class occurence to calendar.");

        var id = await calendarService.AddEventAsync(req, ct);
        
        await SendAsync(id, cancellation: ct);
    } 
}
