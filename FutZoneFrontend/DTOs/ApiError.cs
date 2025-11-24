namespace FutZoneFrontend.DTOs;
    using System.Collections.Generic;
// Nota: Puedes agregar System.Text.Json.Serialization si tu API no usa camelCase en los errores
// using System.Text.Json.Serialization; 

// DTO para capturar respuestas de error de la API (códigos 4xx/5xx)
public class ApiError
{
    // Mensaje de error general (puede ser 'message', 'error', o null)
    public string? message { get; set; }
    public string? error { get; set; }

    // Diccionario para errores de validación de campo (lo que a menudo es un array)
    // Ejemplo: { "Nombre": ["El campo Nombre es requerido"], "Correo": ["Formato inválido"] }
    public Dictionary<string, string[]>? errors { get; set; }

    // Otros campos comunes en las respuestas de error (status, timestamp, etc.)
    public int? status { get; set; }
}