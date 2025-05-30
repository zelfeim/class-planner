using Core.Domain.Entity;
using Core.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.AspNetCore.Routing;
using Group = Core.Domain.Entity.Group;

namespace Core.Features.Year.AddYearGroup;

[HttpPost("api/year/{yearId:int}/group")]
public class CreateYearGroupEndpoint(ApplicationDbContext dbContext)
    : Endpoint<CreateYearGroupRequest>
{
    public override async Task HandleAsync(CreateYearGroupRequest req, CancellationToken ct)
    {
        var year = await dbContext.Years.FindAsync([req.YearId], ct);

        if (year == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var group = new Group()
        {
            YearId = req.YearId,
            Type = req.Type,
            Classification = req.Classification,
        };

        await dbContext.Groups.AddAsync(group, ct);
        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}

public record CreateYearGroupRequest
{
    public int YearId { get; init; }
    public GroupType Type { get; init; }
    public required string Classification { get; init; }
}
