using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace comerciales.Domain.Entities;

[Table("Comerciante", Schema = "reg")]

public partial class Comerciante
{
    [Key]
    public int ComercianteId { get; set; }

    [Required]
    [StringLength(200)]
    public string NombreORazonSocial { get; set; }

    public int MunicipioId { get; set; }

    public int EstadoId { get; set; }

    [StringLength(30)]
    public string Telefono { get; set; }

    [StringLength(256)]
    public string Correo { get; set; }

    public DateTime FechaRegistroUtc { get; set; }

    public DateTime FechaActualizacionUtc { get; set; }

    public int UsuarioActualizacionId { get; set; }

    [InverseProperty("Comerciante")]
    public virtual ICollection<Establecimiento> Establecimientos { get; set; } = new List<Establecimiento>();

    [ForeignKey("EstadoId")]
    [InverseProperty("Comerciantes")]
    public virtual EstadoComerciante Estado { get; set; }

    [ForeignKey("MunicipioId")]
    [InverseProperty("Comerciantes")]
    public virtual Municipio Municipio { get; set; }

    [ForeignKey("UsuarioActualizacionId")]
    [InverseProperty("Comerciantes")]
    public virtual Usuario UsuarioActualizacion { get; set; }
}
