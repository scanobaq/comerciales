
using comerciales.Application.DTOs;
using comerciales.Application.Services;
using comerciales.WebApi.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace comerciales.WebApi.Endpoints;

[ApiController]
[Route("api/[controller]")]
public class ComercianteController(IComercianteService comercianteService) : ControllerBase
{
    private readonly IComercianteService _comercianteService = comercianteService;
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetComerciantes([FromQuery] FiltroParamsDto filtroParamsDto)
    {
        var result = await _comercianteService.GetComerciantesAsync(filtroParamsDto);
        return Ok(result);

    }


    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateComerciante([FromBody] ComercianteDto comercianteDto)
    {
        if (comercianteDto == null)
            throw new ArgumentNullException(nameof(comercianteDto), "El comerciante no puede ser null");

        var result = await _comercianteService.CreateComercianteAsync(comercianteDto);
        return Ok(result);

    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateComerciante([FromBody] ComercianteDto comercianteDto)
    {
        if (comercianteDto == null)
            throw new ArgumentNullException(nameof(comercianteDto), "El comerciante no puede ser null");

        var result = await _comercianteService.UpdateComercianteAsync(comercianteDto);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    [Authorize(policy: PolicyNames.AdminsOnly)]
    public async Task<IActionResult> DeleteComerciante(int id)
    {
        var success = await _comercianteService.DeleteComercianteAsync(id);
        if (!success)
        {
            throw new KeyNotFoundException($"Comerciante con ID {id} no encontrado");
        }
        return NoContent();
    }
}