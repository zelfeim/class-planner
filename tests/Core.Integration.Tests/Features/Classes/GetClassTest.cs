using Core.Domain.Entity;
using Core.Features.Class.GetClasses;
using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using Xunit;
using Group = Core.Domain.Entity.Group;

namespace Core.Integration.Tests.Features.Classes;

public class GetClassTest(Sut app) : TestBase<Sut>
{
    private Domain.Entity.Lecturer Lecturer { get; set; } = null!;
    private Classroom Classroom { get; set; } = null!;
    private Group Group { get; set; } = null!;
    private Class FakeClass { get; set; } = null!;

    protected override async ValueTask SetupAsync()
    {
        Lecturer = Fake.Lecturer();
        await app.DbContext.Lecturers.AddAsync(Lecturer);

        Classroom = Fake.Classroom();
        await app.DbContext.Classrooms.AddAsync(Classroom);

        Group = Fake.Group();
        await app.DbContext.Groups.AddAsync(Group);

        FakeClass = Fake.Class(Lecturer.Id, Classroom.Id, Group.Id);
        await app.DbContext.Classes.AddAsync(FakeClass);

        await app.DbContext.SaveChangesAsync();
    }

    [Fact]
    public async Task GetClasses_Should_Return_Expected_Class()
    {
        // Arrange

        // Act
        var (rsp, res) = await app.Client.GETAsync<GetClassesEndpoint, GetClassesResponse>();

        // Assert
        rsp.EnsureSuccessStatusCode();
        res.Classes.Count.Should().Be(1);

        var responseClass = res.Classes.First();
        responseClass.Should().NotBeNull();
        responseClass.Name.Should().Be(FakeClass.Name);
        responseClass.Classroom.Id.Should().Be(1);
        responseClass.Classroom.Number.Should().Be(Classroom.Number);
        responseClass.Group.Id.Should().Be(1);
        responseClass.Group.Classification.Should().Be(Group.Classification);
        responseClass.Lecturer.Id.Should().Be(1);
        responseClass.Lecturer.Email.Should().Be(Lecturer.Email);
    }
}
