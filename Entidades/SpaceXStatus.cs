namespace CapaDatos.Models
{
    public class SpaceXStatus
    {
        public long Id { get; set; }

        // Claves foráneas
        public long SpaceId { get; set; }
        public Space Space { get; set; }  // Relación con `Space`

        public long OccupancyStatusId { get; set; }
        public OccupancyStatus OccupancyStatus { get; set; }  // Relación con `OccupancyStatus`
    }
}
