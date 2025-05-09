using Core.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Features.Year.GetYearGroups;

[HttpGet("/api/year/{yearId:int}/groups")]
public class GetYearGroupsEndpoint(ILogger<GetYearGroupsEndpoint> logger, ApplicationDbContext dbContext) : Endpoint<GetYearGroupsRequest, GetYearGroupsResponse, GetYearGroupMapper>
{
    public override async Task HandleAsync(GetYearGroupsRequest req, CancellationToken ct)
    {
        logger.LogInformation("Getting groups for year with Id {YearId}.", req.YearId);

        var year = await dbContext.Years.Include(x => x.Groups).FirstOrDefaultAsync(x => x.Id == req.YearId, cancellationToken: ct);

        if (year == null)
        {
            logger.LogWarning("Year with Id {YearId} not found.", req.YearId);
            await SendNotFoundAsync(ct);
            return;       
        }

        var groups = year.Groups.ToList();
        
        await SendMappedAsync(groups, ct: ct);
    }
}
