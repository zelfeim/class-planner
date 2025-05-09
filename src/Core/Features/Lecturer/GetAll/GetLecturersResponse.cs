using Core.Features.Lecturer.GetSingle;

namespace Core.Features.Lecturer.GetMultiple;

public class GetLecturersResponse
{
    public List<GetLecturerResponse> Lecturers { get; set; } = []; 
}