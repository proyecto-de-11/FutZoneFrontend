using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FutZoneFrontend.Services.Models;
using System;

namespace FutZoneFrontend.Services
{
    public class EmpresaService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService; // 1. Inyectamos IAuthService
        private readonly string _apiBaseUrl;

        // Inyectamos HttpClient, IConfiguration y el IAuthService
        public EmpresaService(HttpClient httpClient, IConfiguration configuration, IAuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService; // Almacenamos la referencia
            _apiBaseUrl = configuration["ApiBaseUrl"] ?? "http://localhost:8080"; // Usar un default si no se encuentra
        }

        public async Task<bool> CrearEmpresaAsync(EmpresaModel model)
        {
            // 1. Obtener el ID de Usuario de la sesión de autenticación
            var userId = await _authService.GetUserIdAsync();
            var token = _authService.GetToken();

            if (!userId.HasValue || string.IsNullOrEmpty(token))
            {
                Console.WriteLine("⚠️ Acceso denegado: Usuario no autenticado o ID de usuario/Token no disponible.");
                return false;
            }

            // 2. Mapear y construir el Payload para la API
            var payload = new EmpresaApiPayload // Asumo que este modelo existe en FutZoneFrontend.Services.Models
            {
                UsuarioId = userId.Value, // Usamos el ID de usuario de la sesión
                Nombre = model.NombreEmpresa,
                Tipo = model.TipoEmpresa,
                Descripcion = model.DescripcionEmpresa,
                Ubicacion = model.Ubicacion,
                Contactos = model.Contactos,
                Correo = model.Correo,
                Imagen = model.ImagenUrl
            };

            // 3. Establecer el Token JWT en el encabezado para la autenticación
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // 4. Definir la URL completa de la API
            var apiUrl = $"{_apiBaseUrl}/empresa";

            Console.WriteLine($"\n========== CREAR EMPRESA (dotnet) ==========");
            Console.WriteLine($"Usuario ID a enviar: {userId.Value}");
            Console.WriteLine($"Endpoint: POST {apiUrl}");
            Console.WriteLine($"Token usado: Bearer {token.Substring(0, 20)}..."); // Muestra solo una parte del token


            try
            {
                // 5. Realizar la llamada HTTP POST
                var response = await _httpClient.PostAsJsonAsync(apiUrl, payload);

                // 6. Verificar el código de respuesta
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("✅ Empresa creada exitosamente.");
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"❌ Error al crear empresa. Status: {response.StatusCode}. Contenido: {errorContent}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Excepción al llamar a la API: {ex.Message}");
                return false;
            }
            finally
            {
                // Es buena práctica limpiar el encabezado si no se necesita para llamadas posteriores
                // Aunque en Blazor/HttpClient esto a menudo se maneja mejor en un DelegatingHandler global.
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }
    }
}