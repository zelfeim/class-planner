namespace Core.Domain.Entity;

public class Class
{
    public int Id { get; set; }
    
    public required string Name { get; init; }
    public TimeSpan Length { get; init; }
    
    public int ClassroomId { get; init; }
    public required Classroom Classroom { get; init; } 
    public int LecturerId { get; init; }
    public required Lecturer Lecturer { get; init; }
    public int GroupId { get; init; }
    public required Group Group { get; init; }
    
    public int CourseId { get; init; }
    public required Course Course { get; init; }
}