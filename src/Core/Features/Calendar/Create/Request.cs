namespace Core.Features.Calendar.Create;

public record Request
{
    public required string YearId { get; init; }
}