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

    public class Cancha
    {
        public int Id { get; set; }
        public int? EmpresaId { get; set; }
        public int? TipoDeporteId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string Superficie { get; set; } = string.Empty;
        public bool EstaTechada { get; set; }
        public int CapacidadJugadores { get; set; }
        public decimal PrecioHora { get; set; }
        public decimal PrecioHoraFinSemana { get; set; }
        public string Ubicacion { get; set; } = string.Empty;
        public decimal? CoordenadasLat { get; set; }
        public decimal? CoordenadasLng { get; set; }
        public List<string>? ServiciosAdicionales { get; set; }
        public string? Imagen { get; set; }
        public string? HoraApertura { get; set; }
        public string? HoraCierre { get; set; }
        public bool Activa { get; set; }
        public string? Dimensiones { get; set; }
        public string? TipoSuperficie { get; set; }
        public string? Ciudad { get; set; }
        public string? Pais { get; set; }
        public string? TelefonoContacto { get; set; }
        public string? EmailContacto { get; set; }
        public List<string>? Caracteristicas { get; set; }
    }

    public class Reserva
    {
        public int Id { get; set; }
        public int CanchaId { get; set; }
        public int? EquipoId { get; set; }
        public int UsuarioId { get; set; }
        public int? UsuarioSolicitanteId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime? FechaReserva { get; set; }
        public string? HoraInicio { get; set; }
        public string? HoraFin { get; set; }
        public decimal DuracionHoras { get; set; }
        public decimal PrecioTotal { get; set; }
        public decimal? MontoTotal { get; set; }
        public string? Estado { get; set; }
        public string? Observaciones { get; set; }
        public string? MensajeSolicitud { get; set; }
    }
}
