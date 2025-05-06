using Core.Domain.Entity;
using Core.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.Extensions.Logging;

namespace Core.Features.Calendar.AddClassEvent;

[HttpPost("api/calendar/add-class/{classId}")]
public class Endpoint(
    Logger<Endpoint> logger,
    ApplicationDbContext dbContext
    ) : Endpoint<Request, int>
{
    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        logger.LogInformation("Add class occurence to calendar.");

        var calendar = await dbContext.Calendars.FindAsync([req.CalendarId], cancellationToken: ct);

        if (calendar == null)
        {
            logger.LogWarning("Calendar with id {Id} was not found.", req.CalendarId);
            await SendNotFoundAsync(ct);     
        }
        
        var classInstance = await dbContext.Classes.FindAsync([req.ClassId], cancellationToken: ct);

        if (classInstance == null)
        {
            logger.LogWarning("Class with id {Id} was not found.", req.ClassId);
            await SendNotFoundAsync(ct); 
        }
        
        var allEvents = dbContext.Events.Where(x => x.CalendarId == req.CalendarId);
        
        var dayEvents = allEvents.OfType<DayEvent>();
        var classEvents = allEvents.OfType<ClassEvent>();
        
        // TODO: Implement entire validation there
        // Class occurence validation
        // Day events validation
        // Lecturer availability validation
        // Student availability validation ?
        // Classroom availability validation
        
        // Validate that class occurence does not overlap with another class occurence
        if (classEvents.WhereOverlapping(req.StartTime, req.EndTime).Any())
        {
            logger.LogWarning("Class occurence overlaps with another class occurence");
            AddError("Class occurence overlaps with another class occurence"); 
        }
        
        // Validate that the lecturer does not have another class occurence at the same time
        var lecturerEvents = classEvents.Where(x => x.Class.Lecturer == classInstance!.Lecturer);;
        if (lecturerEvents.WhereOverlapping(req.StartTime, req.EndTime).Any())
        {
            logger.LogWarning("Lecturer has another class occurence at the same time");
            AddError("Lecturer has another class occurence at the same time");
        }
        
        // Validate that the classroom does not have another class occurence at the same time
        var classroomEvents = classEvents.Where(x => x.Class.Classroom == classInstance!.Classroom);
        if (classroomEvents.WhereOverlapping(req.StartTime, req.EndTime).Any())
        {
            logger.LogWarning("Classroom has another class occurence at the same time"); 
            AddError("Classroom has another class occurence at the same time");
        }

        // Validate that day events do not overlap with class occurence
        if (dayEvents.WhereOverlapping(req.StartTime, req.EndTime).Any())
        {
            logger.LogWarning("Day events overlap with class occurence");
            AddError("Day events overlap with class occurence");
        }
        
        ThrowIfAnyErrors();

        var classOccurence = new ClassEvent(calendar!, classInstance!, req.StartTime, req.EndTime);
        
        dbContext.Events.Add(classOccurence);
        
        await SendAsync(classOccurence.Id, cancellation: ct);
    } 
}

public static class DateTimeOverlapExtensions
{
    public static IQueryable<Event> WhereOverlapping(this IQueryable<Event> query, DateTime start, DateTime end)
    {
        return query.Where(x =>
            (x.StartTime <= start && start < x.EndTime) ||     // New event starts during existing
            (x.StartTime < end && end <= x.EndTime) ||         // New event ends during existing
            (start <= x.StartTime && x.EndTime <= end));       // New event completely contains existing
    }
}