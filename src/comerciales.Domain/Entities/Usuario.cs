using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace comerciales.Domain.Entities;


[Table("Usuario", Schema = "reg")]
public partial class Usuario
{
    [Key]
    public int UsuarioId { get; set; }

    [StringLength(150)]
    public string Nombre { get; set; } = null!;

    [StringLength(256)]
    public string Correo { get; set; } = null!;

    [MaxLength(64)]
    public byte[] PasswordHash { get; set; } = null!;

    [StringLength(30)]

    public string Rol { get; set; } = null!;

    public bool Activo { get; set; }

    public DateTime CreadoUtc { get; set; }

    public virtual ICollection<Comerciante> Comerciantes { get; set; } = new List<Comerciante>();

    public virtual ICollection<Establecimiento> Establecimientos { get; set; } = new List<Establecimiento>();
}
