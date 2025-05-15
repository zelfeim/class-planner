namespace Core.Domain.Entity;

public class Group
{
    public int Id { get; set; }
    public int YearId { get; set; }
    public ICollection<Class> Classes { get; set; } = [];
}