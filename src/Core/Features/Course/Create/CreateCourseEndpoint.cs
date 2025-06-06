using Core.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.Extensions.Logging;

namespace Core.Features.Course.Create;

[HttpPost("/api/course")]
public class CreateCourseEndpoint(
    ILogger<CreateCourseEndpoint> logger,
    ApplicationDbContext dbContext
) : EndpointWithMapper<CreateCourseRequest, CreateCourseMapper>
{
    public override async Task HandleAsync(CreateCourseRequest req, CancellationToken ct)
    {
        logger.LogInformation("Adding new course.");

        var newCourse = Map.ToEntity(req);

        if (dbContext.Courses.Any(x => x.Id == newCourse.Id))
        {
            logger.LogWarning("Course with this id already exists.");
            ThrowError("Course with this id  already exists.");
        }

        await dbContext.Courses.AddAsync(Map.ToEntity(req), ct);
        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}
