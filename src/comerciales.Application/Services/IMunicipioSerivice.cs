using comerciales.Application.DTOs;

namespace comerciales.Application.Services;

public interface IMunicipioService
{

    /// <summary>
    /// Obtiene todos los municipios.       
    /// </summary>
    /// <returns>Return all municipalities</returns>
    Task<IEnumerable<MunicipioDto>> GetAllAsync();

    /// <summary>
    /// Obtiene un municipio por su ID.
    /// </summary>
    /// <param name="id">ID del municipio</param>
    /// <returns>El municipio si existe, null en caso contrario</returns>
    Task<MunicipioDto> GetByIdAsync(int id);
    /// <summary>
    /// Busca municipios por nombre.
    /// </summary>
    /// <param name="nombre">Nombre o parte del nombre del municipio</param>
    /// <returns>Lista de municipios que coinciden con el criterio</returns>
    Task<IEnumerable<MunicipioDto>> GetByNameAsync(string nombre);
    /// <summary>
    /// Crea un nuevo municipio.
    /// </summary>
    /// <param name="municipioDto">Municipio a crear</param>
    /// <returns>El municipio creado con su ID generado</returns>
    Task<MunicipioDto> CreateAsync(MunicipioDto municipioDto);
    /// <summary>
    /// Actualiza un municipio existente.
    /// </summary>
    /// <param name="municipioDto">Municipio con los datos actualizados</param>
    /// <returns>El municipio actualizado</returns>
    Task<MunicipioDto> UpdateAsync(MunicipioDto municipioDto);
    /// <summary>
    /// Elimina un municipio por su ID.
    /// </summary>
    /// <param name="id">ID del municipio a eliminar</param>
    /// <returns>True si se eliminó correctamente, false en caso contrario</returns>
    Task<bool> DeleteAsync(int id);

}
