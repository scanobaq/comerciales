namespace comerciales.Application.Models;

/// <summary>
/// Resultado paginado genérico
/// </summary>
/// <typeparam name="T">Tipo de datos del resultado</typeparam>
public class ResultadoPaginado<T>
{
    /// <summary>
    /// Lista de elementos de la página actual
    /// </summary>
    public IEnumerable<T> Elementos { get; set; } = new List<T>();

    /// <summary>
    /// Número de página actual (base 1)
    /// </summary>
    public int NumeroPagina { get; set; }

    /// <summary>
    /// Tamaño de página configurado
    /// </summary>
    public int TamanoPagina { get; set; }

    /// <summary>
    /// Total de registros en la consulta (sin paginación)
    /// </summary>
    public int TotalRegistros { get; set; }

    /// <summary>
    /// Total de páginas disponibles
    /// </summary>
    public int TotalPaginas => (int)Math.Ceiling((double)TotalRegistros / TamanoPagina);

    /// <summary>
    /// Indica si hay página anterior
    /// </summary>
    public bool TienePaginaAnterior => NumeroPagina > 1;

    /// <summary>
    /// Indica si hay página siguiente
    /// </summary>
    public bool TienePaginaSiguiente => NumeroPagina < TotalPaginas;

    /// <summary>
    /// Número de la primera página
    /// </summary>
    public int PrimeraPagina => 1;

    /// <summary>
    /// Número de la última página
    /// </summary>
    public int UltimaPagina => TotalPaginas;

    /// <summary>
    /// Constructor
    /// </summary>
    public ResultadoPaginado(IEnumerable<T> elementos, int totalRegistros, int numeroPagina, int tamanoPagina)
    {
        Elementos = elementos;
        TotalRegistros = totalRegistros;
        NumeroPagina = numeroPagina;
        TamanoPagina = tamanoPagina;
    }

    /// <summary>
    /// Crear resultado paginado vacío
    /// </summary>
    public static ResultadoPaginado<T> Vacio(int numeroPagina, int tamanoPagina)
    {
        return new ResultadoPaginado<T>(new List<T>(), 0, numeroPagina, tamanoPagina);
    }
}
