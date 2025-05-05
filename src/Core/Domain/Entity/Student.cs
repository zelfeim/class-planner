namespace Core.Domain.Entity;

public class Student : Person
{
    public int Id { get; set; }
    public ICollection<Set> Sets { get; set; } = [];
}