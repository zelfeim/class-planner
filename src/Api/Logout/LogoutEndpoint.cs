using FastEndpoints;
using FastEndpoints.Security;

namespace Api.Logout;

[HttpGet("api/logout")]
public class LogoutEndpoint : EndpointWithoutRequest
{
    public override async Task HandleAsync(CancellationToken ct)
    {
        await CookieAuth.SignOutAsync();

        await SendNoContentAsync(ct);
    }
}