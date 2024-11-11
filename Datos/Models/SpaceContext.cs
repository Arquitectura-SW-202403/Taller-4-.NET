using Microsoft.EntityFrameworkCore;

namespace Datos.Models;

public class SpaceContext : DbContext
{
    public static readonly string connectionString = "Server=localhost; User ID=root; Password=31895521Se.; Database=entertainment_spaces";
    public SpaceContext(DbContextOptions<SpaceContext> options) 
    : base(options)
    {

    }

    public DbSet<Space> Spaces {get; set;} = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}