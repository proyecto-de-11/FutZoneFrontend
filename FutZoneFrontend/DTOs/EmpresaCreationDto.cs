using System.Text.Json.Serialization;

namespace FutZoneFrontend.DTOs
{
    // DTO utilizado para enviar datos al API al crear
    public class EmpresaCreationDto
    {
        // Usar nombres de propiedades en C# más estándar (PascalCase)
        // y usar JsonPropertyName para asegurar que el JSON coincida con la API de Node/Express.

        [JsonPropertyName("usuario_id")] // Node API: snake_case
        public int UsuarioId { get; set; } // C#: PascalCase

        [JsonPropertyName("Nombre")] // Node API: PASCAL CASE (asumiendo esto causa el error)
        public string Nombre { get; set; } = string.Empty;

        [JsonPropertyName("Tipo")]
        public string Tipo { get; set; } = string.Empty;

        [JsonPropertyName("Descripcion")]
        public string Descripcion { get; set; } = string.Empty;

        [JsonPropertyName("Ubicacion")]
        public string Ubicacion { get; set; } = string.Empty;

        [JsonPropertyName("contactos")] // Node API: minúscula (o camelCase si así lo espera)
        public string Contactos { get; set; } = string.Empty;

        [JsonPropertyName("Correo")]
        public string Correo { get; set; } = string.Empty;

        [JsonPropertyName("imagen")]
        public string Imagen { get; set; } = string.Empty;
    }

    // DTO mínimo para capturar la respuesta de éxito (solo necesitamos el ID)
    public class EmpresaResponseDto
    {
        // Asumimos que la respuesta de éxito incluye un 'id'
        public int Id { get; set; }
    }
}