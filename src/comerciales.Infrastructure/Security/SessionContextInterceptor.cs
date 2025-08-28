
using comerciales.Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace comerciales.Infrastructure.Security;

public class SessionContextInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUser _current;
    public SessionContextInterceptor(ICurrentUser current) => _current = current;

    private void SetSession(DbContext ctx)
    {
        if (ctx is null || !_current.IsAuthenticated) return;

        // Intentar parsear el ID del usuario como entero
        if (int.TryParse(_current.Id, out int userId))
        {
            // Usar tu stored procedure personalizado con la sintaxis correcta
            ctx.Database.ExecuteSqlRaw(
                "EXEC reg.usp_SetCurrentUserContext @UsuarioId = {0}", userId);
        }
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData e, InterceptionResult<int> r)
    { SetSession(e.Context); return base.SavingChanges(e, r); }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData e, InterceptionResult<int> r, CancellationToken ct = default)
    { SetSession(e.Context); return await base.SavingChangesAsync(e, r, ct); }

}
