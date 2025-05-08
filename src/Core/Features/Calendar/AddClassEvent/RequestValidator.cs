using FastEndpoints;
using FluentValidation;

namespace Core.Features.Calendar.AddClassEvent;

public class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.CalendarId)
            .GreaterThan(0)
            .WithMessage("Calendar ID must be a positive number");

        RuleFor(x => x.ClassId)
            .GreaterThan(0)
            .WithMessage("Class ID must be a positive number");

        RuleFor(x => x.StartTime)
            .Must(startTime => startTime > DateTime.Now)
            .WithMessage("Start time must be in the future");

        RuleFor(x => x.EndTime)
            .Must(endTime => endTime > DateTime.Now)
            .WithMessage("End time must be in the future");

        RuleFor(x => new { x.StartTime, x.EndTime })
            .Must(x => x.StartTime < x.EndTime)
            .WithMessage("Start time must be before end time");

        RuleFor(x => new { x.StartTime, x.EndTime })
            .Must(x => x.EndTime - x.StartTime >= TimeSpan.FromMinutes(90))
            .WithMessage("Event duration must be at least 90 minutes")
            .Must(x => x.EndTime - x.StartTime <= TimeSpan.FromHours(6))
            .WithMessage("Event duration cannot exceed 6 hours");

        RuleFor(x => new { x.StartTime, x.EndTime })
            .Must(x => x.StartTime.Minute % 15 == 0 && x.EndTime.Minute % 15 == 0)
            .WithMessage("Times must be rounded to 45-minute intervals");
    }
}