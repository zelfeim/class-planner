using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Entity;

public class Group
{
    public int Id { get; set; }
    public GroupType Type { get; set; }
    [StringLength(10)]
    public required string Classification { get; set; }
    public int YearId { get; set; }
    public ICollection<Class> Classes { get; set; } = [];
}

public enum GroupType
{
    Lecture,
    Seminary,
    Laboratory
}