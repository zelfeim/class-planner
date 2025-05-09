using Core.Features.Calendar.Services;
using Core.Features.Calendar.Services.Interfaces;
using Core.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.Extensions.Logging;

namespace Core.Features.Calendar.DeleteEvent;

[HttpDelete("/api/calendar/{calendarId:int}/event/{eventId:int}")]
public class Endpoint(ILogger<Endpoint> logger, ICalendarService calendarService) : Endpoint<Request>
{
    public override Task HandleAsync(Request req, CancellationToken ct)
    {
        logger.LogInformation("Deleting event with Id {EventId}. from calendar with Id {CalendarId}.", req.EventId, req.CalendarId);;
        
        calendarService.DeleteEvent(req.CalendarId, req.EventId);
        
        return SendNoContentAsync(ct);
    }
}