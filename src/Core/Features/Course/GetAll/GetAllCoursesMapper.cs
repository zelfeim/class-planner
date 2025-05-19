using Core.Features.Course.GetSingle;
using FastEndpoints;

namespace Core.Features.Course.GetAll;

public class GetAllCoursesMapper : ResponseMapper<GetAllCoursesResponse, List<Domain.Entity.Course>>
{
    public override GetAllCoursesResponse FromEntity(List<Domain.Entity.Course> e)
    {
        var getCoursesMapper = Resolve<GetCourseMapper>();
        return new GetAllCoursesResponse
        {
            Courses = e.Select(getCoursesMapper.FromEntity).ToList()
        };
    }
}