using comerciales.Application.DTOs;

namespace comerciales.Application.Services;

public interface IUserService
{
    /// <summary>
    /// Autentica un usuario y genera un token JWT si las credenciales son correctas
    /// </summary>
    /// <param name="email">Correo electrónico del usuario</param>
    /// <param name="password">Contraseña del usuario</param>
    /// <returns>Objeto LoginResponseDto si la autenticación es exitosa</returns>
    Task<LoginResponseDto> AuthenticateAsync(string email, string password);

}
