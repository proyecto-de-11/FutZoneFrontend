namespace FutZoneFrontend.Services.Models
{
    public class Perfil
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Ciudad { get; set; }
        public string? Foto { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }

    public class PerfilRequest
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Ciudad { get; set; }
        public string? Foto { get; set; }
        public string? Descripcion { get; set; }
    }

    public class PerfilResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public Perfil? Data { get; set; }
    }
}
