namespace Core.Features.Calendar.MoveEvent;

public record Request
{
    public int CalendarId { get; init; }
    public int EventId { get; init; }
    
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
}