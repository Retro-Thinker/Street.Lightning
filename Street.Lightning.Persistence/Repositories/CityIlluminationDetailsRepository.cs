using Street.Lightning.Application.Contracts.Persistence;
using Street.Lightning.Domain;
using Street.Lightning.Persistence.DatabaseContext;

namespace Street.Lightning.Persistence.Repositories;

public class CityIlluminationDetailsRepository : GenericRepository<CityIlluminationDetails>, ICityIlluminationDetailsRepository
{
    public CityIlluminationDetailsRepository(StreetLightningDatabaseContext dbContext) : base(dbContext)
    {
    }
}