using FutZoneFrontend.Services.Models;
using System.Net.Http.Json;

namespace FutZoneFrontend.Services
{
    public interface ICanchasService
    {
        Task<List<Cancha>> GetAllCanchasAsync();
        Task<Cancha?> GetCanchaByIdAsync(int id);
        Task<Cancha?> CreateCanchaAsync(Cancha cancha);
        Task<bool> UpdateCanchaAsync(int id, Cancha cancha);
        Task<bool> DeleteCanchaAsync(int id);
        Task<bool> DisableCanchaAsync(int id);
        Task<bool> EnableCanchaAsync(int id);
    }

    public class CanchasService : ICanchasService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://apicanchasyreservas.onrender.com";
        private const string BaseEndpoint = "/api/canchas";

        public CanchasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Cancha>> GetAllCanchasAsync()
        {
            try
            {
                var url = $"{BaseUrl}{BaseEndpoint}";
                Console.WriteLine($"Obteniendo canchas de: {url}");
                return await _httpClient.GetFromJsonAsync<List<Cancha>>(url) ?? new List<Cancha>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all canchas: {ex.Message}");
                return new List<Cancha>();
            }
        }

        public async Task<Cancha?> GetCanchaByIdAsync(int id)
        {
            try
            {
                var url = $"{BaseUrl}{BaseEndpoint}/{id}";
                return await _httpClient.GetFromJsonAsync<Cancha>(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting cancha by id: {ex.Message}");
                return null;
            }
        }

        public async Task<Cancha?> CreateCanchaAsync(Cancha cancha)
        {
            try
            {
                var url = $"{BaseUrl}{BaseEndpoint}";
                Console.WriteLine($"Creando cancha en: {url}");
                var response = await _httpClient.PostAsJsonAsync(url, cancha);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Cancha>();
                }
                Console.WriteLine($"Error al crear: {response.StatusCode} - {response.ReasonPhrase}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating cancha: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateCanchaAsync(int id, Cancha cancha)
        {
            try
            {
                var url = $"{BaseUrl}{BaseEndpoint}/{id}";
                var response = await _httpClient.PutAsJsonAsync(url, cancha);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating cancha: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteCanchaAsync(int id)
        {
            try
            {
                var url = $"{BaseUrl}{BaseEndpoint}/{id}";
                var response = await _httpClient.DeleteAsync(url);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting cancha: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DisableCanchaAsync(int id)
        {
            try
            {
                var url = $"{BaseUrl}{BaseEndpoint}/{id}/deshabilitar";
                var response = await _httpClient.PutAsync(url, null);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error disabling cancha: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> EnableCanchaAsync(int id)
        {
            try
            {
                var url = $"{BaseUrl}{BaseEndpoint}/{id}/habilitar";
                var response = await _httpClient.PutAsync(url, null);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error enabling cancha: {ex.Message}");
                return false;
            }
        }
    }
}
