using Core.Features.Calendar.Services;
using Core.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.Extensions.Logging;

namespace Core.Features.Calendar.DeleteEvent;

[HttpDelete("/api/calendar/{calendarId:int}/event/{eventId:int}")]
public class Endpoint(Logger<Endpoint> logger, CalendarService calendarService) : Endpoint<Request>
{
    public override Task HandleAsync(Request req, CancellationToken ct)
    {
        logger.LogInformation("Deleting event with Id {EventId}. from calendar with Id {CalendarId}.", req.EventId, req.CalendarId);;
        
        calendarService.DeleteEvent(req.CalendarId, req.EventId);
        
        return SendNoContentAsync(ct);
    }
}