namespace Core.Features.Course.Delete;

public class DeleteCourseRequest
{
    public required string Name { get; init; }
    public required int Hours { get; init; }
}