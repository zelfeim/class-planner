using Bogus;
using Core.Domain.Entity;

namespace Core.Integration.Tests;

public static class Fakes
{
    public static Lecturer Lecturer(this Faker faker)
    {
        return new Lecturer { Id = faker.IndexGlobal, Email = faker.Person.Email };
    }

    public static Year Year(this Faker faker)
    {
        return new Year
        {
            Id = faker.IndexGlobal,
            Mode = StudyMode.FullTime,
            Name = faker.Random.String(3),
        };
    }

    public static Group Group(this Faker faker, int yearId)
    {
        return new Group
        {
            Classification = "A",
            Type = GroupType.Seminary,
            Id = faker.IndexFaker,
            YearId = yearId,
        };
    }

    public static Classroom Classroom(this Faker faker)
    {
        return new Classroom { Id = faker.IndexFaker, Number = faker.Random.AlphaNumeric(3) };
    }

    public static Class Class(this Faker faker, int lecturerId, int classroomId, int groupId)
    {
        return new Class
        {
            Id = faker.IndexFaker,
            Name = faker.Random.String(10),
            LecturerId = lecturerId,
            ClassroomId = classroomId,
            GroupId = groupId,
        };
    }
}
