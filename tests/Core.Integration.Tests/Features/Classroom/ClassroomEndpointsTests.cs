using System.Text.Json;
using Core.Features.Classroom.GetSingle;
using FluentAssertions;

namespace Core.Tests.Features.Classroom;

public class ClassroomEndpointsTests(IntegrationTestWebApplicationFactory factory)
    : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task Get_Classrooms_ReturnsSuccess()
    {
        // Arrange
        DbContext.Classrooms.Add(new Domain.Entity.Classroom { Number = "1" });
        DbContext.Classrooms.Add(new Domain.Entity.Classroom { Number = "2" });
        await DbContext.SaveChangesAsync();

        // Act
        var response = await Client.GetAsync("/api/classroom");

        // Assert
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();

        var classrooms = JsonSerializer.Deserialize<List<GetClassroomResponse>>(content);
        classrooms.Should().HaveCount(1);
        classrooms.First().Number.Should().Be("1");
        classrooms.ElementAt(2).Number.Should().Be("2");
    }
}
