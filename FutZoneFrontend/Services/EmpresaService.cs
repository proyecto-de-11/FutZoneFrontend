using FutZoneFrontend.DTOs;
using System.Net.Http.Json;
using System.Text.Json;
using System.Net.Http; // Necesario para HttpClient
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FutZoneFrontend.Services
{
    public interface IEmpresaService
    {
        Task<List<EmpresaDto>> GetEmpresasDelUsuarioAsync();
        Task<List<EmpresaDto>> GetAllEmpresasAsync();
        Task<EmpresaDto?> GetEmpresaByIdAsync(int id);
        Task<bool> CreateEmpresaAsync(EmpresaDto empresa);
        Task<bool> UpdateEmpresaAsync(int id, EmpresaDto empresa);
        Task<bool> DeleteEmpresaAsync(int id);
    }

    public class EmpresaService : IEmpresaService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService; // Para obtener el ID del usuario
        private readonly JsonSerializerOptions _jsonOptions;

        // Inyectamos el HttpClient (configurado como "ApiClient") y el AuthService
        public EmpresaService(HttpClient httpClient, IAuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
        }

        /// <summary>
        /// Agrega el user_id al header de la petición
        /// </summary>
        private async Task AddUserIdHeaderAsync()
        {
            var userId = await _authService.GetUserIdAsync();
            if (userId.HasValue && userId.Value > 0)
            {
                _httpClient.DefaultRequestHeaders.Remove("user_id");
                _httpClient.DefaultRequestHeaders.Add("user_id", userId.Value.ToString());
            }
        }

        /// <summary>
        /// Obtiene las organizaciones del usuario autenticado.
        /// Trae todas las empresas y filtra las que pertenecen al usuario logueado.
        /// </summary>
        /// <returns>Una lista de EmpresaDto del usuario o una lista vacía si falla o no hay empresas.</returns>
        public async Task<List<EmpresaDto>> GetEmpresasDelUsuarioAsync()
        {
            // 1. Obtener el ID del usuario de la sesión
            var userId = await _authService.GetUserIdAsync();

            if (!userId.HasValue || userId.Value <= 0)
            {
                Console.WriteLine("[EmpresaService] Error: No se pudo obtener el ID del usuario autenticado.");
                return new List<EmpresaDto>();
            }

            Console.WriteLine($"[EmpresaService] Obteniendo empresas para usuario ID: {userId.Value}");

            try
            {
                // 2. Obtener TODAS las empresas
                var todasLasEmpresas = await GetAllEmpresasAsync();

                if (!todasLasEmpresas.Any())
                {
                    Console.WriteLine($"[EmpresaService] No hay empresas en el sistema");
                    return new List<EmpresaDto>();
                }

                // 3. Filtrar empresas que pertenecen al usuario logueado
                var empresasDelUsuario = todasLasEmpresas
                    .Where(e => e.usuario_id == userId.Value)
                    .ToList();

                Console.WriteLine($"[EmpresaService] Se encontraron {empresasDelUsuario.Count} empresas del usuario {userId.Value}");

                return empresasDelUsuario;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmpresaService] Excepción al obtener empresas del usuario: {ex.Message}");
                return new List<EmpresaDto>();
            }
        }

        /// <summary>
        /// Obtiene todas las empresas del sistema.
        /// </summary>
        public async Task<List<EmpresaDto>> GetAllEmpresasAsync()
        {
            // Agregar user_id al header (aunque sea para obtener todas)
            await AddUserIdHeaderAsync();

            try
            {
                Console.WriteLine("[EmpresaService] Obteniendo todas las empresas...");
                var response = await _httpClient.GetAsync("empresa");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"[EmpresaService] Error HTTP: {response.StatusCode} al obtener todas las empresas");
                    return new List<EmpresaDto>();
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[EmpresaService] Response GetAll: {responseContent.Substring(0, Math.Min(100, responseContent.Length))}...");

                var empresas = await response.Content.ReadFromJsonAsync<List<EmpresaDto>>(
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                Console.WriteLine($"[EmpresaService] Se obtuvieron {empresas?.Count ?? 0} empresas totales");
                return empresas ?? new List<EmpresaDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmpresaService] Excepción al obtener todas las empresas: {ex.Message}");
                return new List<EmpresaDto>();
            }
        }

        /// <summary>
        /// Obtiene una empresa por su ID.
        /// </summary>
        public async Task<EmpresaDto?> GetEmpresaByIdAsync(int id)
        {
            // Agregar user_id al header
            await AddUserIdHeaderAsync();

            try
            {
                var response = await _httpClient.GetAsync($"empresa/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"[EmpresaService] Error HTTP: {response.StatusCode} al obtener empresa {id}");
                    return null;
                }

                var empresa = await response.Content.ReadFromJsonAsync<EmpresaDto>(
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return empresa;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmpresaService] Excepción al obtener empresa {id}: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Crea una nueva empresa.
        /// </summary>
        public async Task<bool> CreateEmpresaAsync(EmpresaDto empresa)
        {
            // Agregar user_id al header
            await AddUserIdHeaderAsync();

            try
            {
                var response = await _httpClient.PostAsJsonAsync("empresa", empresa);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"[EmpresaService] Error HTTP: {response.StatusCode} al crear empresa. Detalle: {errorContent}");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmpresaService] Excepción al crear empresa: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Actualiza una empresa existente.
        /// </summary>
        public async Task<bool> UpdateEmpresaAsync(int id, EmpresaDto empresa)
        {
            // Agregar user_id al header
            await AddUserIdHeaderAsync();

            try
            {
                Console.WriteLine($"[EmpresaService] Iniciando actualización de empresa {id}...");
                Console.WriteLine($"[EmpresaService] URL: empresa/{id}");
                Console.WriteLine($"[EmpresaService] Nombre empresa: {empresa.Nombre}");
                
                // Serializar a JSON para ver exactamente qué se envía
                var jsonContent = JsonSerializer.Serialize(empresa, _jsonOptions);
                Console.WriteLine($"[EmpresaService] JSON payload: {jsonContent}");
                
                // Crear la solicitud manualmente para mayor control
                var request = new HttpRequestMessage(HttpMethod.Put, $"empresa/{id}");
                request.Content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                
                Console.WriteLine($"[EmpresaService] Enviando solicitud PUT...");
                var response = await _httpClient.SendAsync(request);
                
                Console.WriteLine($"[EmpresaService] Status code: {response.StatusCode}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"[EmpresaService] ❌ Error HTTP: {response.StatusCode}");
                    Console.WriteLine($"[EmpresaService] Detalle: {errorContent}");
                    return false;
                }

                var successContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[EmpresaService] ✅ Empresa {id} actualizada exitosamente");
                Console.WriteLine($"[EmpresaService] Response: {successContent}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmpresaService] ❌ Excepción: {ex.Message}");
                Console.WriteLine($"[EmpresaService] Stack: {ex.StackTrace}");
                return false;
            }
        }

        /// <summary>
        /// Elimina una empresa.
        /// </summary>
        public async Task<bool> DeleteEmpresaAsync(int id)
        {
            // Agregar user_id al header
            await AddUserIdHeaderAsync();

            try
            {
                Console.WriteLine($"[EmpresaService] Eliminando empresa {id}...");
                var response = await _httpClient.DeleteAsync($"empresa/{id}");

                Console.WriteLine($"[EmpresaService] Status code: {response.StatusCode}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"[EmpresaService] ❌ Error HTTP: {response.StatusCode} al eliminar empresa {id}. Detalle: {errorContent}");
                    return false;
                }

                Console.WriteLine($"[EmpresaService] ✅ Empresa {id} eliminada exitosamente");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmpresaService] ❌ Excepción al eliminar empresa {id}: {ex.Message}");
                return false;
            }
        }
    }
}