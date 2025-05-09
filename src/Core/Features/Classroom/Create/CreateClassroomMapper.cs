using FastEndpoints;

namespace Core.Features.Classroom.Create;

public class CreateClassroomMapper : RequestMapper<CreateClassroomRequest, Domain.Entity.Classroom>
{
    public override Domain.Entity.Classroom ToEntity(CreateClassroomRequest r)
    {
        return new Domain.Entity.Classroom
        {
            Number = r.Number,
        };
    }
}