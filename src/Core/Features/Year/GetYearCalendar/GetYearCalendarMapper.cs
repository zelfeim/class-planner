using Core.Domain.Entity;
using FastEndpoints;

namespace Core.Features.Year.GetYearCalendar;

public class GetYearCalendarMapper
    : Mapper<GetYearCalendarRequest, GetYearCalendarResponse, Domain.Entity.Calendar>
{
    public override GetYearCalendarResponse FromEntity(Domain.Entity.Calendar calendar)
    {
        return new GetYearCalendarResponse
        {
            Id = calendar.Id,
            Events = calendar
                .Events.Select(x => new EventDto
                {
                    Id = x.Id,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    Title = x.Title,
                    ClassId = x is ClassEvent classEvent ? classEvent.Class.Id : null,
                })
                .ToList(),
        };
    }
}
