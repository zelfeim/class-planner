using Core.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.Extensions.Logging;

namespace Core.Features.Classroom.Create;

public class CreateClassroomEndpoint(ILogger<CreateClassroomEndpoint> logger, ApplicationDbContext dbContext) : EndpointWithMapper<CreateClassroomRequest, CreateClassroomMapper>
{
    public override async Task HandleAsync(CreateClassroomRequest req, CancellationToken ct)
    {
        logger.LogInformation("Adding new classroom."); 
        
        var newClassroom = Map.ToEntity(req);

        if (dbContext.Classrooms.Any(x => x.Number == newClassroom.Number))
        {
            logger.LogWarning("Classroom with this number already exists.");
            ThrowError("Classroom with this number already exists.");
        }
        
        await dbContext.Classrooms.AddAsync(Map.ToEntity(req), ct);
        await dbContext.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}