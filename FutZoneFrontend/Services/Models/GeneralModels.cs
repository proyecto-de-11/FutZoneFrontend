namespace FutZoneFrontend.Services.Models
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
    }

    public class TipoDeporte
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
    }

    public class PaginatedResponse<T>
    {
        public List<T> Content { get; set; } = new();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public long TotalElements { get; set; }
        public int TotalPages { get; set; }
        public bool Last { get; set; }
    }

    public class DocumentoLegal
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Contenido { get; set; } = string.Empty;
        public string? Tipo { get; set; }
    }

    public class Preferencia
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Clave { get; set; } = string.Empty;
        public string Valor { get; set; } = string.Empty;
    }

    public class Aceptacion
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int DocumentoLegalId { get; set; }
        public string FechaAceptacion { get; set; } = string.Empty;
    }
}
