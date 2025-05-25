using Core.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.Extensions.Logging;

namespace Core.Features.Course.Delete;

[HttpDelete("/api/course/{id:int}")]
public class DeleteCourseEndpoint(ILogger<DeleteCourseEndpoint> logger, ApplicationDbContext dbContext) : Endpoint<DeleteCourseRequest>
{
    public override async Task HandleAsync(DeleteCourseRequest req, CancellationToken ct)
    {
        logger.LogInformation("Deleting course with Id {Id}.", req.Id);

        var course = await dbContext.Courses.FindAsync([req.Id], cancellationToken: ct);

        if (course == null)
        {
            logger.LogWarning("Course with Id {Id} not found.", req.Id);
            await SendNotFoundAsync(ct);
            return;
        }
        
        dbContext.Courses.Remove(course);
        await dbContext.SaveChangesAsync(ct);
        
        await SendNoContentAsync(ct);
    }
}