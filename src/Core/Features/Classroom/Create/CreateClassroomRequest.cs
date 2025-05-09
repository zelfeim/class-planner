namespace Core.Features.Classroom.Create;

public record CreateClassroomRequest
{
    public required string Number { get; init; }
}