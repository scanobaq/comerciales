
namespace comerciales.Application.Services;

public interface ICurrentUser
{
    bool IsAuthenticated { get; }
    string Id { get; }
    string UserName { get; }
    string TenantId { get; }

}
