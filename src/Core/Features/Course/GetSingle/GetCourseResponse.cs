namespace Core.Features.Course.GetSingle;

public class GetCourseResponse
{
    public string Name { get; init; }
    public required uint Hours { get; init; }
}