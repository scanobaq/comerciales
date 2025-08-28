
using comerciales.Application.DTOs;

namespace comerciales.Application.Services;

public interface IComercianteService
{
    /// <summary>
    /// Obtiene una lista paginada de comerciantes según los parámetros de filtro proporcionados.
    /// </summary>
    /// <param name="filtroParamsDto"></param>
    /// <returns>
    /// Una lista paginada de comerciantes.
    /// </returns>
    Task<PageResultadoDto<ComercianteDto>> GetComerciantesAsync(FiltroParamsDto filtroParamsDto);

    /// <summary>
    /// Crea un nuevo comerciante.
    /// </summary>
    /// <param name="comercianteDto">Datos del comerciante a crear.</param>
    /// <returns>El comerciante creado con su ID generado.</returns>
    Task<ComercianteDto> CreateComercianteAsync(ComercianteDto comercianteDto);

    /// <summary>
    /// Actualiza un comerciante existente.
    /// </summary>
    /// <param name="comercianteDto">Datos del comerciante a actualizar.</param>
    /// <returns>El comerciante actualizado.</returns>
    Task<ComercianteDto> UpdateComercianteAsync(ComercianteDto comercianteDto);

    /// <summary>
    /// Elimina un comerciante por su ID.
    /// </summary>
    /// <param name="comercianteId">ID del comerciante a eliminar.</param>
    /// <returns>True si se eliminó correctamente, de lo contrario false.</returns>
    Task<bool> DeleteComercianteAsync(int comercianteId);

}
