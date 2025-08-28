using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using comerciales.Application.Services;

namespace comerciales.WebApi.Filters;

public class HttpCurrentUser : ICurrentUser
{
    public bool IsAuthenticated { get; }
    public string? Id { get; }
    public string? UserName { get; }
    public string? TenantId { get; }

    public HttpCurrentUser(IHttpContextAccessor accessor)
    {
        var u = accessor.HttpContext?.User;
        IsAuthenticated = u?.Identity?.IsAuthenticated == true;
        Id = u?.FindFirstValue(JwtRegisteredClaimNames.Sub)
                ?? u?.FindFirstValue(ClaimTypes.NameIdentifier);
        UserName = u?.FindFirstValue(ClaimTypes.Name);
        TenantId = u?.FindFirstValue("tenant_id");
    }

}
