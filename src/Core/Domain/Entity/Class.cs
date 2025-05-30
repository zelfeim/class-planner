namespace Core.Domain.Entity;

public class Class
{
    public int Id { get; set; }
    
    public required string Name { get; init; }
    public uint Length { get; init; }
    
    public int ClassroomId { get; init; }
    public Classroom Classroom { get; init; } 
    public int LecturerId { get; init; }
    public Lecturer Lecturer { get; init; }
    public int GroupId { get; init; }
    public Group Group { get; init; }
    
    public int CourseId { get; init; }
    public Course Course { get; init; }
}