using FastEndpoints;

namespace Core.Features.Classroom.GetSingle;

public class GetClassroomMapper : Mapper<GetClassroomRequest, GetClassroomResponse, Domain.Entity.Classroom>
{
    public override GetClassroomResponse FromEntity(Domain.Entity.Classroom e)
    {
        return new GetClassroomResponse()
        {
            Id = e.Id, 
            Number = e.Number,
        }; 
    }
    
}