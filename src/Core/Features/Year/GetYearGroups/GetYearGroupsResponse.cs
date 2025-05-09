namespace Core.Features.Year.GetYearGroups;

public record GetYearGroupsResponse
{
    public List<GroupResponse> Groups { get; init; } = [];
}