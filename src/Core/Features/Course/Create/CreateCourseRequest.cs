namespace Core.Features.Course.Create;

public record CreateCourseRequest
{
    public required int Id{ get; init; }
    public required string Name{ get; init; }
    public required uint Hours{ get; init; }

}