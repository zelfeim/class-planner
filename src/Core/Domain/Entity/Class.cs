namespace Core.Domain.Entity;

public class Class
{
    public required string Name { get; init; }
    public TimeSpan Length { get; init; }
    
    public required Classroom Classroom { get; init; } 
    public required Lecturer Lecturer { get; init; }
    public required Group Group { get; init; }
    
    public required Course Course { get; init; }
}