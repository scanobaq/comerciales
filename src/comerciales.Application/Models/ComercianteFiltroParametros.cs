namespace comerciales.Application.Models;

/// <summary>
/// Parámetros de filtro para la consulta paginada de comerciantes
/// </summary>
public class ComercianteFiltroParametros
{
    /// <summary>
    /// Nombre o razón social del comerciante (búsqueda parcial)
    /// </summary>
    public string? NombreORazonSocial { get; set; }

    /// <summary>
    /// Fecha de registro desde (filtro de rango)
    /// </summary>
    public DateTime? FechaRegistroDesde { get; set; }

    /// <summary>
    /// Fecha de registro hasta (filtro de rango)
    /// </summary>
    public DateTime? FechaRegistroHasta { get; set; }

    /// <summary>
    /// Estado del comerciante (Activo, Inactivo, etc.)
    /// </summary>
    public string? Estado { get; set; }

    /// <summary>
    /// Número de página (base 1)
    /// </summary>
    public int NumeroPagina { get; set; } = 1;

    /// <summary>
    /// Tamaño de página (registros por página)
    /// </summary>
    public int TamanoPagina { get; set; } = 10;

    /// <summary>
    /// Campo para ordenar (NombreORazonSocial, FechaRegistroUtc, Estado)
    /// </summary>
    public string CampoOrden { get; set; } = "FechaRegistroUtc";

    /// <summary>
    /// Dirección del ordenamiento (ASC, DESC)
    /// </summary>
    public string DireccionOrden { get; set; } = "DESC";

    /// <summary>
    /// Validar que el tamaño de página no sea excesivo
    /// </summary>
    public void ValidarParametros()
    {
        if (TamanoPagina > 100) TamanoPagina = 100;
        if (TamanoPagina < 1) TamanoPagina = 10;
        if (NumeroPagina < 1) NumeroPagina = 1;
    }
}
