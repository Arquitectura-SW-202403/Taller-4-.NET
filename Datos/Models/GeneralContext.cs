using Microsoft.EntityFrameworkCore;
using Entidades;
namespace Datos.Models;

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
    public DbSet<SpaceXStatus> space_x_status {get; set;} = null!;


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
            .HasMany(e => e.SpaceXStatus)
            .WithOne(e => e.Space)
            .HasForeignKey(e => e.space_id)
            .IsRequired(false);
        
        modelBuilder.Entity<OccupancyStatus>()
            .HasMany(e => e.SpaceXStatus)
            .WithOne(e => e.OccupancyStatus)
            .HasForeignKey(e => e.occupancy_status_id)
            .IsRequired(false);
    }
}