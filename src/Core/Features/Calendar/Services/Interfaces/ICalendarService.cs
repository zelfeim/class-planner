using Core.Domain.Entity;

namespace Core.Features.Calendar.Services.Interfaces;

public interface ICalendarService
{
    public Task<List<Event>> GetEventsAsync(int id);

    public Task<Event> GetEventAsync(int calendarId, int eventId);

    public Task<int> AddClassEventAsync(AddClassEvent.Request req, CancellationToken ct);

    public void DeleteEvent(int calendarId, int eventId);

    public Task MoveEventAsync(
        int calendarId,
        int eventId,
        DateTime newStartTime,
        DateTime newEndTime
    );
}
