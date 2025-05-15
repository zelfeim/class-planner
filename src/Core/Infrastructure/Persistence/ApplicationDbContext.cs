using Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Lecturer> Lecturers { get; set; }
    public DbSet<Classroom> Classrooms { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<ClassEvent> ClassEvents { get; set; }
    public DbSet<DayEvent> DayEvents { get; set; }
    public DbSet<Calendar> Calendars { get; set; }
    public DbSet<Year> Years { get; set; }
    public DbSet<Group> Groups { get; set; }
}