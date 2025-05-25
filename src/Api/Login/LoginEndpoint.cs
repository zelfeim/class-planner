using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Authorization;

namespace Api.Login;

[HttpPost("api/login")]
[AllowAnonymous]
public class LoginEndpoint(
    IConfiguration configuration
    ) : Endpoint<LoginRequest>
{
    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        var password = configuration["PlannerPassword"];

        if (password == req.Password)
        {
            await CookieAuth.SignInAsync(u =>
            {
                u.Roles.Add("Planner");
            });

            await SendNoContentAsync(ct);
            return;
        }

        await SendForbiddenAsync(ct);
    }
}

public record LoginRequest
{
    public required string Password { get; init; }
}