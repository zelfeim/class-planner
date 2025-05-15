namespace Core.Features.Lecturer.GetSingle;

public record GetLecturerResponse
{
    public int Id { get; init; }
    public required string Email { get; init; }
}