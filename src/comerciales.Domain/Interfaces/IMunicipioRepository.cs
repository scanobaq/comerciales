using comerciales.Domain.Entities;

namespace comerciales.Domain.Interfaces;

public interface IMunicipioRepository
{
    /// <summary>
    /// Obtiene todos los municipios activos
    /// </summary>
    /// <returns>Lista de municipios</returns>
    Task<IEnumerable<Municipio>> GetAllAsync();

    /// <summary>
    /// Obtiene un municipio por su ID
    /// </summary>
    /// <param name="id">ID del municipio</param>
    /// <returns>El municipio si existe, null en caso contrario</returns>
    Task<Municipio?> GetByIdAsync(int id);

    /// <summary>
    /// Busca municipios por nombre
    /// </summary>
    /// <param name="nombre">Nombre o parte del nombre del municipio</param>
    /// <returns>Lista de municipios que coinciden con el criterio</returns>
    Task<IEnumerable<Municipio>> GetByNameAsync(string nombre);

    /// <summary>
    /// Crea un nuevo municipio
    /// </summary>
    /// <param name="municipio">Municipio a crear</param>
    /// <returns>El municipio creado con su ID generado</returns>
    Task<Municipio> CreateAsync(Municipio municipio);

    /// <summary>
    /// Actualiza un municipio existente
    /// </summary>
    /// <param name="municipio">Municipio con los datos actualizados</param>
    /// <returns>El municipio actualizado</returns>
    Task<Municipio> UpdateAsync(Municipio municipio);

    /// <summary>
    /// Elimina un municipio por su ID
    /// </summary>
    /// <param name="id">ID del municipio a eliminar</param>
    /// <returns>True si se eliminó correctamente, false si no existe</returns>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Verifica si existe un municipio con el nombre especificado
    /// </summary>
    /// <param name="nombre">Nombre del municipio</param>
    /// <param name="excludeId">ID a excluir de la búsqueda (opcional, para updates)</param>
    /// <returns>True si existe, false en caso contrario</returns>
    Task<bool> ExistsAsync(string nombre, int? excludeId = null);
}
