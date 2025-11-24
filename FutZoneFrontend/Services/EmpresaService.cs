using FutZoneFrontend.DTOs;
using System.Collections.Generic;
using System.Net.Http; // Necesario para HttpClient
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FutZoneFrontend.Services
{
    public class EmpresaService // No se usa IEmpresaService, solo la clase concreta
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService; // Para obtener el ID del usuario

        // Inyectamos el HttpClient (configurado como "ApiClient") y el AuthService
        public EmpresaService(HttpClient httpClient, IAuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        /// <summary>
        /// Obtiene las organizaciones asociadas al usuario autenticado.
        /// </summary>
        /// <returns>Una lista de EmpresaDto o una lista vacía si falla o no hay empresas.</returns>
        public async Task<List<EmpresaDto>> GetEmpresasDelUsuarioAsync()
        {
            // 1. Obtener el ID del usuario de la sesión
            var userId = await _authService.GetUserIdAsync();

            if (!userId.HasValue || userId.Value <= 0)
            {
                Console.WriteLine("[EmpresaService] Error: No se pudo obtener el ID del usuario autenticado.");
                return new List<EmpresaDto>();
            }

            // URL de la API: /empresa/{userId}
            var url = $"/empresa/usuario/{userId.Value}";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"[EmpresaService] Error HTTP: {response.StatusCode} al consultar {url}");
                    return new List<EmpresaDto>();
                }

                // Leer la respuesta completa
                var responseContent = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(responseContent))
                {
                    return new List<EmpresaDto>();
                }

                // 2. Deserialización Flexible (Manejo de Objeto Único vs. Lista)
                var element = JsonSerializer.Deserialize<JsonElement>(responseContent);

                if (element.ValueKind == JsonValueKind.Array)
                {
                    // Es un array: Deserializamos directamente a List<EmpresaDto>
                    var empresas = JsonSerializer.Deserialize<List<EmpresaDto>>(responseContent,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    return empresas ?? new List<EmpresaDto>();
                }
                else if (element.ValueKind == JsonValueKind.Object)
                {
                    // Es un objeto único: Deserializamos a EmpresaDto y lo envolvemos en una lista
                    var empresaUnica = JsonSerializer.Deserialize<EmpresaDto>(responseContent,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (empresaUnica != null)
                    {
                        Console.WriteLine($"[EmpresaService] Se encontró una empresa única: {empresaUnica.Nombre}");
                        return new List<EmpresaDto> { empresaUnica };
                    }
                }

                Console.WriteLine("[EmpresaService] Advertencia: Respuesta JSON no reconocida.");
                return new List<EmpresaDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmpresaService] Excepción al obtener empresas: {ex.Message}");
                return new List<EmpresaDto>();
            }
        }

        public async Task<(int empresaId, string message, bool success)> CreateEmpresaAsync(EmpresaCreationDto creationDto)
        {
            // 1. Obtener ID del usuario autenticado
            await _authService.EnsureAuthDataLoadedAsync();
            var userId = await _authService.GetUserIdAsync();

            if (!userId.HasValue || userId.Value <= 0)
            {
                return (0, "Error de autenticación: El ID de usuario no está disponible.", false);
            }

            // 2. Asignar el ID al DTO (Usando UsuarioId, el nombre C# corregido)
            creationDto.UsuarioId = userId.Value; // ¡CAMBIO AQUÍ!
                                                  // Asegúrate de que los campos en el DTO se llenan correctamente antes de esta llamada.

            var url = "empresa"; // Endpoint completo: /empresa

            try
            {
                // *** CAMBIO CRÍTICO: Serializar sin la política CamelCase ***
                // Los atributos [JsonPropertyName] ahora definen el casing del JSON.
                var jsonContent = JsonSerializer.Serialize(creationDto); // Eliminamos las opciones de CamelCase
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(url, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    // ... (Lógica de éxito se mantiene igual) ...
                    var createdEmpresa = JsonSerializer.Deserialize<EmpresaResponseDto>(responseContent,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (createdEmpresa?.Id > 0)
                    {
                        return (createdEmpresa.Id, $"Empresa creada con éxito (ID: {createdEmpresa.Id}).", true);
                    }

                    return (0, "Empresa creada, pero el API no devolvió un ID.", true);
                }
                else
                {
                    // ... (Manejo de Errores se mantiene, ya está mejorado) ...
                    var errorMsg = $"Error HTTP {response.StatusCode}. No se pudo crear la empresa.";

                    try
                    {
                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var apiError = JsonSerializer.Deserialize<ApiError>(responseContent, options);

                        if (apiError != null)
                        {
                            if (!string.IsNullOrWhiteSpace(apiError.message))
                            {
                                errorMsg = apiError.message;
                            }
                            else if (!string.IsNullOrWhiteSpace(apiError.error))
                            {
                                errorMsg = apiError.error;
                            }

                            if (apiError.errors != null && apiError.errors.Any())
                            {
                                var validationErrors = apiError.errors
                                    .SelectMany(kvp => kvp.Value.Select(error => $"{kvp.Key}: {error}"))
                                    .ToList();
                                errorMsg += "\n Detalles de Validación: " + string.Join("; ", validationErrors);
                            }
                        }
                        else
                        {
                            errorMsg = responseContent;
                        }
                    }
                    catch (JsonException)
                    {
                        errorMsg = responseContent;
                    }

                    return (0, $"Error al guardar: {errorMsg}", false);
                }
            }
            catch (HttpRequestException ex)
            {
                return (0, $"Error de conexión con el API: {ex.Message}", false);
            }
            catch (Exception ex)
            {
                return (0, $"Error inesperado: {ex.Message}", false);
            }
        }


    }
}