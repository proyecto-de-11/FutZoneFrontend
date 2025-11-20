using FutZoneFrontend.Services.Models;
using System.Net.Http.Json;

namespace FutZoneFrontend.Services
{
    public interface IDocumentosLegalesService
    {
        Task<List<DocumentoLegal>> GetAllAsync();
        Task<DocumentoLegal?> GetByIdAsync(int id);
    }

    public class DocumentosLegalesService : IDocumentosLegalesService
    {
        private readonly HttpClient _httpClient;
        private const string BaseEndpoint = "/api/documentoslegales";

        public DocumentosLegalesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DocumentoLegal>> GetAllAsync()
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<DocumentoLegal>>(BaseEndpoint);
                return result ?? new List<DocumentoLegal>();
            }
            catch
            {
                return new List<DocumentoLegal>();
            }
        }

        public async Task<DocumentoLegal?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<DocumentoLegal>($"{BaseEndpoint}/{id}");
            }
            catch
            {
                return null;
            }
        }
    }
}
