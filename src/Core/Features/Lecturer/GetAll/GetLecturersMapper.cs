using Core.Features.Lecturer.GetMultiple;
using Core.Features.Lecturer.GetSingle;
using FastEndpoints;

namespace Core.Features.Lecturer.GetAll;

public class GetLecturersMapper : ResponseMapper<GetLecturersResponse, List<Domain.Entity.Lecturer>>
{
    public override GetLecturersResponse FromEntity(List<Domain.Entity.Lecturer> e)
    {
        var lecturerMapper = Resolve<GetLecturerMapper>();

        return new GetLecturersResponse
        {
            Lecturers = e.Select(lecturerMapper.FromEntity).ToList()
        };
    }
}