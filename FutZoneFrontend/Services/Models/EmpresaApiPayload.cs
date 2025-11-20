namespace FutZoneFrontend.Services.Models
{
    using System.Text.Json.Serialization; // << ¡La directiva 'using' que faltaba!

    public class EmpresaApiPayload
    {
        // Mapea a "usuario_id" en minúsculas y con guion bajo
        [JsonPropertyName("usuario_id")]
        public int UsuarioId { get; set; }

        // Mapea a "Nombre"
        [JsonPropertyName("Nombre")]
        public string Nombre { get; set; }

        // Mapea a "Tipo"
        [JsonPropertyName("Tipo")]
        public string Tipo { get; set; }

        // Mapea a "Descripcion"
        [JsonPropertyName("Descripcion")]
        public string Descripcion { get; set; }

        // Mapea a "Ubicacion"
        [JsonPropertyName("Ubicacion")]
        public string Ubicacion { get; set; }

        // Mapea a "contactos"
        [JsonPropertyName("contactos")]
        public string Contactos { get; set; }

        // Mapea a "Correo"
        [JsonPropertyName("Correo")]
        public string Correo { get; set; }

        // Mapea a "imagen"
        [JsonPropertyName("imagen")]
        public string Imagen { get; set; }
    }
}
