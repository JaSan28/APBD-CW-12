using Microsoft.EntityFrameworkCore;

namespace zadanie12.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<ClientTrip> ClientTrips { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<CountryTrip> CountryTrips { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClientTrip>().HasKey(ct => new { ct.IdClient, ct.IdTrip });
        modelBuilder.Entity<CountryTrip>().HasKey(ct => new { ct.IdCountry, ct.IdTrip });
    }
}