using Core.Features.Calendar.Services;
using Core.Features.Calendar.Services.Interfaces;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Features.Calendar.GetEvents;

[HttpGet("/api/calendar/{id:int}/events")]
public class Endpoint(ILogger<Endpoint> logger, ICalendarService calendarService) : Endpoint<int, List<Response>, EventMapper>
{
    public override async Task HandleAsync(int req, CancellationToken ct)
    {
        logger.LogInformation("Getting events for calendar with Id {CalendarId}.", req);
        
        var events = await calendarService.GetEventsAsync(req);
        var response = events.Select(Map.FromEntity).ToList(); 
        
        await SendAsync(response, cancellation: ct);
    }
}