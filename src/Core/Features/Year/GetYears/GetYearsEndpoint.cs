using Core.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Year.GetYears;

[HttpGet("api/year")]
public class GetYearsEndpoint(ApplicationDbContext dbContext)
    : EndpointWithoutRequest<GetYearsResponse>
{
    public override async Task HandleAsync(CancellationToken ct)
    {
        var years = await dbContext.Years.ToListAsync(cancellationToken: ct);

        await SendAsync(new GetYearsResponse() { Years = years }, cancellation: ct);
    }
}

public class GetYearsResponse
{
    public List<Domain.Entity.Year> Years { get; set; } = [];
}
