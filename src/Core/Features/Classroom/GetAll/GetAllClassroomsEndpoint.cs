using Core.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Features.Classroom.GetAll;

[HttpGet("/api/classroom")]
public class GetAllClassroomsEndpoint(
    ILogger<GetAllClassroomsEndpoint> logger,
    ApplicationDbContext dbContext
) : EndpointWithoutRequest<GetAllClassroomsResponse, GetAllClassroomsMapper>
{
    public override async Task HandleAsync(CancellationToken ct)
    {
        logger.LogInformation("Getting all classrooms.");

        var classrooms = await dbContext.Classrooms.ToListAsync(cancellationToken: ct);

        await SendMapped(classrooms, ct: ct);
    }
}
