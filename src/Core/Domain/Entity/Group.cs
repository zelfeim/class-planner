namespace Core.Domain.Entity;

public class Group : Set
{
    public ICollection<Class> Classes { get; set; } = [];
}