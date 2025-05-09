namespace Core.Domain.Entity;

public class Year
{
    public int Id { get; set; }
    public ICollection<Group> Groups { get; set; } = [];
    public ICollection<Student> Students { get; set; } = [];
    public Calendar? Calendar { get; set; }
}