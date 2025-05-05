namespace Core.Domain.Entity;

public class Calendar
{
    public required Year Year { get; set; }
    public ICollection<Event> Events { get; set; } = [];
}