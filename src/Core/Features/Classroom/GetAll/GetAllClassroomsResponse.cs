using Core.Features.Classroom.GetSingle;

namespace Core.Features.Classroom.GetAll;

public record GetAllClassroomsResponse
{
    public List<GetClassroomResponse> Classrooms { get; init; } = [];
}