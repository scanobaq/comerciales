

using comercial.Infrastructure.Persistence.Models;
using comerciales.Domain.Entities;
using comerciales.Domain.Interfaces;
using comerciales.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace comerciales.Infrastructure.Repository;

public class ComercianteRepository(AppDbContext appDbContext) : IComercianteRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;
    public async Task<(IEnumerable<Comerciante> Comerciantes, int TotalCount)> GetComerciantesAsync(FiltroParams filtroParams)
    {

        var query = _appDbContext.Comerciantes.AsQueryable();
        // Aplicar filtros
        if (!string.IsNullOrWhiteSpace(filtroParams.NombreORazonSocial))
        {
            query = query.Where(c => c.NombreORazonSocial.Contains(filtroParams.NombreORazonSocial));
        }
        if (filtroParams.FechaRegistroDesde.HasValue)
        {
            query = query.Where(c => c.FechaRegistroUtc >= filtroParams.FechaRegistroDesde.Value);
        }
        if (filtroParams.FechaRegistroHasta.HasValue)
        {
            query = query.Where(c => c.FechaRegistroUtc <= filtroParams.FechaRegistroHasta.Value);
        }
        if (filtroParams.EstadoId > 0)
        {
            query = query.Where(c => c.EstadoId == filtroParams.EstadoId);
        }

        int totalCount = await query.CountAsync();
        // Aplicar paginación
        var comerciantes = await query
            .Skip((filtroParams.PageNumber - 1) * filtroParams.PageSize)
            .Take(filtroParams.PageSize)
            .ToListAsync();

        return (comerciantes, totalCount);

    }

    public async Task<Comerciante> CreateComercianteAsync(Comerciante comerciante)
    {
        comerciante.ComercianteId = 0;

        _appDbContext.Comerciantes.Add(comerciante);
        await _appDbContext.SaveChangesAsync();

        await _appDbContext.Entry(comerciante).ReloadAsync();

        return comerciante;
    }

    public async Task<Comerciante> UpdateComercianteAsync(Comerciante comerciante)
    {
        var existingComerciante = await _appDbContext.Comerciantes
            .FirstOrDefaultAsync(c => c.ComercianteId == comerciante.ComercianteId);

        if (existingComerciante == null)
        {
            throw new ArgumentException($"No se encontró el comerciante con ID {comerciante.ComercianteId}");
        }

        existingComerciante.NombreORazonSocial = comerciante.NombreORazonSocial;
        existingComerciante.MunicipioId = comerciante.MunicipioId;
        existingComerciante.EstadoId = comerciante.EstadoId;
        existingComerciante.Telefono = comerciante.Telefono;
        existingComerciante.Correo = comerciante.Correo;


        await _appDbContext.SaveChangesAsync();

        await _appDbContext.Entry(existingComerciante).ReloadAsync();

        return existingComerciante;
    }

    public async Task<bool> DeleteComercianteAsync(int comercianteId)
    {
        var comerciante = await _appDbContext.Comerciantes
            .FirstOrDefaultAsync(c => c.ComercianteId == comercianteId);

        if (comerciante == null)
            return false;

        _appDbContext.Comerciantes.Remove(comerciante);
        await _appDbContext.SaveChangesAsync();
        return true;
    }
}
