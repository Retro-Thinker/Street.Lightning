using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Street.Lightning.Application.Contracts.Persistence;
using Street.Lightning.Application.Contracts.Persistence.Common;
using Street.Lightning.Persistence.DatabaseContext;
using Street.Lightning.Persistence.Repositories;

namespace Street.Lightning.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<StreetLightningDatabaseContext>(options =>
        {
            options.UseSqlServer(configuration.GetValue<string>("Azure-Sql-Connection-String"));
        });

        var serviceProvider = services.BuildServiceProvider();
        var dbContext = serviceProvider.GetRequiredService<StreetLightningDatabaseContext>();
        dbContext.Database.Migrate();
        
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<IIlluminationRepository, IlluminationRepository>();
        services.AddScoped<ICityIlluminationDetailsRepository, CityIlluminationDetailsRepository>();
        
        return services;
    }
}