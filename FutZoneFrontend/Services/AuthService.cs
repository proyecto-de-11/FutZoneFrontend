using FutZoneFrontend.Services.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace FutZoneFrontend.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        Task LogoutAsync();
        string? GetToken();
        void SetToken(string token);
        void ClearToken();
    }

    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private const string TokenKey = "auth_token";

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            try
            {
                // GET /api/usuarios con email y contrasena
                var url = $"/api/usuarios?email={Uri.EscapeDataString(request.Email)}&contrasena={Uri.EscapeDataString(request.Password)}";
                
                Console.WriteLine($"Login Request URL: {url}");
                var response = await _httpClient.GetAsync(url);
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Login Response: {jsonContent}");
                    
                    // Intentar deserializar como lista primero
                    var usuarios = JsonSerializer.Deserialize<List<Usuario>>(jsonContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    
                    // Buscar el usuario que coincida EXACTAMENTE con email y contrasena
                    var user = usuarios?.FirstOrDefault(u => 
                        u.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase) &&
                        u.Contrasena == request.Password);
                    
                    if (user != null && !string.IsNullOrEmpty(user.Email) && user.EstaActivo)
                    {
                        // Simulamos un token (en producción vendrá del servidor)
                        var token = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{user.Email}:{DateTime.UtcNow.Ticks}"));
                        SetToken(token);
                        
                        return new LoginResponse 
                        { 
                            Success = true, 
                            Message = "Login exitoso",
                            Token = token,
                            User = user
                        };
                    }
                    else
                    {
                        return new LoginResponse 
                        { 
                            Success = false, 
                            Message = "Credenciales inválidas o usuario inactivo" 
                        };
                    }
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Login Error Response ({response.StatusCode}): {errorContent}");
                    return new LoginResponse 
                    { 
                        Success = false, 
                        Message = $"Credenciales inválidas - {response.StatusCode}" 
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login Exception: {ex.Message}\n{ex.StackTrace}");
                return new LoginResponse 
                { 
                    Success = false, 
                    Message = $"Error de conexión: {ex.Message}" 
                };
            }
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            try
            {
                // Preparar el objeto para enviar según la estructura esperada por la API
                var usuarioData = new
                {
                    email = request.Email,
                    contrasena = request.Password,
                    idRol = 2, // Por defecto usuario normal (ajustar según tus roles)
                    estaActivo = true
                };

                var jsonPayload = JsonSerializer.Serialize(usuarioData);
                Console.WriteLine($"Register Request Payload: {jsonPayload}");

                // POST /api/usuarios para crear nuevo usuario
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/usuarios", content);
                
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Register Response ({response.StatusCode}): {responseContent}");
                
                if (response.IsSuccessStatusCode)
                {
                    var user = JsonSerializer.Deserialize<Usuario>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    
                    if (user != null)
                    {
                        // Simulamos un token
                        var token = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{user.Email}:{DateTime.UtcNow.Ticks}"));
                        SetToken(token);
                        
                        return new RegisterResponse 
                        { 
                            Success = true, 
                            Message = "Registro exitoso",
                            Token = token,
                            User = user
                        };
                    }
                }
                else
                {
                    return new RegisterResponse 
                    { 
                        Success = false, 
                        Message = $"Error en el registro: {response.StatusCode}" 
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Register Exception: {ex.Message}\n{ex.StackTrace}");
                return new RegisterResponse 
                { 
                    Success = false, 
                    Message = $"Error de conexión: {ex.Message}" 
                };
            }
            
            return new RegisterResponse 
            { 
                Success = false, 
                Message = "Error desconocido" 
            };
        }

        public Task LogoutAsync()
        {
            ClearToken();
            return Task.CompletedTask;
        }

        public string? GetToken()
        {
            // Aquí puedes implementar la lógica para obtener el token desde localStorage
            // Por ahora retorna null, necesitarás implementar interop con JavaScript
            return null;
        }

        public void SetToken(string token)
        {
            // Aquí puedes implementar la lógica para guardar el token en localStorage
            // Necesitarás usar interop con JavaScript
        }

        public void ClearToken()
        {
            // Aquí puedes implementar la lógica para limpiar el token de localStorage
        }
    }
}
