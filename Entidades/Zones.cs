namespace CapaDatos.Models
{
    public class Zone
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        // Relación con la tabla `Space`
        public List<Space> Espacios { get; set; }
    }
}
