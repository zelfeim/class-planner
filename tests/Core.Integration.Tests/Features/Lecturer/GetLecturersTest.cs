using Core.Features.Lecturer.GetAll;
using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using Xunit;

namespace Core.Integration.Tests.Features.Lecturer;

public class GetLecturersEndpointTest(Sut app) : TestBase<Sut>
{
    protected override async ValueTask SetupAsync()
    {
        List<Domain.Entity.Lecturer> lecturers = [Fake.Lecturer(), Fake.Lecturer()];
        await app.DbContext.Lecturers.AddRangeAsync(lecturers);

        await app.DbContext.SaveChangesAsync();
    }

    [Fact(Skip = "Doesn't work")]
    public async Task GetLecturers_Should_Return_Expected_Lecturers()
    {
        // Arrange

        // Act
        var response = await app.Client.GETAsync<GetLecturersEndpoint, GetLecturersResponse>();

        // Assert
        response.Response.EnsureSuccessStatusCode();
        response.Result.Lecturers.Should().HaveCount(2);
    }
}
