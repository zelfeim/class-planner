namespace Core.Domain.Entity;

public class ClassEvent : Event
{
    public ClassEvent() {}
    
    public ClassEvent(Calendar calendar, Class classInstance, DateTime startTime, DateTime endTime)
    {
        Calendar = calendar;
        Class = classInstance;
        StartTime = startTime;
        EndTime = endTime;
    }

    public Class Class { get; set; }
}