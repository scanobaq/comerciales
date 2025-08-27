namespace comerciales.Application.Abstractions;

public interface IJwtTokenGenerator
{
    /// <summary>
    /// Genera un token JWT para el usuario autenticado 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="userName"></param>
    /// <param name="userEmail"></param>
    /// <param name="role"></param>
    /// <returns>Token JWT generado</returns>
    string GenerateToken(int userId, string userName, string userEmail, string role);

}
