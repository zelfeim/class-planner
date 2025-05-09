using FastEndpoints;

namespace Core.Features.Lecturer.GetSingle;

public class GetLecturerMapper : Mapper<GetLecturerRequest, GetLecturerResponse, Domain.Entity.Lecturer>
{
    public override GetLecturerResponse FromEntity(Domain.Entity.Lecturer e)
    {
        return new GetLecturerResponse
        {
            Id = e.Id,
            Email = e.Email,
        };   
    }    
}