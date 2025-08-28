using comerciales.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace comerciales.WebApi.Filters;

/// <summary>
/// Filtro global para manejar excepciones y estandarizar respuestas de error
/// </summary>
public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "Error no controlado en {Action}",
            context.ActionDescriptor.DisplayName);

        var response = context.Exception switch
        {
            ArgumentNullException ex => ApiResponse<object>.ErrorResponse(
                "Parámetro requerido no proporcionado",
                new List<string> { ex.Message },
                400),

            ArgumentException ex => ApiResponse<object>.ErrorResponse(
                "Parámetro inválido",
                new List<string> { ex.Message },
                400),

            InvalidOperationException ex => ApiResponse<object>.ErrorResponse(
                "Operación no válida",
                new List<string> { ex.Message },
                409),

            UnauthorizedAccessException => ApiResponse<object>.ErrorResponse(
                "Acceso no autorizado",
                new List<string> { "No tiene permisos para realizar esta operación" },
                401),

            KeyNotFoundException ex => ApiResponse<object>.ErrorResponse(
                "Recurso no encontrado",
                new List<string> { ex.Message },
                404),

            _ => ApiResponse<object>.ErrorResponse(
                "Error interno del servidor",
                new List<string> { "Ha ocurrido un error inesperado" },
                500)
        };

        context.Result = new ObjectResult(response)
        {
            StatusCode = response.StatusCode
        };

        context.ExceptionHandled = true;
    }
}
