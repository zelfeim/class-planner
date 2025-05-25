namespace Core.Features.Course.Delete;

public class DeleteCourseRequest
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required int Hours { get; init; }
}