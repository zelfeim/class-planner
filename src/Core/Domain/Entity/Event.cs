namespace Core.Domain.Entity;

public abstract class Event
{
    public int Id { get; set; }
    public int CalendarId { get; set; }
    
    public required DateTime StartTime { get; set; }
    public required DateTime EndTime { get; set; }
    
    public required Calendar Calendar { get; init; }
}