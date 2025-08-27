
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace comerciales.Domain.Entities;

[Table("Comerciante", Schema = "reg")]
public partial class Comerciante
{
    [Key]
    public int ComercianteId { get; set; }

    [StringLength(200)]
    public string NombreORazonSocial { get; set; } = null!;

    [StringLength(120)]
    public string Municipio { get; set; } = null!;

    [StringLength(30)]
    public string? Telefono { get; set; }

    [StringLength(256)]
    public string? Correo { get; set; }

    public DateTime FechaRegistroUtc { get; set; }

    [StringLength(10)]

    public string Estado { get; set; } = null!;

    public DateTime FechaActualizacionUtc { get; set; }

    public int UsuarioActualizacionId { get; set; }

    [InverseProperty("Comerciante")]
    public virtual ICollection<Establecimiento> Establecimientos { get; set; } = new List<Establecimiento>();

    [ForeignKey("UsuarioActualizacionId")]
    [InverseProperty("Comerciantes")]
    public virtual Usuario UsuarioActualizacion { get; set; } = null!;
}
