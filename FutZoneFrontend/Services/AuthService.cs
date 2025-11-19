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

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            // Creamos el cliente nombrado "AuthClient" que usa la AutenticacionUrl
            _httpClient = httpClientFactory.CreateClient("AuthClient");
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            try
            {
                Console.WriteLine($"\n========== INICIAR SESIÓN ==========");
                Console.WriteLine($"Email: {request.Email}");
                Console.WriteLine($"Contraseña: {new string('*', request.Password.Length)}");
                
                // Consumir endpoint: POST /api/auth/login
                var url = "/api/auth/login";
                
                // Enviar con los nombres exactos que espera la API Java
                var loginData = new 
                { 
                    email = request.Email.Trim().ToLower(), 
                    password = request.Password  // Campo correcto: "password" no "contrasena"
                };
                
                var jsonContent = JsonSerializer.Serialize(loginData);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                
                Console.WriteLine($"Enviando a: {url}");
                Console.WriteLine($"Body: {jsonContent}");
                
                var response = await _httpClient.PostAsync(url, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                Console.WriteLine($"Status: {response.StatusCode}");
                Console.WriteLine($"Response: {responseContent}");
                Console.WriteLine($"====================================\n");
                
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var loginResponse = JsonSerializer.Deserialize<LoginResponse>(responseContent, 
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        
                        if (!string.IsNullOrEmpty(loginResponse?.Token))
                        {
                            SetToken(loginResponse.Token);
                            loginResponse.Success = true;
                            loginResponse.Message = "Login exitoso";
                            Console.WriteLine($"✓ LOGIN EXITOSO - Token almacenado");
                            return loginResponse;
                        }
                        
                        Console.WriteLine($"✗ Respuesta sin token");
                        return new LoginResponse 
                        { 
                            Success = false, 
                            Message = "No se recibió token" 
                        };
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"✗ Error deserializando: {ex.Message}");
                        return new LoginResponse 
                        { 
                            Success = false, 
                            Message = $"Error procesando respuesta: {ex.Message}" 
                        };
                    }
                }
                else
                {
                    // Mostrar error específico
                    try
                    {
                        var errorObj = JsonSerializer.Deserialize<JsonElement>(responseContent);
                        var errorMsg = errorObj.GetProperty("message").GetString() ?? 
                                     errorObj.GetProperty("error").GetString() ?? 
                                     responseContent;
                        
                        Console.WriteLine($"✗ Error API: {errorMsg}");
                        return new LoginResponse 
                        { 
                            Success = false, 
                            Message = errorMsg ?? "Credenciales inválidas" 
                        };
                    }
                    catch
                    {
                        return new LoginResponse 
                        { 
                            Success = false, 
                            Message = $"Error: {response.StatusCode} - Verifica tu email y contraseña" 
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ EXCEPCIÓN: {ex.Message}");
                Console.WriteLine($"Stack: {ex.StackTrace}");
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
                    idRol = 3, // Por defecto usuario normal (ajustar según tus roles)
                    estaActivo = true
                };

                var jsonPayload = JsonSerializer.Serialize(usuarioData);
                Console.WriteLine($"Register Request Payload: {jsonPayload}");

                // POST /api/auth/registrar para crear nuevo usuario
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/auth/registrar", content);
                
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
