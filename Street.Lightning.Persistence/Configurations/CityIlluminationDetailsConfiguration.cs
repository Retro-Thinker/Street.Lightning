using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Street.Lightning.Domain;

namespace Street.Lightning.Persistence.Configurations;

public class CityIlluminationDetailsConfiguration : IEntityTypeConfiguration<CityIlluminationDetails>
{
    public void Configure(EntityTypeBuilder<CityIlluminationDetails> builder)
    {
        builder.HasKey(prop => new { prop.CityId, prop.IlluminationId });

        builder.HasOne(prop => prop.City)
            .WithMany(cities => cities.CityIlluminationDetails)
            .HasForeignKey(e => e.CityId);

        builder.HasOne(prop => prop.Illumination)
            .WithMany(illuminations => illuminations.CityIlluminationDetails)
            .HasForeignKey(e => e.IlluminationId);
        
        builder.Property(prop => prop.CityId)
            .IsRequired();

        builder.Property(prop => prop.IlluminationId)
            .IsRequired();

        builder.Property(prop => prop.QuantityOfBulbs)
            .IsRequired();

        var initCityIlluminationDetails = new CityIlluminationDetails
        {
            CityId = 1,
            IlluminationId = 1,
            QuantityOfBulbs = 250,
            DateCreated = DateTime.UtcNow
        };

        builder.HasData(initCityIlluminationDetails);
    }
}