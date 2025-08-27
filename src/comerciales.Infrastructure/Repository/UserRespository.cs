

using comercial.Infrastructure.Persistence.Models;
using comerciales.Domain.Entities;
using comerciales.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace comerciales.Infrastructure.Repository;

public class UserRespository(AppDbContext appDbContext) : IUserRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<Usuario> UserExistsAsync(string email)
    {
        var userQuery = _appDbContext.Usuarios.AsNoTracking();
        var user = await userQuery.FirstOrDefaultAsync(u => u.Correo == email && u.Activo);
        return user;
    }

}
