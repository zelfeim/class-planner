namespace Core.Domain.Entity;

public abstract class Event
{
    public required DateTime StartTime { get; set; }
    public required DateTime EndTime { get; set; }
    
    public required Calendar Calendar { get; init; }
}