using Core.Domain.Entity;
using FastEndpoints;
using Group = Core.Domain.Entity.Group;

namespace Core.Features.Year.GetYearGroups;

public class GetYearGroupMapper : Mapper<GetYearGroupsRequest, GetYearGroupsResponse, List<Group>>
{
    public override GetYearGroupsResponse FromEntity(List<Group> e)
    {
        var groups = e.Select(x => new GroupResponse
            {
                Id = x.Id,
                Type = x.Type,
                Classification = x.Classification,
            })
            .ToList();

        return new GetYearGroupsResponse { Groups = groups };
    }
}

// TODO: Should be in different place and named differently, maybe just Models?
public record GroupResponse
{
    public required int Id { get; init; }
    public GroupType Type { get; init; }
    public required string Classification { get; init; }
}
