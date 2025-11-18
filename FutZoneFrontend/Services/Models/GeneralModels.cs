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
}
