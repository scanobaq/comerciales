
namespace comerciales.Domain.Models;

public class FiltroParams
{
    public string NombreORazonSocial { get; set; }
    public DateTime? FechaRegistroDesde { get; set; }
    public DateTime? FechaRegistroHasta { get; set; }
    public int EstadoId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

}
