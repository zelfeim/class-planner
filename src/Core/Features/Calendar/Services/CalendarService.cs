using Core.Domain.Entity;
using Core.Features.Calendar.Services.Interfaces;
using Core.Infrastructure.Persistence;
using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Features.Calendar.Services;

public class CalendarService(ILogger<CalendarService> logger, ApplicationDbContext dbContext)
    : ICalendarService
{
    // Should be independent of the application type

    public async Task<List<Event>> GetEventsAsync(int id)
    {
        var validationContext = ValidationContext.Instance;

        var calendar = await dbContext
            .Calendars.Include(calendar => calendar.Events)
            .ThenInclude(e => ((ClassEvent)e).Class)
            .FirstOrDefaultAsync(x => x.Id == id);

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
        var validationContext = ValidationContext.Instance;

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
    public async Task<int> AddClassEventAsync(AddClassEvent.Request req, CancellationToken ct)
    {
        var validationContext = ValidationContext.Instance;

        var calendar = await dbContext.Calendars.FindAsync([req.CalendarId], cancellationToken: ct);

        #region EventCreation

        // TODO: Should somehow return Result which creates NotFound response with code 404
        if (calendar == null)
        {
            logger.LogWarning("Calendar with id {Id} was not found.", req.CalendarId);
            validationContext.ThrowError($"Calendar with id {req.CalendarId} was not found.");
        }

        var classInstance = await dbContext
            .Classes.Include(x => x.Lecturer)
            .Include(x => x.Classroom)
            .FirstOrDefaultAsync(x => x.Id == req.ClassId, cancellationToken: ct);

        if (classInstance == null)
        {
            logger.LogWarning("Class with id {Id} was not found.", req.ClassId);
            validationContext.ThrowError($"Class with id {req.ClassId} was not found.");
        }

        var newClassEvent = new ClassEvent
        {
            Calendar = calendar,
            Class = classInstance,
            StartTime = req.StartTime,
            EndTime = req.EndTime,
            Title = classInstance.Name,
        };

        #endregion

        #region Validation

        await ValidateCalendarCollisions(
            calendar.Id,
            req.StartTime,
            req.EndTime,
            classInstance,
            classInstance.Lecturer,
            classInstance.Classroom
        );

        validationContext.ThrowIfAnyErrors();

        #endregion

        await dbContext.ClassEvents.AddAsync(newClassEvent, ct);
        await dbContext.SaveChangesAsync(ct);

        return newClassEvent.Id;
    }

    public void DeleteEvent(int calendarId, int eventId)
    {
        var foundEvent = GetEventAsync(calendarId, eventId);

        dbContext.Remove(foundEvent);
        dbContext.SaveChanges();
    }

    public async Task MoveEventAsync(
        int calendarId,
        int eventId,
        DateTime newStartTime,
        DateTime newEndTime
    )
    {
        var validationContext = ValidationContext.Instance;

        var selectedEvent = await GetEventAsync(calendarId, eventId);

        if (selectedEvent is ClassEvent classEvent)
        {
            await ValidateClassCollisionsAsync(
                calendarId,
                newStartTime,
                newEndTime,
                classEvent.Class
            );
            await ValidateSpecialDaysCollisionsAsync(calendarId, newStartTime, newEndTime);
        }

        validationContext.ThrowIfAnyErrors();

        selectedEvent.StartTime = newStartTime;
        selectedEvent.EndTime = newEndTime;

        await dbContext.SaveChangesAsync();
    }

    private async Task ValidateCalendarCollisions(
        int calendarId,
        DateTime startTime,
        DateTime endTime,
        Domain.Entity.Class @class,
        Domain.Entity.Lecturer lecturer,
        Domain.Entity.Classroom classroom
    )
    {
        await ValidateClassCollisionsAsync(calendarId, startTime, endTime, @class);
        await ValidateSpecialDaysCollisionsAsync(calendarId, startTime, endTime);
        await ValidateLecturerAvailabilityAsync(calendarId, startTime, endTime, lecturer);
        await ValidateClassroomAvailabilityAsync(calendarId, startTime, endTime, classroom);
    }

    // Should do something to not pass calendarId to method...
    // Such a validator should work in a calendar class context (e.g., we are inside a calendar for the 3rd year)
    private async Task ValidateClassCollisionsAsync(
        int calendarId,
        DateTime startTime,
        DateTime endTime,
        Domain.Entity.Class @class
    )
    {
        var validationContext = ValidationContext.Instance;

        var events = await GetEventsAsync(calendarId);
        var classEvents = events.OfType<ClassEvent>().ToList();

        var sameClassEvents = classEvents.Where(x => x.Class.Id == @class.Id);

        if (sameClassEvents.WhereOverlapping(startTime, endTime).Any())
        {
            logger.LogWarning("Class occurence overlaps with another class occurence");
            validationContext.AddError("Class occurence overlaps with another class occurence");
        }
    }

    private async Task ValidateSpecialDaysCollisionsAsync(
        int calendarId,
        DateTime startTime,
        DateTime endTime
    )
    {
        var validationContext = ValidationContext.Instance;

        var events = await GetEventsAsync(calendarId);
        var dayEvents = events.OfType<DayEvent>().ToList();

        if (dayEvents.WhereOverlapping(startTime, endTime).Any())
        {
            logger.LogWarning("Day events overlap with class occurence");
            validationContext.AddError("Day events overlap with class occurence");
        }
    }

    private async Task ValidateLecturerAvailabilityAsync(
        int calendarId,
        DateTime startTime,
        DateTime endTime,
        Domain.Entity.Lecturer lecturer
    )
    {
        var validationContext = ValidationContext.Instance;

        var events = await GetEventsAsync(calendarId);

        var classEvents = events.OfType<ClassEvent>().ToList();

        var lecturerEvents = classEvents.Where(x => x.Class.Lecturer == lecturer);

        if (lecturerEvents.WhereOverlapping(startTime, endTime).Any())
        {
            logger.LogWarning("Lecturer has another class occurence at the same time");
            validationContext.AddError("Lecturer has another class occurence at the same time");
        }
    }

    private async Task ValidateClassroomAvailabilityAsync(
        int calendarId,
        DateTime startTime,
        DateTime endTime,
        Domain.Entity.Classroom classroom
    )
    {
        var validationContext = ValidationContext.Instance;

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
    public static IEnumerable<Event> WhereOverlapping(
        this IEnumerable<Event> query,
        DateTime startTime,
        DateTime endTime
    )
    {
        return query.Where(x =>
            (x.StartTime <= startTime && startTime < x.EndTime)
            || // New event starts during existing
            (x.StartTime < endTime && endTime <= x.EndTime)
            || // New event ends during existing
            (startTime <= x.StartTime && x.EndTime <= endTime)
        ); // New event completely contains existing
    }
}
