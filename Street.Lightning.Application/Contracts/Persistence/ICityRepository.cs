using Street.Lightning.Application.Contracts.Persistence.Common;
using Street.Lightning.Domain;

namespace Street.Lightning.Application.Contracts.Persistence;

public interface ICityRepository : IGenericRepository<City>
{
    public Task<bool> IsCityNameUnique(string cityNameToVerify, CancellationToken cancellationToken);
    public Task<City> GetCityWithDetails(int cityId);
    public Task<IEnumerable<CityIlluminationDetails>> GetCityWithIlluminationDetails(int cityId);
}