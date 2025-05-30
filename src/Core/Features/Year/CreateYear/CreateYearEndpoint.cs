using Core.Domain.Entity;
using Core.Infrastructure.Persistence;
using FastEndpoints;

namespace Core.Features.Year.CreateYear;

[HttpPost("api/year")]
public class CreateYearEndpoint(ApplicationDbContext dbContext) : Endpoint<CreateYearRequest>
{
    public override async Task HandleAsync(CreateYearRequest req, CancellationToken ct)
    {
        var duplicateYear = dbContext.Years.FirstOrDefault(x =>
            x.Mode == req.Mode && x.Name == req.Name
        );

        if (duplicateYear != null)
        {
            ThrowError("Year with this mode and name already exists!");
        }

        var year = new Domain.Entity.Year { Name = req.Name, Mode = req.Mode };

        await dbContext.Years.AddAsync(year, ct);
        await dbContext.SaveChangesAsync(ct);
    }
}

public record CreateYearRequest
{
    public StudyMode Mode { get; init; }
    public required string Name { get; init; }
}
