namespace Core.Features.Calendar.DeleteEvent;

public record Request
{
    public int CalendarId { get; init; }
    public int EventId { get; init; }
}