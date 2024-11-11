using Microsoft.EntityFrameworkCore;
using Entidades.Zone;
namespace Datos.Models;

public class ZonesContext : DbContext
{
    public static readonly string connectionString = "Server=localhost; User ID=root; Password=31895521Se.; Database=entertainment_spaces";
    public ZonesContext(DbContextOptions<ZonesContext> options) 
    : base(options)
    {

    }

    public DbSet<Zone> zones {get; set;} = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}