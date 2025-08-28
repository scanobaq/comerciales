using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace comerciales.Domain.Entities;

[Table("EstadoComerciante", Schema = "reg")]

public partial class EstadoComerciante
{
    [Key]
    public int EstadoId { get; set; }

    [Required]
    [StringLength(50)]
    public string Nombre { get; set; }

    [InverseProperty("Estado")]
    public virtual ICollection<Comerciante> Comerciantes { get; set; } = new List<Comerciante>();
}
