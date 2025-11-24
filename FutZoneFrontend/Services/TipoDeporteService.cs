using FutZoneFrontend.Services.Models;
using System.Net.Http.Json;

namespace FutZoneFrontend.Services
{
    public interface ITipoDeporteService
    {
        Task<PaginatedResponse<TipoDeporte>> GetTiposDeportePaginatedAsync(int page = 0, int size = 10);
        Task<List<TipoDeporte>> GetAllTiposDeporteAsync();
        Task<TipoDeporte?> GetTipoDeporteByIdAsync(int id);
        Task<TipoDeporte?> CreateTipoDeporteAsync(TipoDeporte tipoDeporte);
        Task<bool> UpdateTipoDeporteAsync(int id, TipoDeporte tipoDeporte);
        Task<bool> DeleteTipoDeporteAsync(int id);
    }

    public class TipoDeporteService : ITipoDeporteService
    {
        private readonly HttpClient _httpClient;
        private const string BaseEndpoint = "/api/tiposdeporte";

        public TipoDeporteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginatedResponse<TipoDeporte>> GetTiposDeportePaginatedAsync(int page = 0, int size = 10)
        {
            try
            {
                var url = $"{BaseEndpoint}?page={page}&size={size}";
                return await _httpClient.GetFromJsonAsync<PaginatedResponse<TipoDeporte>>(url) 
                    ?? new PaginatedResponse<TipoDeporte>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting paged tipos deporte: {ex.Message}");
                return new PaginatedResponse<TipoDeporte>();
            }
        }

        public async Task<List<TipoDeporte>> GetAllTiposDeporteAsync()
        {
            try
            {
                // Intentar con /lista primero (endpoint actual)
                try
                {
                    return await _httpClient.GetFromJsonAsync<List<TipoDeporte>>($"{BaseEndpoint}/lista") 
                        ?? new List<TipoDeporte>();
                }
                catch
                {
                    // Si falla, intentar sin /lista
                    return await _httpClient.GetFromJsonAsync<List<TipoDeporte>>($"{BaseEndpoint}") 
                        ?? new List<TipoDeporte>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all tipos deporte: {ex.Message}");
                return new List<TipoDeporte>();
            }
        }

        public async Task<TipoDeporte?> GetTipoDeporteByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<TipoDeporte>($"{BaseEndpoint}/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting tipo deporte by id: {ex.Message}");
                return null;
            }
        }

        public async Task<TipoDeporte?> CreateTipoDeporteAsync(TipoDeporte tipoDeporte)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(BaseEndpoint, tipoDeporte);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<TipoDeporte>();
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating tipo deporte: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateTipoDeporteAsync(int id, TipoDeporte tipoDeporte)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{BaseEndpoint}/{id}", tipoDeporte);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating tipo deporte: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteTipoDeporteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{BaseEndpoint}/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting tipo deporte: {ex.Message}");
                return false;
            }
        }
    }
}
