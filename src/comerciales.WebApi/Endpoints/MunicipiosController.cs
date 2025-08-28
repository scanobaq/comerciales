using comerciales.Application.DTOs;
using comerciales.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace comerciales.WebApi.Endpoints;

[ApiController]
[Route("api/[controller]")]
[Authorize] // ← Esto requiere JWT para TODOS los endpoints del controlador
public class MunicipiosController(IMunicipioService municipioService) : ControllerBase
{
    private readonly IMunicipioService _municipioService = municipioService;


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var municipios = await _municipioService.GetAllAsync();
        return Ok(municipios);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var municipio = await _municipioService.GetByIdAsync(id);
        if (municipio == null)
            throw new KeyNotFoundException($"Municipio con ID {id} no encontrado");

        return Ok(municipio);
    }
    [HttpGet("search")]
    public async Task<IActionResult> GetByName([FromQuery] string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El parámetro 'nombre' es requerido y no puede estar vacío", nameof(nombre));

        var municipios = await _municipioService.GetByNameAsync(nombre);
        return Ok(municipios);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MunicipioDto municipioDto)
    {
        if (municipioDto == null)
            throw new ArgumentNullException(nameof(municipioDto), "El municipio no puede ser null");


        var createdMunicipio = await _municipioService.CreateAsync(municipioDto);
        return CreatedAtAction(nameof(GetById), new { id = createdMunicipio.MunicipioId }, createdMunicipio);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] MunicipioDto municipioDto)
    {
        if (municipioDto == null)
            throw new ArgumentNullException(nameof(municipioDto), "El municipio no puede ser null");

        if (municipioDto.MunicipioId != id)
            throw new ArgumentException("El ID de la URL no coincide con el ID del municipio", nameof(id));


        var updatedMunicipio = await _municipioService.UpdateAsync(municipioDto);
        return Ok(updatedMunicipio);
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {

        var deleted = await _municipioService.DeleteAsync(id);
        if (!deleted)
            throw new KeyNotFoundException($"Municipio con ID {id} no encontrado");

        return NoContent();
    }

}