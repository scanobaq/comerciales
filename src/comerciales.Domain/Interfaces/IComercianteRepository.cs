using comerciales.Domain.Entities;
using comerciales.Domain.Models;

namespace comerciales.Domain.Interfaces;

public interface IComercianteRepository
{
    /// <summary>
    /// Obtiene una lista paginada de comerciantes según los parámetros de filtro proporcionados.
    /// </summary> 
    /// <param name="filtroParams">Parámetros de filtro y paginación</param>
    /// <returns>Tupla con la lista de comerciantes y el conteo total</returns 
    Task<(IEnumerable<Comerciante> Comerciantes, int TotalCount)> GetComerciantesAsync(FiltroParams filtroParams);

    /// <summary>
    /// Crea un nuevo comerciante en la base de datos.
    /// </summary>
    /// <param name="comerciante">El comerciante a crear</param>
    /// <returns>El comerciante creado</returns>
    Task<Comerciante> CreateComercianteAsync(Comerciante comerciante);

    /// <summary>
    /// Actualiza un comerciante existente en la base de datos.
    /// </summary>
    /// <param name="comerciante">El comerciante con los datos actualizados</param>
    /// <returns>El comerciante actualizado</returns>
    Task<Comerciante> UpdateComercianteAsync(Comerciante comerciante);


    /// <summary>
    /// Elimina un comerciante de la base de datos.
    /// </summary>
    /// <param name="comercianteId">El ID del comerciante a eliminar</param>
    /// <returns>True si se eliminó correctamente, de lo contrario false</returns>
    Task<bool> DeleteComercianteAsync(int comercianteId);

}
