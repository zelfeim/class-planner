namespace Core.Domain.Entity;

public class Lecturer : Person
{
    public int Id { get; set; }
    public ICollection<Class> Classes { get; set; } = [];
}
