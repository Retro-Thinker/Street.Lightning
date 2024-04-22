using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Street.Lightning.Domain;

namespace Street.Lightning.Persistence.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.Property(prop => prop.CountryName)
            .IsRequired()
            .HasMaxLength(50);

        var initCountryData = new Country
        {
            Id = 1,
            CountryName = "Poland",
            DateCreated = DateTime.UtcNow
        };

        builder.HasData(initCountryData);
    }
}