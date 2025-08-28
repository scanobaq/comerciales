namespace comerciales.WebApi.Models;

/// <summary>
/// Respuesta estándar para todos los endpoints de la API
/// </summary>
/// <typeparam name="T">Tipo de datos de la respuesta</typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// Indica si la operación fue exitosa
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Mensaje descriptivo de la operación
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Datos de la respuesta
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Lista de errores si los hay
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// Código de estado HTTP
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Timestamp de la respuesta
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Constructor para respuesta exitosa
    /// </summary>
    public static ApiResponse<T> SuccessResponse(T data, string message = "Operación exitosa", int statusCode = 200)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data,
            StatusCode = statusCode
        };
    }

    /// <summary>
    /// Constructor para respuesta de error
    /// </summary>
    public static ApiResponse<T> ErrorResponse(string message, List<string>? errors = null, int statusCode = 400)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Errors = errors ?? new List<string>(),
            StatusCode = statusCode
        };
    }

    /// <summary>
    /// Constructor para respuesta de error con una sola excepción
    /// </summary>
    public static ApiResponse<T> ErrorResponse(Exception exception, int statusCode = 500)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = "Error interno del servidor",
            Errors = new List<string> { exception.Message },
            StatusCode = statusCode
        };
    }
}
