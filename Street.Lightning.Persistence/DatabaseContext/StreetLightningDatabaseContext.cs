using Microsoft.EntityFrameworkCore;
using Street.Lightning.Domain;
using Street.Lightning.Domain.Common;

namespace Street.Lightning.Persistence.DatabaseContext;

public class StreetLightningDatabaseContext : DbContext
{
    public StreetLightningDatabaseContext(DbContextOptions<StreetLightningDatabaseContext> options) : base(options)
    {
        
    }

    public DbSet<Country> Country { get; set; }
    public DbSet<City> City { get; set; }
    public DbSet<Illumination> Illumination { get; set; }
    public DbSet<CityIlluminationDetails> CityIlluminationDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // This is invoked to build all DbSet configurations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StreetLightningDatabaseContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                     .Where(entryDetails => 
                            entryDetails.State is EntityState.Added or 
                                                  EntityState.Modified)
                 )
        {
            entry.Entity.DateModified = DateTime.UtcNow;

            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTime.UtcNow;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}