using System.ComponentModel.DataAnnotations;

namespace comerciales.Application.DTOs;

public class ComercianteDto
{
    public int ComercianteId { get; set; }

    [Required]
    public string NombreORazonSocial { get; set; }
    [Required]
    public int MunicipioId { get; set; }

    public string Telefono { get; set; }

    public string Correo { get; set; }
    [Required]
    public DateTime FechaRegistroUtc { get; set; }
    [Required]
    public int EstadoId { get; set; }


}
