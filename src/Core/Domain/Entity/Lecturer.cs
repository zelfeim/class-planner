namespace Core.Domain.Entity;

public class Lecturer : Person
{
    public int Id { get; set; }
    public Class? Class { get; set; }
}