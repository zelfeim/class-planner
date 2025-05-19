using Core.Features.Course.GetSingle;

namespace Core.Features.Course.GetAll;

public record GetAllCoursesResponse
{
    public List<GetCourseResponse> Courses { get; init; } = [];
}