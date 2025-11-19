using FutZoneFrontend.Services.Models;
using System.Net.Http.Json;

namespace FutZoneFrontend.Services
{
    public interface IPreferenciasService
    {
        Task<List<Preferencia>> GetAllAsync();
        Task<Preferencia?> GetByUsuarioAsync(int usuarioId);
    }

    public class PreferenciasService : IPreferenciasService
    {
        private readonly HttpClient _httpClient;
        private const string BaseEndpoint = "/api/preferencias";

        public PreferenciasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Preferencia>> GetAllAsync()
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<Preferencia>>(BaseEndpoint);
                return result ?? new List<Preferencia>();
            }
            catch
            {
                return new List<Preferencia>();
            }
        }

        public async Task<Preferencia?> GetByUsuarioAsync(int usuarioId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Preferencia>($"{BaseEndpoint}/usuario/{usuarioId}");
            }
            catch
            {
                return null;
            }
        }
    }
}
