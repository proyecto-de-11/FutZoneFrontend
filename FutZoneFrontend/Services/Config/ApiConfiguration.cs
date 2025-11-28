using Microsoft.Extensions.Configuration;

namespace FutZoneFrontend.Services.Config
{
    public interface IApiConfiguration
    {
        string GetCanchasApiUrl();
        string GetReservasApiUrl();
        string GetHorariosApiUrl();
        string GetEmpresaApiUrl();
        string GetPublicacionesApiUrl();
        string GetComentariosApiUrl();
    }

    public class ApiConfiguration : IApiConfiguration
    {
        private readonly IConfiguration _configuration;

        public ApiConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetCanchasApiUrl()
        {
            var baseUrl = _configuration["ApiEndpoints:CanchasYReservas:BaseUrl"] ?? "https://apicanchasyreservas.onrender.com";
            return baseUrl;
        }

        public string GetReservasApiUrl()
        {
            var baseUrl = _configuration["ApiEndpoints:CanchasYReservas:BaseUrl"] ?? "https://apicanchasyreservas.onrender.com";
            return baseUrl;
        }

        public string GetHorariosApiUrl()
        {
            var baseUrl = _configuration["ApiEndpoints:CanchasYReservas:BaseUrl"] ?? "https://apicanchasyreservas.onrender.com";
            return baseUrl;
        }

        public string GetEmpresaApiUrl()
        {
            var baseUrl = _configuration["ApiEndpoints:EmpresaPublicidad:BaseUrl"] ?? "https://api-empresa-publicidad.onrender.com";
            return baseUrl;
        }

        public string GetPublicacionesApiUrl()
        {
            var baseUrl = _configuration["ApiEndpoints:EmpresaPublicidad:BaseUrl"] ?? "https://api-empresa-publicidad.onrender.com";
            return baseUrl;
        }

        public string GetComentariosApiUrl()
        {
            var baseUrl = _configuration["ApiEndpoints:EmpresaPublicidad:BaseUrl"] ?? "https://api-empresa-publicidad.onrender.com";
            return baseUrl;
        }
    }
}
