namespace FutZoneFrontend.Services.Models
{
    public class TipoDeporteResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<TipoDeporte> Data { get; set; } = new();
    }

    public class Membresia
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int DuracionDias { get; set; }
        public string? Beneficios { get; set; }
    }

    public class MembresiaResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<Membresia> Data { get; set; } = new();
    }

    public class UsuarioMembresia
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int MembresiaId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Activa { get; set; }
    }

    public class UsuarioMembresiaResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<UsuarioMembresia> Data { get; set; } = new();
    }
}
