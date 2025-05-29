using Core.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.Extensions.Logging;

namespace Core.Features.Class.DeleteClass;

[HttpDelete("api/classes/{id:int}")]
public class DeleteClassEndpoint(
    ILogger<DeleteClassEndpoint> logger,
    ApplicationDbContext dbContext
) : Endpoint<DeleteClassRequest>
{
    public override async Task HandleAsync(DeleteClassRequest req, CancellationToken ct)
    {
        logger.LogInformation("Delete class with id {Id}.", req.Id);

        var classToDelete = await dbContext.Classes.FindAsync([req.Id], cancellationToken: ct);

        if (classToDelete == null)
        {
            logger.LogWarning("Delete class with id {Id} was not found.", req.Id);
            await SendNotFoundAsync(ct);
            return;
        }

        dbContext.Classes.Remove(classToDelete);
        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}

public class DeleteClassRequest
{
    public int Id { get; set; }
}
