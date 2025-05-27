namespace Core.Features.Year.GetYearCalendar;

public record GetYearCalendarResponse
{
    public required int Id { get; init; }
    public List<EventDto> Events { get; init; } = [];
}

public record EventDto
{
    public required int Id { get; init; }
    public required DateTime StartTime { get; init; }
    public required DateTime EndTime { get; init; }
    public required string Title { get; init; } = "";
    public int? ClassId { get; init; }
}
