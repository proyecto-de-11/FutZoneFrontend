using FutZoneFrontend.Services.Models;
using System.Net.Http.Json;

namespace FutZoneFrontend.Services
{
    public interface IRolService
    {
        Task<List<Rol>> GetAllRolesAsync();
        Task<Rol?> GetRoleByIdAsync(int id);
        Task<PaginatedResponse<Rol>> GetRolesPagedAsync(string? busqueda = null, int page = 0, int size = 10);
        Task<Rol?> CreateRoleAsync(Rol rol);
        Task<bool> UpdateRoleAsync(int id, Rol rol);
        Task<bool> DeleteRoleAsync(int id);
    }

    public class RolService : IRolService
    {
        private readonly HttpClient _httpClient;
        private const string BaseEndpoint = "/api/roles";

        public RolService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Rol>> GetAllRolesAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Rol>>(BaseEndpoint) ?? new List<Rol>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all roles: {ex.Message}");
                return new List<Rol>();
            }
        }

        public async Task<Rol?> GetRoleByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Rol>($"{BaseEndpoint}/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting role by id: {ex.Message}");
                return null;
            }
        }

        public async Task<PaginatedResponse<Rol>> GetRolesPagedAsync(string? busqueda = null, int page = 0, int size = 10)
        {
            try
            {
                var url = $"{BaseEndpoint}/paginado?page={page}&size={size}";
                if (!string.IsNullOrEmpty(busqueda))
                {
                    url += $"&busqueda={Uri.EscapeDataString(busqueda)}";
                }

                return await _httpClient.GetFromJsonAsync<PaginatedResponse<Rol>>(url) 
                    ?? new PaginatedResponse<Rol>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting paged roles: {ex.Message}");
                return new PaginatedResponse<Rol>();
            }
        }

        public async Task<Rol?> CreateRoleAsync(Rol rol)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(BaseEndpoint, rol);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Rol>();
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating role: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateRoleAsync(int id, Rol rol)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{BaseEndpoint}/{id}", rol);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating role: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{BaseEndpoint}/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting role: {ex.Message}");
                return false;
            }
        }
    }
}
