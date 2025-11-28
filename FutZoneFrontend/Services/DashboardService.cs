using FutZoneFrontend.Services.Models;
using System.Net.Http.Json;

namespace FutZoneFrontend.Services
{
    public interface IDashboardService
    {
        Task<DashboardStatsDTO?> GetDashboardStatsAsync();
        Task<List<EmpresaDTO>?> GetEmpresasAsync();
        Task<List<RolDTO>?> GetRolesAsync();
        Task<List<ActividadDTO>?> GetActividadRecienteAsync();
        Task<bool> CreateEmpresaAsync(EmpresaDTO empresa);
        Task<bool> UpdateEmpresaAsync(int id, EmpresaDTO empresa);
        Task<bool> DeleteEmpresaAsync(int id);
        Task<bool> CreateRolAsync(RolDTO rol);
    }

    public class DashboardService : IDashboardService
    {
        private readonly HttpClient _httpClient;
        private readonly IRolService _rolService;
        private const string BaseEndpoint = "/api/dashboard";

        public DashboardService(HttpClient httpClient, IRolService rolService)
        {
            _httpClient = httpClient;
            _rolService = rolService;
        }

        public async Task<DashboardStatsDTO?> GetDashboardStatsAsync()
        {
            try
            {
                // Intentar obtener estadísticas del dashboard
                try
                {
                    return await _httpClient.GetFromJsonAsync<DashboardStatsDTO>($"{BaseEndpoint}/stats");
                }
                catch
                {
                    // Si falla, intentar construir estadísticas desde otros endpoints
                    Console.WriteLine("Intentando obtener stats desde endpoints alternativos...");
                    
                    // Intentar obtener datos agregados
                    try
                    {
                        var stats = await _httpClient.GetFromJsonAsync<DashboardStatsDTO>($"{BaseEndpoint}");
                        if (stats != null) return stats;
                    }
                    catch { }
                    
                    // Si no hay endpoint de dashboard, retornar null y dejar que el frontend manaje
                    throw new Exception("Endpoint de dashboard no disponible");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting dashboard stats: {ex.Message}");
                return null;
            }
        }

        public async Task<List<EmpresaDTO>?> GetEmpresasAsync()
        {
            try
            {
                // Intentar con diferentes rutas
                try
                {
                    return await _httpClient.GetFromJsonAsync<List<EmpresaDTO>>("/api/empresas");
                }
                catch
                {
                    try
                    {
                        return await _httpClient.GetFromJsonAsync<List<EmpresaDTO>>("/api/empresas/lista");
                    }
                    catch
                    {
                        return new List<EmpresaDTO>();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting empresas: {ex.Message}");
                return new List<EmpresaDTO>();
            }
        }

        public async Task<List<RolDTO>?> GetRolesAsync()
        {
            try
            {
                // Usar el servicio de roles si está disponible
                var roles = await _rolService.GetAllRolesAsync();
                return roles.Select(r => new RolDTO 
                { 
                    Id = r.Id, 
                    Nombre = r.Nombre, 
                    Descripcion = r.Descripcion ?? "",
                    Usuarios = 0 // Por ahora no hay forma de obtener cantidad de usuarios por rol
                }).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting roles: {ex.Message}");
                return new List<RolDTO>();
            }
        }

        public async Task<List<ActividadDTO>?> GetActividadRecienteAsync()
        {
            try
            {
                // Intentar obtener actividad reciente
                try
                {
                    return await _httpClient.GetFromJsonAsync<List<ActividadDTO>>($"{BaseEndpoint}/actividad-reciente");
                }
                catch
                {
                    // Si falla, intentar ruta alternativa
                    try
                    {
                        return await _httpClient.GetFromJsonAsync<List<ActividadDTO>>("/api/actividades");
                    }
                    catch
                    {
                        // Si no hay datos de actividad, retornar lista vacía
                        return new List<ActividadDTO>();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting actividad reciente: {ex.Message}");
                return new List<ActividadDTO>();
            }
        }

        public async Task<bool> CreateEmpresaAsync(EmpresaDTO empresa)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/empresas", empresa);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating empresa: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateEmpresaAsync(int id, EmpresaDTO empresa)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"/api/empresas/{id}", empresa);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating empresa: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteEmpresaAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/empresas/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting empresa: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> CreateRolAsync(RolDTO rol)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/roles", rol);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating rol: {ex.Message}");
                return false;
            }
        }
    }

    public class DashboardStatsDTO
    {
        public int EmpresasActivas { get; set; }
        public int UsuariosTotales { get; set; }
        public int ReservasEsteMes { get; set; }
        public decimal IngresosMensuales { get; set; }
    }

    public class EmpresaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string RUC { get; set; } = "";
        public int NumeroEmpleados { get; set; }
        public bool Activa { get; set; }
    }

    public class RolDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public int Usuarios { get; set; }
    }

    public class ActividadDTO
    {
        public string Titulo { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string Hora { get; set; } = "";
        public string Icono { get; set; } = "";
    }
}
