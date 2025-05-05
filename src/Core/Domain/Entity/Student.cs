namespace Core.Domain.Entity;

public class Student : Person
{
    public ICollection<Set> Sets { get; set; } = [];
}