using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Entity;

public class Year
{
    public int Id { get; set; }
    [StringLength(10)]
    public required string Name { get; set; }
    public StudyMode Mode { get; set; }
    public ICollection<Group> Groups { get; set; } = [];
    public Calendar? Calendar { get; set; }
}

public enum StudyMode
{
    FullTime,
    PartTime
}