using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace Entidades;


[PrimaryKey(nameof(space_id), nameof(occupancy_status_id))]
public class SpaceXStatus
{
    // Claves foráneas
    public long space_id { get; set; }
    public long occupancy_status_id { get; set; }

    public Space? Space {get; set; }
    public OccupancyStatus? OccupancyStatus {get; set;}
}
