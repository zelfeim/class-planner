using Core.Features.Class.CreateClass;
using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;

namespace Core.Tests.Features.Classes;

public class CreateClassTest(Sut app) : TestBase<Sut>
{
    protected override async ValueTask SetupAsync()
    {
        var lecturer = Fake.Lecturer();
        await app.DbContext.Lecturers.AddAsync(lecturer);

        var classroom = Fake.Classroom();
        await app.DbContext.Classrooms.AddAsync(classroom);

        var group = Fake.Group();
        await app.DbContext.Groups.AddAsync(group);

        await app.DbContext.SaveChangesAsync();
    }

    [Fact]
    public async Task CreateClass_Should_Successfully_Create_New_Class()
    {
        // Arrange
        var request = new CreateClassRequest()
        {
            Name = "NewClass",
            ClassroomId = 1,
            GroupId = 1,
            LecturerId = 1,
            Length = 90,
        };

        // Act
        var (rsp, res) = await app.Client.POSTAsync<
            CreateClassEndpoint,
            CreateClassRequest,
            CreateClassResponse
        >(request);

        // Assert
        rsp.EnsureSuccessStatusCode();
        res.Id.Should().Be(1);
    }
}
