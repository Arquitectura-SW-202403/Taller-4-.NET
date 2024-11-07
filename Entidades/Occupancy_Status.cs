namespace CapaDatos.Models
{
    public class OccupancyStatus
    {
        public long Id { get; set; }

        // Propiedades de la tabla `occupancy_status`
        public string Estado { get; set; }
        public int StartTime { get; set; }  // Hora de inicio
        public int EndTime { get; set; }    // Hora de fin
        public string Dueño { get; set; }

        // Relación con la tabla `SpaceXStatus`
        public List<SpaceXStatus> SpaceXStatus { get; set; }
    }
}
