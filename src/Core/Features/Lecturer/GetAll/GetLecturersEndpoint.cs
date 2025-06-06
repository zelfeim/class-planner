using Core.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Features.Lecturer.GetAll;

[HttpGet("/api/lecturer")]
public class GetLecturersEndpoint(
    ILogger<GetLecturersEndpoint> logger,
    ApplicationDbContext dbContext
) : EndpointWithoutRequest<GetLecturersResponse, GetLecturersMapper>
{
    public override async Task HandleAsync(CancellationToken ct)
    {
        logger.LogInformation("Getting all lecturers.");

        var lecturers = await dbContext.Lecturers.ToListAsync(cancellationToken: ct);

        await SendMapped(lecturers, ct: ct);
    }
}
