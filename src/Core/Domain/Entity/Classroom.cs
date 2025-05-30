namespace Core.Domain.Entity;

public class Classroom
{
    public int Id { get; set; }

    public required string Number { get; set; }

    public ICollection<Class> Classes { get; set; } = [];
}
