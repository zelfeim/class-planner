using FastEndpoints;

namespace Core.Features.Course.GetSingle;

public class GetCourseMapper : Mapper<GetCourseRequest, GetCourseResponse, Domain.Entity.Course>
{
    public override GetCourseResponse FromEntity(Domain.Entity.Course e)
    {
        return new GetCourseResponse()
        {
            Id = e.Id,
            Name = e.Name,
            Hours = e.Hours,
        };
    }
}