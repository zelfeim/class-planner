using Core.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.Extensions.Logging;

namespace Core.Features.Classroom.GetSingle;

[HttpGet("/api/classroom/{id:int}")]
public class GetClassroomEndpoint(ILogger<GetClassroomEndpoint> logger, ApplicationDbContext dbContext) : Endpoint<GetClassroomRequest, GetClassroomResponse, GetClassroomMapper>
{
    public override async Task HandleAsync(GetClassroomRequest req, CancellationToken ct)
    {
        logger.LogInformation("Getting classroom with Id {Id}.", req.Id); 
        
        var classroom  = await dbContext.Classrooms.FindAsync([req.Id], cancellationToken: ct);
        
        if (classroom == null)
        {
            Logger.LogWarning("Classroom with Id {Id} not found.", req.Id);
            await SendNotFoundAsync(ct);
            return;
        }
        
        await SendMappedAsync(classroom, ct: ct);
    }
}