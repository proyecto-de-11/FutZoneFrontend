namespace FutZoneFrontend.Services.Models
{
    using System.Text.Json.Serialization;

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
        [JsonPropertyName("imagen")]
        public string? Imagen { get; set; }
        [JsonPropertyName("empresa_id")]
        public int? EmpresaId { get; set; }
        [JsonPropertyName("tipo_deporte_id")]
        public int? TipoDeporteId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string Superficie { get; set; } = string.Empty;
        [JsonPropertyName("esta_techada")]
        public string? EstaTechada { get; set; }
        [JsonPropertyName("capacidad_jugadores")]
        public int CapacidadJugadores { get; set; }
        [JsonPropertyName("precio_hora")]
        public decimal PrecioHora { get; set; }
        [JsonPropertyName("precio_hora_fin_semana")]
        public decimal PrecioHoraFinSemana { get; set; }
        public string Ubicacion { get; set; } = string.Empty;
        [JsonPropertyName("coordenadas_lat")]
        public decimal? CoordenadasLat { get; set; }
        [JsonPropertyName("coordenadas_lng")]
        public decimal? CoordenadasLng { get; set; }
        [JsonPropertyName("servicios_adicionales")]
        public List<string>? ServiciosAdicionales { get; set; }
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
        [JsonPropertyName("cancha_id")]
        public int CanchaId { get; set; }
        [JsonPropertyName("equipo_id")]
        public int? EquipoId { get; set; }
        [JsonPropertyName("usuario_id")]
        public int UsuarioId { get; set; }
        [JsonPropertyName("usuario_solicitante_id")]
        public int? UsuarioSolicitanteId { get; set; }
        [JsonPropertyName("nombre_usuario")]
        public string? NombreUsuario { get; set; }
        [JsonPropertyName("fecha_inicio")]
        public DateTime FechaInicio { get; set; }
        [JsonPropertyName("fecha_fin")]
        public DateTime FechaFin { get; set; }
        [JsonPropertyName("fecha_reserva")]
        public DateTime? FechaReserva { get; set; }
        [JsonPropertyName("hora_inicio")]
        public string? HoraInicio { get; set; }
        [JsonPropertyName("hora_fin")]
        public string? HoraFin { get; set; }
        [JsonPropertyName("duracion_horas")]
        public decimal DuracionHoras { get; set; }
        [JsonPropertyName("precio_total")]
        public decimal PrecioTotal { get; set; }
        [JsonPropertyName("monto_total")]
        public decimal? MontoTotal { get; set; }
        public string? Estado { get; set; }
        public string? Observaciones { get; set; }
        [JsonPropertyName("mensaje_solicitud")]
        public string? MensajeSolicitud { get; set; }
    }
}
