using Core.Features.Classroom.GetSingle;
using FastEndpoints;

namespace Core.Features.Classroom.GetAll;

public class GetAllClassroomsMapper : ResponseMapper<GetAllClassroomsResponse, List<Domain.Entity.Classroom>>
{
    public override GetAllClassroomsResponse FromEntity(List<Domain.Entity.Classroom> e)
    {
        var getClassroomMapper = Resolve<GetClassroomMapper>();
        return new GetAllClassroomsResponse
        {
            Classrooms = e.Select(getClassroomMapper.FromEntity).ToList()
        };
    }
}