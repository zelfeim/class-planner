using Core.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Features.Class.GetClasses;

[HttpGet("api/classes")]
public class GetClassesEndpoint(ILogger<GetClassesEndpoint> logger, ApplicationDbContext dbContext)
    : EndpointWithoutRequest<GetClassesResponse, GetClassesMapper>
{
    public override async Task HandleAsync(CancellationToken ct)
    {
        logger.LogInformation("Getting all classes.");

        var classes = await dbContext
            .Classes.Include(x => x.Classroom)
            .Include(x => x.Lecturer)
            .Include(x => x.Group)
            .ToListAsync(cancellationToken: ct);

        await SendMapped(classes, ct: ct);
    }
}

public class GetClassesMapper : ResponseMapper<GetClassesResponse, List<Domain.Entity.Class>>
{
    public override GetClassesResponse FromEntity(List<Domain.Entity.Class> e)
    {
        return new GetClassesResponse()
        {
            Classes = e.Select(x => new ClassDto()
                {
                    Name = x.Name,
                    Length = x.Length,
                    ClassroomId = x.ClassroomId,
                    Classroom = x.Classroom,
                    LecturerId = x.LecturerId,
                    Lecturer = x.Lecturer,
                    GroupId = x.GroupId,
                    Group = x.Group,
                })
                .ToList(),
        };
    }
}

public record GetClassesResponse
{
    public List<ClassDto> Classes { get; init; } = [];
}

public record ClassDto
{
    public required string Name { get; init; }
    public uint Length { get; init; }
    public int ClassroomId { get; init; }
    public required Domain.Entity.Classroom Classroom { get; init; }
    public int LecturerId { get; init; }
    public required Domain.Entity.Lecturer Lecturer { get; init; }
    public int GroupId { get; init; }
    public required Domain.Entity.Group Group { get; init; }
}
