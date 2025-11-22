using FutZoneFrontend.DTOs;
using System.Net.Http.Json;
using System.Text.Json;
using System.Net.Http; // Necesario para HttpClient
using System.Collections.Generic;
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
            var url = $"empresa/{userId.Value}";
            
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
    }
}