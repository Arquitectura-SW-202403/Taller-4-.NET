
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entidades;

public class OccupancyStatus
{
    
    public long Id { get; set; }

    // Propiedades de la tabla `occupancy_status`
    
    public string status { get; set; }
    
    public int start_time { get; set; }  // Hora de inicio
    
    public int end_time { get; set; }    // Hora de fin
    
    public string owner { get; set; }

    // Relación con la tabla `SpaceXStatus`
    public ICollection<SpaceXStatus> SpaceXStatus { get; } = new List<SpaceXStatus>();
}
