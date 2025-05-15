using FastEndpoints;
using Group = Core.Domain.Entity.Group;

namespace Core.Features.Year.GetYearGroups;

public class GetYearGroupMapper : Mapper<GetYearGroupsRequest, GetYearGroupsResponse, List<Domain.Entity.Group>>
{
    public override GetYearGroupsResponse FromEntity(List<Group> e)
    {
        var groups = e.Select(x => new GroupResponse()
        {
            Classes = x.Classes.Select(c => new ClassResponse
            {
                Lecturer = c.Lecturer.Email,
                Name = c.Name,
                Classroom = c.Classroom.Number,
                LectureId = c.LecturerId,
                CourseId = c.CourseId,
                Length = c.Length,
            }).ToList()
        }).ToList();

        return new GetYearGroupsResponse()
        {
            Groups = groups
        };
    }
}

// TODO: Should be in different place and named differently, maybe just Models?
public record GroupResponse
{
    public List<ClassResponse> Classes { get; init; } = [];
}

public record ClassResponse
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public TimeSpan Length { get; init; }
    public required string Classroom { get; init; }
    public required int LectureId { get; init; }
    public required string Lecturer { get; init; }
    public required int CourseId { get; init; }
}
