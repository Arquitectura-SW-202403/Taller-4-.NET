using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades;

public class Zone
{
    public int id { get; set; }

    public string name { get; set; }

    // Relación con la tabla `Space`
    public ICollection<Space> Espacios { get; } = new List<Space>();
}
