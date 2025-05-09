namespace Core.Domain.Entity;

public class Student : Person
{
    public int Id { get; set; }
    public int YearId { get; set; }
    public ICollection<Group> Groups { get; set; } = [];
}