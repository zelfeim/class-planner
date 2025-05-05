namespace Core.Domain.Entity;

public class Classroom
{
    public required string Number { get; set; }
    
    public Class? Class { get; set; }
}