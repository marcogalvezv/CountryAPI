using CountryCityAPI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CountryCityAPI.DataAccess.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>()
            .HasIndex(c => c.Name)
            .IsUnique();

        
        modelBuilder.Entity<Country>()
            .HasMany(c => c.Cities)          
            .WithOne(c => c.Country)          
            .HasForeignKey(c => c.CountryId)  
            .OnDelete(DeleteBehavior.Cascade); 
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=CountryCityDB;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
