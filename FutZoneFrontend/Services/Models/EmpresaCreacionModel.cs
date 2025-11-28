namespace FutZoneFrontend.Services.Models
{
    // Este es el modelo que usarás en tu componente Blazor/View
    public class EmpresaModel
    {
        // Las propiedades pueden tener el nombre que desees en C#
        public string NombreEmpresa { get; set; }
        public string TipoEmpresa { get; set; }
        public string DescripcionEmpresa { get; set; }
        public string Ubicacion { get; set; }
        public string Contactos { get; set; }
        public string Correo { get; set; }
        public string ImagenUrl { get; set; }

        // El ID del usuario no viene del formulario, se inyecta desde la sesión
        public int UsuarioId { get; set; }
    }
}
