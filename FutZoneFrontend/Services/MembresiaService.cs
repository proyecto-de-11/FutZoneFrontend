using FutZoneFrontend.Services.Models;
using System.Net.Http.Json;

namespace FutZoneFrontend.Services
{
    public interface IMembresiaService
    {
        Task<List<Membresia>> GetAllMembresiasAsync();
        Task<Membresia?> GetMembresiaByIdAsync(int id);
        Task<Membresia?> CreateMembresiaAsync(Membresia membresia);
        Task<bool> UpdateMembresiaAsync(int id, Membresia membresia);
        Task<bool> DeleteMembresiaAsync(int id);
    }

    public class MembresiaService : IMembresiaService
    {
        private readonly HttpClient _httpClient;
        private const string BaseEndpoint = "/api/membresias";

        public MembresiaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Membresia>> GetAllMembresiasAsync()
        {
            try
            {
                // Intentar sin parámetros primero
                try
                {
                    return await _httpClient.GetFromJsonAsync<List<Membresia>>($"{BaseEndpoint}") 
                        ?? new List<Membresia>();
                }
                catch
                {
                    // Si falla, intentar con /lista
                    return await _httpClient.GetFromJsonAsync<List<Membresia>>($"{BaseEndpoint}/lista") 
                        ?? new List<Membresia>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all membresias: {ex.Message}");
                return new List<Membresia>();
            }
        }

        public async Task<Membresia?> GetMembresiaByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Membresia>($"{BaseEndpoint}/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting membresia by id: {ex.Message}");
                return null;
            }
        }

        public async Task<Membresia?> CreateMembresiaAsync(Membresia membresia)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(BaseEndpoint, membresia);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Membresia>();
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating membresia: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateMembresiaAsync(int id, Membresia membresia)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{BaseEndpoint}/{id}", membresia);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating membresia: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteMembresiaAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{BaseEndpoint}/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting membresia: {ex.Message}");
                return false;
            }
        }
    }
}
