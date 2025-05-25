namespace Core.Features.Course.GetSingle;

public class GetCourseRequest
{
    public int Id { get; init; }
    public string Name { get; init; }
    public uint Hours { get; init; }
}