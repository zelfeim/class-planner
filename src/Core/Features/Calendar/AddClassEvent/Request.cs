namespace Core.Features.Calendar.AddClassEvent;

public class Request
{
    public int CalendarId { get; init; }
    public int ClassId { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
}