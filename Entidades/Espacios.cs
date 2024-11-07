namespace Entidades;
{
    public class Space
    {
        public long Id { get; set; }
        
        // Propiedades de la tabla `spaces`
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public float Capacidad { get; set; }

        // Clave foránea: Relación con la tabla `zones`
        public int ZoneId { get; set; }
        public Zone Zone { get; set; } // Relación con `Zone`

        // Relación con la tabla `SpaceXStatus`
        public List<SpaceXStatus> SpaceXStatus { get; set; }
    }
}
