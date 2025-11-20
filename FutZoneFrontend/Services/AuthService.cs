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
        private readonly ILocalStorageService _localStorage;
        private const string TokenKey = "auth_token";
        private string? _token;

        public AuthService(IHttpClientFactory httpClientFactory, ILocalStorageService localStorage)
        {
            _httpClient = httpClientFactory.CreateClient("Auth");
            _localStorage = localStorage;
            _ = InitializeTokenAsync();
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            try
            {
                var loginData = new { email = request.Email.Trim().ToLower(), password = request.Password };
                var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginData);

                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.Token))
                    {
                        SetToken(loginResponse.Token);
                        loginResponse.Success = true;
                        loginResponse.Message = "Login exitoso";
                        return loginResponse;
                    }
                    return new LoginResponse { Success = false, Message = "No se recibió token" };
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new LoginResponse { Success = false, Message = errorContent ?? "Credenciales inválidas" };
                }
            }
            catch (Exception ex)
            {
                return new LoginResponse { Success = false, Message = $"Error de conexión: {ex.Message}" };
            }
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            try
            {
                var usuarioData = new
                {
                    email = request.Email,
                    contrasena = request.Password,
                    idRol = 2, 
                    estaActivo = true
                };

                var response = await _httpClient.PostAsJsonAsync("api/auth/registrar", usuarioData);

                if (response.IsSuccessStatusCode)
                {
                    var user = await response.Content.ReadFromJsonAsync<Usuario>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (user != null)
                    {
                        var token = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{user.Email}:{DateTime.UtcNow.Ticks}"));
                        SetToken(token);
                        return new RegisterResponse { Success = true, Message = "Registro exitoso", Token = token, User = user };
                    }
                }
                
                var errorContent = await response.Content.ReadAsStringAsync();
                return new RegisterResponse { Success = false, Message = $"Error en el registro: {errorContent}" };
            }
            catch (Exception ex)
            {
                return new RegisterResponse { Success = false, Message = $"Error de conexión: {ex.Message}" };
            }
        }

        public Task LogoutAsync()
        {
            ClearToken();
            return Task.CompletedTask;
        }

        private async Task InitializeTokenAsync()
        {
            try
            {
                _token = await _localStorage.GetItemAsync(TokenKey);
            }
            catch
            {
                _token = null;
            }
        }

        public string? GetToken()
        {
            return _token;
        }

        public void SetToken(string token)
        {
            _token = token;
            _ = _localStorage.SetItemAsync(TokenKey, token);
        }

        public void ClearToken()
        {
            _token = null;
            _ = _localStorage.RemoveItemAsync(TokenKey, CancellationToken.None);
        }
    }
}
