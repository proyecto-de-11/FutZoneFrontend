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

        [JsonPropertyName("usuario_id")]
        public int? UsuarioId { get; set; }

        [JsonPropertyName("usuario_solicitante_id")]
        public int? UsuarioSolicitanteId { get; set; }

        [JsonPropertyName("fecha_reserva")]
        public string? FechaReserva { get; set; }

        [JsonPropertyName("fecha_inicio")]
        public DateTime? FechaInicio { get; set; }

        [JsonPropertyName("fecha_fin")]
        public DateTime? FechaFin { get; set; }

        [JsonPropertyName("hora_inicio")]
        public string? HoraInicio { get; set; }

        [JsonPropertyName("hora_fin")]
        public string? HoraFin { get; set; }

        [JsonPropertyName("duracion_horas")]
        public decimal DuracionHoras { get; set; }

        [JsonPropertyName("monto_total")]
        public decimal MontoTotal { get; set; }

        [JsonPropertyName("precio_total")]
        public decimal? PrecioTotal { get; set; }

        [JsonPropertyName("estado")]
        public string? Estado { get; set; }

        [JsonPropertyName("observaciones")]
        public string? Observaciones { get; set; }

        [JsonPropertyName("mensaje_solicitud")]
        public string? MensajeSolicitud { get; set; }
    }

    public class ReservasService : IReservasService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://apicanchasyreservas.onrender.com";
        private const string BaseEndpoint = "/api/reservas";

        public ReservasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ReservaDto>> GetAllReservasAsync()
        {
            try
            {
                var url = $"{BaseUrl}{BaseEndpoint}";
                Console.WriteLine($"Obteniendo reservas de: {url}");
                var options = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return await _httpClient.GetFromJsonAsync<List<ReservaDto>>(url, options) ?? new List<ReservaDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all reservas: {ex.Message}");
                return new List<ReservaDto>();
            }
        }

        public async Task<ReservaDto?> GetReservaByIdAsync(int id)
        {
            try
            {
                var url = $"{BaseUrl}{BaseEndpoint}/{id}";
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
                var url = $"{BaseUrl}{BaseEndpoint}";
                Console.WriteLine($"Creando reserva en: {url}");
                var response = await _httpClient.PostAsJsonAsync(url, reserva);
                if (response.IsSuccessStatusCode)
                {
                    var options = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return await response.Content.ReadFromJsonAsync<ReservaDto>(options);
                }
                Console.WriteLine($"Error al crear: {response.StatusCode} - {response.ReasonPhrase}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating reserva: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> ProcessReservaAsync(int solicitudId)
        {
            try
            {
                var url = $"{BaseUrl}/api/solicitudes/{solicitudId}/process";
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
                var url = $"{BaseUrl}/api/solicitudes/{id}";
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
                var url = $"{BaseUrl}{BaseEndpoint}/{id}";
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
