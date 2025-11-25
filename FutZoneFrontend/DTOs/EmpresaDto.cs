namespace FutZoneFrontend.DTOs
{
    public class EmpresaDto
    {
        public int ID { get; set; }
        public int Usuario_id { get; set; } // Coincide con el campo 'usuario_id' del JSON
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public string Ubicacion { get; set; }
        public string Contactos { get; set; }
        public string Correo { get; set; }
        public string Imagen { get; set; }
    }
    
    
}