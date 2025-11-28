using FutZoneFrontend.Services.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace FutZoneFrontend.Services
{
    public interface IPropietarioService
    {
        Task<PerfilResponse> GetPerfilAsync(int id);
        Task<PerfilResponse> CreatePerfilAsync(PerfilRequest request);
        Task<PerfilResponse> UpdatePerfilAsync(int id, PerfilRequest request);
        Task<TipoDeporteResponse> GetTiposDeporteAsync();
        Task<MembresiaResponse> GetMembresiasAsync();
        Task<UsuarioMembresiaResponse> GetMisMembresiasAsync();
    }

    public class PropietarioService : IPropietarioService
    {
        private readonly HttpClient _httpClient;
        private readonly ITipoDeporteService _tipoDeporteService;
        private readonly IMembresiaService _membresiaService;

        public PropietarioService(HttpClient httpClient, ITipoDeporteService tipoDeporteService, IMembresiaService membresiaService)
        {
            _httpClient = httpClient;
            _tipoDeporteService = tipoDeporteService;
            _membresiaService = membresiaService;
        }

        public async Task<PerfilResponse> GetPerfilAsync(int id)
        {
            try
            {
                Console.WriteLine($"Obteniendo perfil ID: {id}");
                var response = await _httpClient.GetAsync($"/api/perfiles/{id}");
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var perfil = JsonSerializer.Deserialize<Perfil>(content, 
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    
                    return new PerfilResponse 
                    { 
                        Success = true, 
                        Data = perfil 
                    };
                }

                return new PerfilResponse 
                { 
                    Success = false, 
                    Message = "Error al obtener perfil" 
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new PerfilResponse 
                { 
                    Success = false, 
                    Message = ex.Message 
                };
            }
        }

        public async Task<PerfilResponse> CreatePerfilAsync(PerfilRequest request)
        {
            try
            {
                Console.WriteLine($"Creando nuevo perfil");
                var response = await _httpClient.PostAsJsonAsync("/api/perfiles", request);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var perfil = JsonSerializer.Deserialize<Perfil>(content, 
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    
                    return new PerfilResponse 
                    { 
                        Success = true, 
                        Data = perfil,
                        Message = "Perfil creado exitosamente"
                    };
                }

                return new PerfilResponse 
                { 
                    Success = false, 
                    Message = "Error al crear perfil" 
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new PerfilResponse 
                { 
                    Success = false, 
                    Message = ex.Message 
                };
            }
        }

        public async Task<PerfilResponse> UpdatePerfilAsync(int id, PerfilRequest request)
        {
            try
            {
                Console.WriteLine($"Actualizando perfil ID: {id}");
                var response = await _httpClient.PutAsJsonAsync($"/api/perfiles/{id}", request);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var perfil = JsonSerializer.Deserialize<Perfil>(content, 
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    
                    return new PerfilResponse 
                    { 
                        Success = true, 
                        Data = perfil,
                        Message = "Perfil actualizado exitosamente"
                    };
                }

                return new PerfilResponse 
                { 
                    Success = false, 
                    Message = "Error al actualizar perfil" 
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new PerfilResponse 
                { 
                    Success = false, 
                    Message = ex.Message 
                };
            }
        }

        public async Task<TipoDeporteResponse> GetTiposDeporteAsync()
        {
            try
            {
                Console.WriteLine("Obteniendo tipos de deporte");
                var deportes = await _tipoDeporteService.GetAllTiposDeporteAsync();
                
                return new TipoDeporteResponse 
                { 
                    Success = deportes.Count > 0, 
                    Data = deportes,
                    Message = deportes.Count > 0 ? "Tipos de deporte obtenidos" : "No hay tipos de deporte disponibles"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new TipoDeporteResponse 
                { 
                    Success = false, 
                    Message = $"Error al obtener tipos de deporte: {ex.Message}" 
                };
            }
        }

        public async Task<MembresiaResponse> GetMembresiasAsync()
        {
            try
            {
                Console.WriteLine("Obteniendo membresías");
                var membresias = await _membresiaService.GetAllMembresiasAsync();
                
                return new MembresiaResponse 
                { 
                    Success = membresias.Count > 0, 
                    Data = membresias,
                    Message = membresias.Count > 0 ? "Membresías obtenidas" : "No hay membresías disponibles"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new MembresiaResponse 
                { 
                    Success = false, 
                    Message = $"Error al obtener membresías: {ex.Message}" 
                };
            }
        }

        public async Task<UsuarioMembresiaResponse> GetMisMembresiasAsync()
        {
            try
            {
                Console.WriteLine("Obteniendo mis membresías");
                var response = await _httpClient.GetAsync("/api/usuario-membresias/lista");
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var membresias = JsonSerializer.Deserialize<List<UsuarioMembresia>>(content, 
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
                    
                    return new UsuarioMembresiaResponse 
                    { 
                        Success = true, 
                        Data = membresias 
                    };
                }

                return new UsuarioMembresiaResponse 
                { 
                    Success = false, 
                    Message = "Error al obtener membresías" 
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new UsuarioMembresiaResponse 
                { 
                    Success = false, 
                    Message = ex.Message 
                };
            }
        }
    }
}
