
namespace comerciales.Application.Abstractions;

public interface IPasswordHasher
{
    /// <summary>
    /// Verifica si la contraseña en texto plano coincide con el hash almacenado en la base de datos
    /// </summary>
    /// <param name="plainPassword">Contraseña en texto plano</param>
    /// <param name="dbHash">Hash almacenado en la base de datos</param>
    /// <returns>True si coinciden, false en caso contrario</returns>
    bool Verify(string plainPassword, byte[] dbHash);

}
