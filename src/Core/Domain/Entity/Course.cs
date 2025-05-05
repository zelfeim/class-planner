namespace Core.Domain.Entity;

public class Course
{
    public required string Name { get; set; }
    
    // TODO: Hours for stationary and non-stationary
    public uint Hours { get; set; } = 0;
}