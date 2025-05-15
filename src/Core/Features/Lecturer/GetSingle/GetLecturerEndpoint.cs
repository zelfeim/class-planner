using Core.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.Extensions.Logging;

namespace Core.Features.Lecturer.GetSingle;

[HttpGet("/api/lecturer/{id:int}")]
public class GetLecturerEndpoint(ILogger<GetLecturerEndpoint> logger, ApplicationDbContext dbContext) : Endpoint<GetLecturerRequest, GetLecturerResponse, GetLecturerMapper>
{
    public override async Task HandleAsync(GetLecturerRequest req, CancellationToken ct)
    {
        logger.LogInformation("Getting lecturer with Id {Id}.", req.Id);
        
        var lecturer = await dbContext.Lecturers.FindAsync([req.Id], cancellationToken: ct);

        if (lecturer == null)
        {
            logger.LogWarning("Lecturer with Id {Id} not found.", req.Id);
            await SendNotFoundAsync(ct);
        }

        await SendMappedAsync(lecturer, ct: ct);
    }
}