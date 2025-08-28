using comercial.Infrastructure.Persistence.Models;
using comerciales.Domain.Entities;
using comerciales.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace comerciales.Infrastructure.Repository;

public class MunicipioRepository(AppDbContext appDbContext) : IMunicipioRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<IEnumerable<Municipio>> GetAllAsync()
    {
        return await _appDbContext.Municipios
            .AsNoTracking()
            .OrderBy(m => m.Nombre)
            .ToListAsync();
    }

    public async Task<Municipio?> GetByIdAsync(int id)
    {
        return await _appDbContext.Municipios
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MunicipioId == id);
    }

    public async Task<IEnumerable<Municipio>> GetByNameAsync(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            return new List<Municipio>();

        return await _appDbContext.Municipios
            .AsNoTracking()
            .Where(m => m.Nombre.Contains(nombre))
            .OrderBy(m => m.Nombre)
            .ToListAsync();
    }

    public async Task<Municipio> CreateAsync(Municipio municipio)
    {
        if (municipio == null)
            throw new ArgumentNullException(nameof(municipio));

        _appDbContext.Municipios.Add(municipio);
        await _appDbContext.SaveChangesAsync();
        return municipio;
    }

    public async Task<Municipio> UpdateAsync(Municipio municipio)
    {
        if (municipio == null)
            throw new ArgumentNullException(nameof(municipio));

        var existingMunicipio = await _appDbContext.Municipios
            .FirstOrDefaultAsync(m => m.MunicipioId == municipio.MunicipioId);

        if (existingMunicipio == null)
            throw new InvalidOperationException($"Municipio con ID {municipio.MunicipioId} no encontrado");

        // Actualizar propiedades
        existingMunicipio.Nombre = municipio.Nombre;

        _appDbContext.Municipios.Update(existingMunicipio);
        await _appDbContext.SaveChangesAsync();
        return existingMunicipio;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var municipio = await _appDbContext.Municipios
            .FirstOrDefaultAsync(m => m.MunicipioId == id);

        if (municipio == null)
            return false;

        // Verificar si tiene comerciantes asociados
        var hasComerciantesAssociated = await _appDbContext.Comerciantes
            .AnyAsync(c => c.MunicipioId == id);

        if (hasComerciantesAssociated)
            throw new InvalidOperationException("No se puede eliminar el municipio porque tiene comerciantes asociados");

        _appDbContext.Municipios.Remove(municipio);
        await _appDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(string nombre, int? excludeId = null)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            return false;

        var query = _appDbContext.Municipios
            .AsNoTracking()
            .Where(m => m.Nombre.ToLower() == nombre.ToLower());

        if (excludeId.HasValue)
            query = query.Where(m => m.MunicipioId != excludeId.Value);

        return await query.AnyAsync();
    }
}
