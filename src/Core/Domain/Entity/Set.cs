namespace Core.Domain.Entity;

public abstract class Set
{
    public int Id { get; set; }
    public List<Student> Students { get; set; } = [];
}