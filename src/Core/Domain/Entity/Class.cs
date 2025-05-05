namespace Core.Domain.Entity;

public class Class
{
    public required string Name { get; set; }
    public TimeSpan Length { get; set; }
    
    public required Classroom Classroom { get; set; } 
    public required Lecturer Lecturer { get; set; }
    public required Group Group { get; set; }
    
    public required Course Course { get; set; }
}