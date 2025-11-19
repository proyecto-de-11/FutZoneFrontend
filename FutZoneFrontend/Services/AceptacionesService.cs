using FutZoneFrontend.Services.Models;
using System.Net.Http.Json;

namespace FutZoneFrontend.Services
{
    public interface IAceptacionesService
    {
        Task<List<Aceptacion>> GetAllAsync();
        Task<Aceptacion?> GetByIdAsync(int id);
    }

    public class AceptacionesService : IAceptacionesService
    {
        private readonly HttpClient _httpClient;
        private const string BaseEndpoint = "/api/aceptaciones";

        public AceptacionesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Aceptacion>> GetAllAsync()
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<Aceptacion>>(BaseEndpoint);
                return result ?? new List<Aceptacion>();
            }
            catch
            {
                return new List<Aceptacion>();
            }
        }

        public async Task<Aceptacion?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Aceptacion>($"{BaseEndpoint}/{id}");
            }
            catch
            {
                return null;
            }
        }
    }
}
