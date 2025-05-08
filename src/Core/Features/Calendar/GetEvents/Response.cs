namespace Core.Features.Calendar.GetEvents;

public record Response
{
    public int Id { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
}