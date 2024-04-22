using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Street.Lightning.Domain;

namespace Street.Lightning.Persistence.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.Property(prop => prop.CityName)
            .IsRequired()
            .HasMaxLength(50);

        var initCityData = new City
        {
            Id = 1,
            CountryId = 1,
            CityName = "Wrocław",
            Latitude = 51.107883,
            Longitude = 17.038538,
            DateCreated = DateTime.UtcNow
        };

        builder.HasData(initCityData);
    }
}