namespace Core.Domain.Entity;

public abstract class Event
{
    public int Id { get; set; }
    public int CalendarId { get; set; }
    
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    
    public Calendar Calendar { get; init; }
}