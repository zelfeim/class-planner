namespace Core.Domain.Entity;

public class Calendar
{
    public int Id { get; set; }
    public int YearId { get; set; }
    public required Year Year { get; set; }
    public ICollection<Event> Events { get; set; } = [];
}