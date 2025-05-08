using Core.Features.Calendar.Services;
using Core.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Features.Calendar.MoveEvent;

[HttpPut("/api/calendar/{calendarId:int}/move-event/{eventId:int}")]
public class Endpoint(Logger<Endpoint> logger, CalendarService calendarService) : Endpoint<Request>
{
    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        logger.LogInformation("Updating existing event.");
        
        await calendarService.MoveEventAsync(req.CalendarId, req.EventId, req.StartTime, req.EndTime);
        
        await SendNoContentAsync(ct);
    }
}