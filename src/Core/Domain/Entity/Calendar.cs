namespace Core.Domain.Entity;

public class Calendar
{
    public Calendar() {}
    
    public Calendar(string yearId)
    {
        YearId = Convert.ToInt32(yearId);
    }

    public int Id { get; set; }
    public int YearId { get; set; }
    public Year Year { get; set; }
    public ICollection<Event> Events { get; set; } = [];
}