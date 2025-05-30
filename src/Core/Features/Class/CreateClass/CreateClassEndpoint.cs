using Core.Features.Classroom.Create;
using Core.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.Extensions.Logging;

namespace Core.Features.Class.CreateClass;

[HttpPost("api/class")]
public class CreateClassEndpoint(
    ILogger<CreateClassEndpoint> logger,
    ApplicationDbContext dbContext
) : Endpoint<CreateClassRequest, CreateClassResponse, CreateClassMapper>
{
    public override async Task HandleAsync(CreateClassRequest req, CancellationToken ct)
    {
        logger.LogInformation(
            "Creating new class for classroom {classroomId}, lecturer {lecturerId}, group {groupId}.",
            req.ClassroomId,
            req.LecturerId,
            req.GroupId
        );

        if (!dbContext.Courses.Any(x => x.Id == req.CourseId))
        {
            ThrowError("Course with this id does not exist.");
        }
        if (!dbContext.Lecturers.Any(x => x.Id == req.LecturerId))
        {
            ThrowError("Lecturer with this id does not exist.");
        }
        if (!dbContext.Groups.Any(x => x.Id == req.GroupId))
        {
            ThrowError("Group with this id does not exist.");
        }
        if (!dbContext.Classrooms.Any(x => x.Id == req.ClassroomId))
        {
            ThrowError("Classroom with this id does not exist.");
        }

        var classEntity = Map.ToEntity(req);

        await dbContext.Classes.AddAsync(classEntity, ct);

        await dbContext.SaveChangesAsync(ct);

        await SendMapped(classEntity, ct: ct);
    }
}

public record CreateClassResponse
{
    public int Id { get; init; }
}

public class CreateClassMapper
    : Mapper<CreateClassRequest, CreateClassResponse, Domain.Entity.Class>
{
    public override Domain.Entity.Class ToEntity(CreateClassRequest r)
    {
        return new Domain.Entity.Class()
        {
            Name = r.Name,
            ClassroomId = r.ClassroomId,
            LecturerId = r.LecturerId,
            CourseId = r.CourseId,
            GroupId = r.GroupId,
            Length = r.Length,
        };
    }

    public override CreateClassResponse FromEntity(Domain.Entity.Class entity)
    {
        return new CreateClassResponse() { Id = entity.Id };
    }
}

public record CreateClassRequest
{
    public required string Name { get; init; }
    public int ClassroomId { get; init; }
    public int CourseId { get; init; }
    public int LecturerId { get; init; }
    public int GroupId { get; init; }
    public uint Length { get; init; }
}
