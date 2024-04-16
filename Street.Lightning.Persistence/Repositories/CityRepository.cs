using Microsoft.EntityFrameworkCore;
using Street.Lightning.Application.Contracts.Persistence;
using Street.Lightning.Domain;
using Street.Lightning.Persistence.DatabaseContext;

namespace Street.Lightning.Persistence.Repositories;

public class CityRepository : GenericRepository<City>, ICityRepository
{
    public CityRepository(StreetLightningDatabaseContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> IsCityNameUnique(string cityNameToVerify, CancellationToken cancellationToken)
    {
        return !(await _context.City.AnyAsync(city => city.CityName.Equals(cityNameToVerify)));
    }

    public async Task<City> GetCityWithDetails(int cityId)
    {
        var cityDetails = await  _context.City
            .Include(entity => entity.Country)
            .FirstOrDefaultAsync(city => city.Id == cityId);

        return cityDetails;
    }

    public async Task<IEnumerable<CityIlluminationDetails>> GetCityWithIlluminationDetails(int cityId)
    {
        var cityWithIlluminationDetails = await _context.CityIlluminationDetails
            .Where(entity => entity.CityId == cityId)
            .Include(entity => entity.City)
                .ThenInclude(cityEntity => cityEntity.Country)
            .Include(entity => entity.IlluminationDetails)
            .ToListAsync();

        return cityWithIlluminationDetails;
    }
}