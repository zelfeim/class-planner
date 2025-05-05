namespace Core.Domain.Entity;

public class Calendar
{
    public required Year Year { get; set; }
    public List<Event> Events { get; set; } = [];
}