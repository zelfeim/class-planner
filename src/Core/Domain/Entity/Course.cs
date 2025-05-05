namespace Core.Domain.Entity;

public class Course
{
    public int Id { get; set; }
    public required string Name { get; set; }
    
    // TODO: Hours for stationary and non-stationary
    public uint Hours { get; set; } = 0;
    
    public ICollection<Class> Classes { get; set; } = [];
}