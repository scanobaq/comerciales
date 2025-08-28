using AutoMapper;
using comerciales.Application.DTOs;
using comerciales.Domain.Interfaces;

namespace comerciales.Application.Services;

public class MunicipioService(
    IMunicipioRepository municipioRepository, IMapper mapper
    ) : IMunicipioService
{
    private readonly IMunicipioRepository _municipioRepository = municipioRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<MunicipioDto>> GetAllAsync()
    {
        var municipios = await _municipioRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<MunicipioDto>>(municipios);
    }

    public async Task<MunicipioDto> GetByIdAsync(int id)
    {
        var municipio = await _municipioRepository.GetByIdAsync(id);
        return municipio == null ? null : _mapper.Map<MunicipioDto>(municipio);
    }

    public async Task<IEnumerable<MunicipioDto>> GetByNameAsync(string nombre)
    {
        var municipios = await _municipioRepository.GetByNameAsync(nombre);
        return _mapper.Map<IEnumerable<MunicipioDto>>(municipios);
    }

    public async Task<MunicipioDto> CreateAsync(MunicipioDto municipioDto)
    {
        if (municipioDto == null)
            throw new ArgumentNullException(nameof(municipioDto));

        var municipio = _mapper.Map<Domain.Entities.Municipio>(municipioDto);
        var createdMunicipio = await _municipioRepository.CreateAsync(municipio);
        return _mapper.Map<MunicipioDto>(createdMunicipio);
    }

    public async Task<MunicipioDto> UpdateAsync(MunicipioDto municipioDto)
    {
        if (municipioDto == null)
            throw new ArgumentNullException(nameof(municipioDto));

        var municipio = _mapper.Map<Domain.Entities.Municipio>(municipioDto);
        var updatedMunicipio = await _municipioRepository.UpdateAsync(municipio);
        return _mapper.Map<MunicipioDto>(updatedMunicipio);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _municipioRepository.DeleteAsync(id);
    }

}
