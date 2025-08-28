using AutoMapper;
using comerciales.Application.DTOs;
using comerciales.Domain.Entities;
using comerciales.Domain.Interfaces;
using comerciales.Domain.Models;

namespace comerciales.Application.Services;

public class ComercianteService(IComercianteRepository comercianteRepository, IMapper mapper) : IComercianteService
{
    private readonly IComercianteRepository _comercianteRepository = comercianteRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<PageResultadoDto<ComercianteDto>> GetComerciantesAsync(FiltroParamsDto filtroParamsDto)
    {
        var filtros = _mapper.Map<FiltroParams>(filtroParamsDto);
        var (comerciantes, totalCount) = await _comercianteRepository.GetComerciantesAsync(filtros);

        return new PageResultadoDto<ComercianteDto>
        {
            TotalCount = totalCount,
            Items = _mapper.Map<IEnumerable<ComercianteDto>>(comerciantes),
            PageNumber = filtroParamsDto.PageNumber,
            PageSize = filtroParamsDto.PageSize,
            TotalPages = (int)Math.Ceiling((double)totalCount / filtroParamsDto.PageSize)

        };
    }

    public async Task<ComercianteDto> CreateComercianteAsync(ComercianteDto comercianteDto)
    {
        var comerciante = _mapper.Map<Comerciante>(comercianteDto);
        var createdComerciante = await _comercianteRepository.CreateComercianteAsync(comerciante);
        return _mapper.Map<ComercianteDto>(createdComerciante);
    }

    public async Task<ComercianteDto> UpdateComercianteAsync(ComercianteDto comercianteDto)
    {
        var comerciante = _mapper.Map<Comerciante>(comercianteDto);
        var updatedComerciante = await _comercianteRepository.UpdateComercianteAsync(comerciante);
        return _mapper.Map<ComercianteDto>(updatedComerciante);
    }

    public async Task<bool> DeleteComercianteAsync(int comercianteId)
    {
        return await _comercianteRepository.DeleteComercianteAsync(comercianteId);
    }

}
