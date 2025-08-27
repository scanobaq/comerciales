using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace comerciales.Domain.Entities;

[Table("Establecimiento", Schema = "reg")]
public partial class Establecimiento
{
    [Key]
    public int EstablecimientoId { get; set; }

    public int ComercianteId { get; set; }

    [StringLength(200)]
    public string NombreEstablecimiento { get; set; } = null!;

    [Column(TypeName = "decimal(14, 2)")]
    public decimal Ingresos { get; set; }

    public int NumeroEmpleados { get; set; }

    public DateTime FechaActualizacionUtc { get; set; }

    public int UsuarioActualizacionId { get; set; }

    [ForeignKey("ComercianteId")]
    [InverseProperty("Establecimientos")]
    public virtual Comerciante Comerciante { get; set; } = null!;

    [ForeignKey("UsuarioActualizacionId")]
    [InverseProperty("Establecimientos")]
    public virtual Usuario UsuarioActualizacion { get; set; } = null!;
}
