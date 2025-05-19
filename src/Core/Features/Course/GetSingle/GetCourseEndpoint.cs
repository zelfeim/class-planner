using Core.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.Extensions.Logging;

namespace Core.Features.Course.GetSingle;

[HttpGet("/api/course/{id:int}")]
public class GetCourseEndpoint(ILogger<GetCourseEndpoint> logger, ApplicationDbContext dbContext) : Endpoint<GetCourseRequest, GetCourseResponse, GetCourseMapper>
{
    public override async Task HandleAsync(GetCourseRequest req, CancellationToken ct)
    {
        logger.LogInformation("Getting course with Name {Name}.", req.Name); 
        
        var course = await dbContext.Courses.FindAsync([req.Name], cancellationToken: ct);
        
        if (course == null)
        {
            Logger.LogWarning("Course with Name {Name} not found.", req.Name);
            await SendNotFoundAsync(ct);
            return;
        }                                                                                                    
        
        await SendMappedAsync(course, ct: ct);
    }
}