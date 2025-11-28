namespace FutZoneFrontend.DTOs
{
    public class EmpresaDto
    {
        public int ID { get; set; }
        public int usuario_id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public string Ubicacion { get; set; }
        public string contactos { get; set; }
        public string Correo { get; set; }
        public string imagen { get; set; }
    }
}