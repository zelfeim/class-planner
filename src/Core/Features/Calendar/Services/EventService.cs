using Core.Domain.Entity;

namespace Core.Features.Calendar.Services;

public class EventService
{
    public ClassEvent CreateClassEvent()
    {
        return new ClassEvent();
    }
}