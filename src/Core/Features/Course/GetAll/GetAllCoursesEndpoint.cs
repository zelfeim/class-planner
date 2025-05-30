using Core.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Features.Course.GetAll;

[HttpGet("/api/course")]
public class GetAllCoursesEndpoint(
    ILogger<GetAllCoursesEndpoint> logger,
    ApplicationDbContext dbContext
) : EndpointWithoutRequest<GetAllCoursesResponse, GetAllCoursesMapper>
{
    public override async Task HandleAsync(CancellationToken ct)
    {
        logger.LogInformation("Getting all courses.");

        var courses = await dbContext.Classrooms.ToListAsync(cancellationToken: ct);

        await SendMapped(courses, ct: ct);
    }
}
