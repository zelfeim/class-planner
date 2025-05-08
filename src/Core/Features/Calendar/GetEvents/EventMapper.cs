using Core.Domain.Entity;
using FastEndpoints;

namespace Core.Features.Calendar.GetEvents;

public class EventMapper : Mapper<int, Response, Event>
{
    public override Response FromEntity(Event e)
    {
        return new Response
        {
            Id = e.Id,
            StartTime = e.StartTime,
            EndTime = e.EndTime,
        };
    }
}