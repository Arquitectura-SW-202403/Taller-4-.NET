using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades;
public class Space
{
    public long id { get; set; }
    
    // Propiedades de la tabla `spaces`
    
    public string name { get; set; }

    
    public string description { get; set; }

    
    public float capacity { get; set; }

    // Clave foránea: Relación con la tabla `zones`

   
    public int? zone_id { get; set; }

    public Zone? Zone {get; set;} = null!;

    // Relación con la tabla `SpaceXStatus`
    public ICollection<OccupancyStatus> Occupancies { get;} = new List<OccupancyStatus>();
}
