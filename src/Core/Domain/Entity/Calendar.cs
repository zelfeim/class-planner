namespace Core.Domain.Entity;

public class Calendar
{
    public Calendar() { }

    public Calendar(int yearId)
    {
        YearId = yearId;
    }

    public int Id { get; set; }
    public int YearId { get; set; }
    public Year Year { get; set; }
    public ICollection<Event> Events { get; set; } = [];
}
