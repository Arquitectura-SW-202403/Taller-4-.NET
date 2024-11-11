using Microsoft.EntityFrameworkCore;
using Entidades;
namespace Datos.Context;

public class GeneralContext : DbContext
{
    public static readonly string connectionString = "Server=localhost; User ID=taller4; Password=taller4; Database=entertainment_spaces";
    public GeneralContext(DbContextOptions<GeneralContext> options) 
    : base(options)
    {

    }

    public DbSet<Space> spaces {get; set;} = null!;
    public DbSet<Zone> zones {get; set;} = null!;
    public DbSet<OccupancyStatus> occupancy_status {get; set;} = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Zone>()
            .HasMany(e => e.Espacios)
            .WithOne(e => e.Zone)
            .HasForeignKey(e => e.zone_id)
            .IsRequired();

        modelBuilder.Entity<Space>()
            .HasOne(e => e.Zone)
            .WithMany(e => e.Espacios)
            .HasForeignKey(e => e.zone_id)
            .IsRequired();

        modelBuilder.Entity<Space>()
            .HasMany(e => e.Occupancies)
            .WithOne(e => e.Space)
            .HasForeignKey(e => e.space_id)
            .IsRequired();

        modelBuilder.Entity<OccupancyStatus>()
            .HasOne(e => e.Space)
            .WithMany(e => e.Occupancies)
            .HasForeignKey(e => e.space_id)
            .IsRequired();
    }
}