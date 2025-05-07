namespace Core.Domain.Entity;

public class ClassEvent : Event
{
    public required Class Class { get; set; }
}