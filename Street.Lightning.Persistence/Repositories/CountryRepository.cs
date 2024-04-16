using Street.Lightning.Application.Contracts.Persistence;
using Street.Lightning.Domain;
using Street.Lightning.Persistence.DatabaseContext;

namespace Street.Lightning.Persistence.Repositories;

public class CountryRepository : GenericRepository<Country>, ICountryRepository
{
    public CountryRepository(StreetLightningDatabaseContext dbContext) : base(dbContext)
    {
    }
}