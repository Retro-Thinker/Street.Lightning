using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Street.Lightning.Domain;

namespace Street.Lightning.Persistence.Configurations;

public class IlluminationConfiguration : IEntityTypeConfiguration<Illumination>
{
    public void Configure(EntityTypeBuilder<Illumination> builder)
    {
        builder.Property(prop => prop.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(prop => prop.Power)
            .IsRequired()
            .HasMaxLength(100);

        var initIlluminationData = new Illumination
        {
            Id = 1,
            Name = "Eco Bulbs",
            Power = 30d,
            IlluminationProvider = "Tauron",
            DateCreated = DateTime.UtcNow
        };

        builder.HasData(initIlluminationData);
    }
}