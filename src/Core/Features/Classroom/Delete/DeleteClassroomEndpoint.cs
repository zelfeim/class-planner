using Core.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.Extensions.Logging;

namespace Core.Features.Classroom.Delete;

[HttpDelete("/api/classroom/{id:int}")]
public class DeleteClassroomEndpoint(ILogger<DeleteClassroomEndpoint> logger, ApplicationDbContext dbContext) : Endpoint<DeleteClassroomRequest>
{
    public override async Task HandleAsync(DeleteClassroomRequest req, CancellationToken ct)
    {
        logger.LogInformation("Deleting classroom with Id {Id}.", req.Id);

        var classroom = await dbContext.Classrooms.FindAsync([req.Id], cancellationToken: ct);

        if (classroom == null)
        {
            logger.LogWarning("Classroom with Id {Id} not found.", req.Id);
            await SendNotFoundAsync(ct);
            return;
        }
        
        dbContext.Classrooms.Remove(classroom);
        await dbContext.SaveChangesAsync(ct);
        
        await SendNoContentAsync(ct);
    }
}