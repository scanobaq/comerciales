
namespace comerciales.Application.DTOs;

public class UserDto
{
    public int UsuarioId { get; set; }
    public string Nombre { get; set; } = null!;
    public string Correo { get; set; } = null!;
    public byte[] PasswordHash { get; set; } = null!;
    public string Rol { get; set; } = null!;
    public bool Activo { get; set; }
    public DateTime CreadoUtc { get; set; }
}
