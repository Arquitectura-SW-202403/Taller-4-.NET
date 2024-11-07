using Microsoft.EntityFrameworkCore;

namespace Datos.Models;

public class SpaceContext : DbContext
{
    public SpaceContext(DbContextOptions<SpaceContext> options) 
    : base(options)
    {

    }

    public DbSet<Space> Spaces {get; set;} = null!;
}