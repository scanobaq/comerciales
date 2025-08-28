using comerciales.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace comerciales.WebApi.Filters;

/// <summary>
/// Filtro global para estandarizar todas las respuestas de la API
/// </summary>
public class ApiResponseFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // No necesitamos hacer nada antes de la ejecución
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Solo procesar si no hay excepción y la respuesta es exitosa
        if (context.Exception != null)
            return;

        var result = context.Result;

        // Si ya es una ApiResponse, no la modifiques
        if (result is ObjectResult objectResult &&
            objectResult.Value?.GetType().IsGenericType == true &&
            objectResult.Value.GetType().GetGenericTypeDefinition() == typeof(ApiResponse<>))
        {
            return;
        }

        // Estandarizar la respuesta
        switch (result)
        {
            case ObjectResult objResult:
                var standardResponse = ApiResponse<object>.SuccessResponse(
                    objResult.Value,
                    "Operación realizada exitosamente",
                    objResult.StatusCode ?? 200
                );
                context.Result = new ObjectResult(standardResponse)
                {
                    StatusCode = objResult.StatusCode ?? 200
                };
                break;

            case StatusCodeResult statusResult:
                var emptyResponse = ApiResponse<object>.SuccessResponse(
                    null,
                    GetMessageForStatusCode(statusResult.StatusCode),
                    statusResult.StatusCode
                );
                context.Result = new ObjectResult(emptyResponse)
                {
                    StatusCode = statusResult.StatusCode
                };
                break;

            case EmptyResult:
                var noContentResponse = ApiResponse<object>.SuccessResponse(
                    null,
                    "Operación completada",
                    200
                );
                context.Result = new ObjectResult(noContentResponse);
                break;
        }
    }

    private static string GetMessageForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            200 => "Operación exitosa",
            201 => "Recurso creado exitosamente",
            204 => "Operación completada sin contenido",
            400 => "Solicitud incorrecta",
            401 => "No autorizado",
            403 => "Prohibido",
            404 => "Recurso no encontrado",
            500 => "Error interno del servidor",
            _ => "Operación completada"
        };
    }
}
