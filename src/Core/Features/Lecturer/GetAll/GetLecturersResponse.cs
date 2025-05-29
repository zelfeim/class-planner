using Core.Features.Lecturer.GetSingle;

namespace Core.Features.Lecturer.GetAll;

public class GetLecturersResponse
{
    public List<GetLecturerResponse> Lecturers { get; init; } = [];
}
