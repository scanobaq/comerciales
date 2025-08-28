
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace comerciales.Domain.Entities;

[Table("Municipio", Schema = "reg")]
public partial class Municipio
{
    [Key]
    public int MunicipioId { get; set; }

    [Required]
    [StringLength(120)]
    public string Nombre { get; set; }

    [InverseProperty("Municipio")]
    public virtual ICollection<Comerciante> Comerciantes { get; set; } = new List<Comerciante>();
}
