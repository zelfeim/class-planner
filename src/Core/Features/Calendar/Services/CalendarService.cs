using Core.Domain.Entity;
using Core.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Features.Calendar.Services;

public class CalendarService(
    Logger<CalendarService> logger,
    ApplicationDbContext dbContext,
    ValidationContext validationContext
    )
{
    // Should be independent of the application type
    
    public async Task<List<Event>> GetEventsAsync(int id)
    {
        var calendar = await dbContext.Calendars.Include(calendar => calendar.Events).FirstOrDefaultAsync(x => x.Id == id);

        if (calendar == null)
        {
            logger.LogWarning("Calendar with Id {CalendarId} not found.", id);
            validationContext.ThrowError($"Calendar with Id {id} not found.");
        }
        
        return calendar.Events.ToList();
    }

    // Should these events be optimized?
    // Idea to filter them by a timeslot (e.g., a week)
    public async Task<Event> GetEventAsync(int calendarId, int eventId)
    {
        var events = await GetEventsAsync(calendarId); 
        
        var foundEvent = events.FirstOrDefault(x => x.Id == eventId);

        // TODO: Should somehow return Result which creates NotFound response with code 404
        if (foundEvent == null)
        {
            logger.LogWarning("Event with Id {EventId} not found.", eventId);
            validationContext.ThrowError($"Event with Id {eventId} not found.");
        }
        
        return foundEvent;
    }

    // TODO: Verify whether Request class should be used there?
    // TODO: Event creation should be in event service
    public async Task<int> AddEventAsync(AddClassEvent.Request req, CancellationToken ct)
    {
        var calendar = await dbContext.Calendars.FindAsync([req.CalendarId], cancellationToken: ct);

        #region EventCreation

        // TODO: Should somehow return Result which creates NotFound response with code 404
        if (calendar == null)
        {
            logger.LogWarning("Calendar with id {Id} was not found.", req.CalendarId);
            validationContext.ThrowError($"Calendar with id {req.CalendarId} was not found.");
        }
        
        var classInstance = await dbContext.Classes
            .Include(x => x.Lecturer)
            .Include(x => x.Classroom)
            .FirstOrDefaultAsync(x => x.Id == req.ClassId, cancellationToken: ct);

        if (classInstance == null)
        {
            logger.LogWarning("Class with id {Id} was not found.", req.ClassId);
            validationContext.ThrowError($"Class with id {req.ClassId} was not found.");
        }
        
        var newClassEvent = new ClassEvent(calendar, classInstance, req.StartTime, req.EndTime);

        #endregion

        #region Validation
        
        await ValidateCalendarCollisions(calendar.Id, req.StartTime, req.EndTime, classInstance.Lecturer, classInstance.Classroom);
        
        validationContext.ThrowIfAnyErrors();

        #endregion

        dbContext.Events.Add(newClassEvent);
        
        return newClassEvent.Id;
    }

    public void DeleteEvent(int calendarId, int eventId)
    {
        var foundEvent = GetEventAsync(calendarId, eventId);
        
        dbContext.Remove(foundEvent);
    }

    public async Task MoveEventAsync(int calendarId, int eventId, DateTime newStartTime, DateTime newEndTime)
    {
        var selectedEvent = await GetEventAsync(calendarId, eventId);
        
        await ValidateClassCollisionsAsync(calendarId, newStartTime, newEndTime);
        await ValidateSpecialDaysCollisionsAsync(calendarId, newStartTime, newEndTime);
        
        validationContext.ThrowIfAnyErrors();
        
        selectedEvent.StartTime = newStartTime;
        selectedEvent.EndTime = newEndTime;

        await dbContext.SaveChangesAsync();
    }
    
    private async Task ValidateCalendarCollisions(int calendarId, DateTime startTime, DateTime endTime, Domain.Entity.Lecturer lecturer, Domain.Entity.Classroom classroom)
    {
        var classCollisionTask = ValidateClassCollisionsAsync(calendarId, startTime, endTime);
        var specialDaysTask = ValidateSpecialDaysCollisionsAsync(calendarId, startTime, endTime);
        var lecturerAvailabilityTask = ValidateLecturerAvailabilityAsync(calendarId, startTime, endTime, lecturer);;
        var classroomAvailabilityTask = ValidateClassroomAvailabilityAsync(calendarId, startTime, endTime, classroom);
        
        await Task.WhenAll(classCollisionTask, specialDaysTask, lecturerAvailabilityTask, classroomAvailabilityTask); 
    }

    // Should do something to not pass calendarId to method...
    // Such a validator should work in a calendar class context (e.g., we are inside a calendar for the 3rd year)
    private async Task ValidateClassCollisionsAsync(int calendarId, DateTime startTime, DateTime endTime)
    {
        var events = await GetEventsAsync(calendarId);
        var classEvents = events.OfType<ClassEvent>().ToList();
        
        if (classEvents.WhereOverlapping(startTime, endTime).Any())
        {
            logger.LogWarning("Class occurence overlaps with another class occurence");
            validationContext.AddError("Class occurence overlaps with another class occurence"); 
        }
    }
    
    private async Task ValidateSpecialDaysCollisionsAsync(int calendarId, DateTime startTime, DateTime endTime)
    {
        var events = await GetEventsAsync(calendarId);
        var dayEvents = events.OfType<DayEvent>().ToList();
        
        if (dayEvents.WhereOverlapping(startTime, endTime).Any())
        {
            logger.LogWarning("Day events overlap with class occurence");
            validationContext.AddError("Day events overlap with class occurence");
        }
    }

    private async Task ValidateLecturerAvailabilityAsync(int calendarId, DateTime startTime, DateTime endTime, Domain.Entity.Lecturer lecturer)
    {
        var events = await GetEventsAsync(calendarId);
        
        var classEvents = events.OfType<ClassEvent>().ToList();
        
        var lecturerEvents = classEvents.Where(x => x.Class.Lecturer == lecturer);;
        if (lecturerEvents.WhereOverlapping(startTime, endTime).Any())
        {
            logger.LogWarning("Lecturer has another class occurence at the same time");
            validationContext.AddError("Lecturer has another class occurence at the same time");
        }
    }

    private async Task ValidateClassroomAvailabilityAsync(int calendarId, DateTime startTime, DateTime endTime,
        Domain.Entity.Classroom classroom)
    {
        var events = await GetEventsAsync(calendarId);
        
        var classEvents = events.OfType<ClassEvent>().ToList();
        
        var classroomEvents = classEvents.Where(x => x.Class.Classroom == classroom);
        if (classroomEvents.WhereOverlapping(startTime, endTime).Any())
        {
            logger.LogWarning("Classroom has another class occurence at the same time"); 
            validationContext.AddError("Classroom has another class occurence at the same time");
        }
    }
}

internal class EventCollision
{
    // TODO: Hold event collision information
}

internal static class DateTimeOverlapExtensions
{
    public static IEnumerable<Event> WhereOverlapping(this IEnumerable<Event> query, DateTime startTime, DateTime endTime)
    {
        return query.Where(x =>
            (x.StartTime <= startTime && startTime < x.EndTime) ||     // New event starts during existing
            (x.StartTime < endTime && endTime <= x.EndTime) ||         // New event ends during existing
            (startTime <= x.StartTime && x.EndTime <= endTime));       // New event completely contains existing
    }
}
