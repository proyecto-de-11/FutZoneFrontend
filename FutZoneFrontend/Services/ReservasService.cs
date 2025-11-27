using System.Net.Http.Json;
using System.Text.Json.Serialization;
using FutZoneFrontend.Services.Models;

namespace FutZoneFrontend.Services
{
    public interface IReservasService
    {
        Task<List<ReservaDto>> GetAllReservasAsync();
        Task<ReservaDto?> GetReservaByIdAsync(int id);
        Task<ReservaDto?> CreateReservaAsync(CreateReservaDto reserva);
        Task<bool> ProcessReservaAsync(int solicitudId);
        Task<bool> DeleteSolicitudAsync(int id);
        Task<bool> DeleteReservaAsync(int id);
    }

    public class CreateReservaDto
    {
        [JsonPropertyName("cancha_id")]
        public int CanchaId { get; set; }

        [JsonPropertyName("equipo_id")]
        public int? EquipoId { get; set; }

        [JsonPropertyName("usuario_solicitante_id")]
        public int UsuarioSolicitanteId { get; set; }

        [JsonPropertyName("fecha_reserva")]
        public string? FechaReserva { get; set; }

        [JsonPropertyName("hora_inicio")]
        public string? HoraInicio { get; set; }

        [JsonPropertyName("hora_fin")]
        public string? HoraFin { get; set; }

        [JsonPropertyName("duracion_horas")]
        public decimal DuracionHoras { get; set; }

        [JsonPropertyName("monto_total")]
        public decimal MontoTotal { get; set; }

        [JsonPropertyName("mensaje_solicitud")]
        public string? MensajeSolicitud { get; set; }
    }

    public class ReservaDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("cancha_id")]
        public int CanchaId { get; set; }

        [JsonPropertyName("equipo_id")]
        public int? EquipoId { get; set; }

        [JsonPropertyName("usuario_solicitante_id")]
        public int? UsuarioSolicitanteId { get; set; }

        [JsonPropertyName("usuario_id")]
        public int? UsuarioId { get; set; }

        [JsonPropertyName("partido_id")]
        public int? PartidoId { get; set; }

        [JsonPropertyName("fecha_reserva")]
        public DateTime? FechaReserva { get; set; }

        [JsonPropertyName("hora_inicio")]
        public string? HoraInicio { get; set; }

        [JsonPropertyName("hora_fin")]
        public string? HoraFin { get; set; }

        [JsonPropertyName("duracion_horas")]
        public string? DuracionHoras { get; set; }

        [JsonPropertyName("monto_total")]
        public string? MontoTotal { get; set; }

        [JsonPropertyName("precio_total")]
        public decimal? PrecioTotal { get; set; }

        [JsonPropertyName("estado")]
        public string? Estado { get; set; }

        [JsonPropertyName("observaciones")]
        public string? Observaciones { get; set; }

        [JsonPropertyName("mensaje_solicitud")]
        public string? MensajeSolicitud { get; set; }

        [JsonPropertyName("fecha_creacion")]
        public DateTime? FechaCreacion { get; set; }

        [JsonPropertyName("fecha_actualizacion")]
        public DateTime? FechaActualizacion { get; set; }

        // Propiedades derivadas para compatibilidad
        public DateTime? FechaInicio => FechaReserva;
        public DateTime? FechaFin => FechaReserva;
    }

    public class ReservasService : IReservasService
    {
        private readonly HttpClient _httpClient;
        private const string BaseEndpoint = "/api/reservas";

        public ReservasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ReservaDto>> GetAllReservasAsync()
        {
            try
            {
                var url = BaseEndpoint;
                Console.WriteLine($"[ReservasService] Obteniendo reservas de: {_httpClient.BaseAddress}{url}");
                var options = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                
                var response = await _httpClient.GetAsync(url);
                
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"[ReservasService] ❌ Error HTTP: {response.StatusCode}");
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"[ReservasService] Error content: {errorContent}");
                    return new List<ReservaDto>();
                }
                
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[ReservasService] Response length: {content.Length}");
                
                var result = System.Text.Json.JsonSerializer.Deserialize<List<ReservaDto>>(content, options) ?? new List<ReservaDto>();
                Console.WriteLine($"[ReservasService] ✅ Reservas desserializadas: {result.Count}");
                
                foreach (var reserva in result)
                {
                    Console.WriteLine($"[ReservasService]   - Reserva ID: {reserva.Id}, Cancha: {reserva.CanchaId}, Estado: {reserva.Estado}");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ReservasService] ❌ Error getting all reservas: {ex.Message}");
                Console.WriteLine($"[ReservasService] Stack trace: {ex.StackTrace}");
                return new List<ReservaDto>();
            }
        }

        public async Task<ReservaDto?> GetReservaByIdAsync(int id)
        {
            try
            {
                var url = $"{BaseEndpoint}/{id}";
                var options = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return await _httpClient.GetFromJsonAsync<ReservaDto>(url, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting reserva by id: {ex.Message}");
                return null;
            }
        }

        public async Task<ReservaDto?> CreateReservaAsync(CreateReservaDto reserva)
        {
            try
            {
                var url = BaseEndpoint;
                Console.WriteLine($"[ReservasService] Creando reserva en: {_httpClient.BaseAddress}{url}");
                Console.WriteLine($"[ReservasService] Payload: {System.Text.Json.JsonSerializer.Serialize(reserva)}");
                
                var response = await _httpClient.PostAsJsonAsync(url, reserva);
                
                Console.WriteLine($"[ReservasService] Response Status: {response.StatusCode}");
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[ReservasService] Response Body: {content}");
                
                if (response.IsSuccessStatusCode)
                {
                    var options = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return System.Text.Json.JsonSerializer.Deserialize<ReservaDto>(content, options);
                }
                else
                {
                    Console.WriteLine($"[ReservasService] ❌ Error al crear: {response.StatusCode} - {response.ReasonPhrase}");
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ReservasService] ❌ Exception creating reserva: {ex.Message}");
                Console.WriteLine($"[ReservasService] Stack trace: {ex.StackTrace}");
                return null;
            }
        }

        public async Task<bool> ProcessReservaAsync(int solicitudId)
        {
            try
            {
                var url = $"/api/solicitudes/{solicitudId}/process";
                var response = await _httpClient.PutAsync(url, null);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing reserva: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteSolicitudAsync(int id)
        {
            try
            {
                var url = $"/api/solicitudes/{id}";
                var response = await _httpClient.DeleteAsync(url);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting solicitud: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteReservaAsync(int id)
        {
            try
            {
                var url = $"{BaseEndpoint}/{id}";
                var response = await _httpClient.DeleteAsync(url);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting reserva: {ex.Message}");
                return false;
            }
        }
    }
}
