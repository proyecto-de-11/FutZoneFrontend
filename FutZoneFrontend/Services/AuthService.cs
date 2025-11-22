using FutZoneFrontend.Services.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Blazored.LocalStorage;

namespace FutZoneFrontend.Services
{
    // ===================================
    // 1. INTERFACE ACTUALIZADA (IAuthService)
    // ===================================
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        Task LogoutAsync();

        string? GetToken();

        // Nueva sobrecarga para almacenar Token, ID y Rol
        void SetToken(string token, int userId, string rol);

        // Método original (compatibilidad)
        void SetToken(string token);

        void ClearToken();

        // Nuevos métodos para acceder a los datos de la sesión
        Task<int?> GetUserIdAsync();
        Task<string?> GetRoleAsync();
        Task EnsureAuthDataLoadedAsync();
    }

    public class AuthService : IAuthService
    {
        private bool _isInitialized = false;
        private readonly HttpClient _httpClient;
        private readonly Blazored.LocalStorage.ILocalStorageService _localStorage;

        private const string TokenKey = "auth_token";
        private const string UserIdKey = "user_id"; // Clave para el ID de Usuario
        private const string RoleKey = "user_role"; // Clave para el Rol

        private string? _token;
        private int? _userId;   // Campo en memoria para el ID
        private string? _role;   // Campo en memoria para el Rol

        public AuthService(HttpClient httpClient, Blazored.LocalStorage.ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;

        }

        // ===================================
        // 2. LÓGICA DE INICIALIZACIÓN
        // ===================================
        private async Task InitializeAuthDataAsync()
        {
            if (_isInitialized) return; 
        
        try
        {
            // La carga de LocalStorage DEBE ocurrir aquí, en un contexto async (como OnInitializedAsync de la vista)
            _token = await _localStorage.GetItemAsync<string>(TokenKey);
            _userId = await _localStorage.GetItemAsync<int?>(UserIdKey);
            _role = await _localStorage.GetItemAsync<string>(RoleKey);
            
            _isInitialized = true;

                Console.WriteLine($"[AuthService - INIT] Estado de autenticación cargado.");
                Console.WriteLine($"[AuthService - INIT] Token: {!string.IsNullOrEmpty(_token)} | User ID: {_userId} | Rol: {_role}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AuthService - INIT] Error al cargar datos: {ex.Message}");
                _token = null;
                _userId = null;
                _role = null;
            }
        }

        // Sobreescribimos el método original (InitializeTokenAsync) para usar el nuevo InitializeAuthDataAsync
        private Task InitializeTokenAsync() => InitializeAuthDataAsync();

        // ===================================
        // 3. LOGIN (Almacenamiento del ID y Rol)
        // ===================================
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            try
            {
                Console.WriteLine($"\n========== INICIAR SESIÓN (dotnet) ==========");
                Console.WriteLine($"Email: {request.Email}");

                var url = "/api/auth/login";
                var loginData = new
                {
                    email = request.Email.Trim().ToLower(),
                    password = request.Password
                };

                var jsonContent = JsonSerializer.Serialize(loginData);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

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

                        // VERIFICACIÓN CLAVE: Asegurarse de que tenemos Token, UserId y Rol.
                        if (!string.IsNullOrEmpty(loginResponse?.Token) && loginResponse.UserId.HasValue && !string.IsNullOrEmpty(loginResponse.Rol))
                        {
                            // *** USAMOS LA NUEVA SOBRECARGA CON ID Y ROL ***
                            SetToken(loginResponse.Token, loginResponse.UserId.Value, loginResponse.Rol);

                            loginResponse.Success = true;
                            loginResponse.Message = "Login exitoso";
                            Console.WriteLine($"✓ LOGIN EXITOSO - Token, ID ({loginResponse.UserId}) y Rol ({loginResponse.Rol}) almacenados.");
                            return loginResponse;
                        }

                        Console.WriteLine($"✗ Respuesta de éxito incompleta (falta token, User ID o Rol).");
                        return new LoginResponse
                        {
                            Success = false,
                            Message = "No se recibieron datos de autenticación completos"
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
                    // Lógica de manejo de errores...
                    try
                    {
                        var errorObj = JsonSerializer.Deserialize<JsonElement>(responseContent);
                        var errorMsg = errorObj.GetProperty("message").GetString() ??
                                               errorObj.GetProperty("error").GetString() ??
                                               responseContent;

                        return new LoginResponse
                        {
                            Success = false,
                            Message = errorMsg ?? $"Error: {response.StatusCode} - Credenciales inválidas"
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
                Console.WriteLine($"✗ EXCEPCIÓN DE CONEXIÓN: {ex.Message}");
                return new LoginResponse
                {
                    Success = false,
                    Message = $"Error de conexión: {ex.Message}"
                };
            }
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            // ... (cuerpo del método RegisterAsync original)
            try
            {
                var usuarioData = new
                {
                    email = request.Email,
                    contrasena = request.Password,
                    idRol = 2,
                    estaActivo = true
                };

                var jsonPayload = JsonSerializer.Serialize(usuarioData);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/auth/registrar", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var user = JsonSerializer.Deserialize<Usuario>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (user != null)
                    {
                        var token = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{user.Email}:{DateTime.UtcNow.Ticks}"));

                        // Usamos la sobrecarga con ID y Rol simulados para el registro
                        SetToken(token, user.Id, user.IdRol == 1 ? "Admin" : "Usuario");

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
            return new RegisterResponse { Success = false, Message = "Error desconocido" };
        }

        public Task LogoutAsync()
        {
            ClearToken();
            Console.WriteLine("[AuthService - LOGOUT] Token, ID de Usuario y Rol eliminados.");
            return Task.CompletedTask;
        }

        // ===================================
        // 4. MÉTODOS DE ACCESO (Getters)
        // ===================================
        public string? GetToken()
        {
            return _token;
        }

        public Task<int?> GetUserIdAsync()
        {
            // Devolvemos el valor almacenado en memoria (_userId)
            return Task.FromResult(_userId);
        }

        public Task<string?> GetRoleAsync()
        {
            // Devolvemos el valor almacenado en memoria (_role)
            return Task.FromResult(_role);
        }
        public Task EnsureAuthDataLoadedAsync()
    {
        // Llamamos a la inicialización para forzar la carga si no se ha hecho
        if (!_isInitialized)
        {
            return InitializeAuthDataAsync();
        }
        return Task.CompletedTask;
    }

        // ===================================
        // 5. MÉTODOS DE ALMACENAMIENTO (Setters/Clear)
        // ===================================
        public void SetToken(string token, int userId, string rol)
        {
            _token = token;
            _userId = userId;
            _role = rol;

            // Almacenar en LocalStorage
            _ = _localStorage.SetItemAsync(TokenKey, token);
            _ = _localStorage.SetItemAsync(UserIdKey, userId);
            _ = _localStorage.SetItemAsync(RoleKey, rol);

            Console.WriteLine($"[AuthService - SET] Datos guardados: UserID={userId}, Rol={rol}");
        }

        // Método original (SetToken sin ID y Rol)
        public void SetToken(string token)
        {
            // Si solo se pasa el token, re-guardamos usando los valores que ya están en memoria (o valores por defecto)
            SetToken(token, _userId ?? 0, _role ?? "Desconocido");
        }

        public void ClearToken()
        {
            _token = null;
            _userId = null;
            _role = null;

            // Eliminar de LocalStorage
            _ = _localStorage.RemoveItemAsync(TokenKey);
            _ = _localStorage.RemoveItemAsync(UserIdKey);
            _ = _localStorage.RemoveItemAsync(RoleKey);

            Console.WriteLine($"[AuthService - CLEAR] LocalStorage limpiado.");
        }
    }
}