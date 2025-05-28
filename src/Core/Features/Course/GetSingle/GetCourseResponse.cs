namespace Core.Features.Course.GetSingle;

public class GetCourseResponse
{
    public int Id { get; init; }
    public string Name { get; init; }
    public required uint Hours { get; init; }
}