using comerciales.Domain.Entities;

namespace comerciales.Domain.Interfaces;

public interface IUserRepository
{
    /// <summary>
    /// Verifica si un usuario existe por su correo electrónico
    /// </summary>
    /// <param name="email">Correo electrónico del usuario</param>
    /// <returns>El usuario si existe, null en caso contrario</returns>
    Task<Usuario?> UserExistsAsync(string email);

}
