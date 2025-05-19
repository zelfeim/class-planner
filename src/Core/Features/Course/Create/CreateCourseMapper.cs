using FastEndpoints;

namespace Core.Features.Course.Create;

public class CreateCourseMapper : RequestMapper<CreateCourseRequest, Domain.Entity.Course>
{
    public override Domain.Entity.Course ToEntity(CreateCourseRequest r)
    {
        return new Domain.Entity.Course
        {
            Name = r.Name,
            Hours = r.Hours,
        };
    }
}