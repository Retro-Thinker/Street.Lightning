using Street.Lightning.Application.Contracts.Persistence;
using Street.Lightning.Domain;
using Street.Lightning.Persistence.DatabaseContext;

namespace Street.Lightning.Persistence.Repositories;

public class IlluminationRepository : GenericRepository<Illumination>, IIlluminationRepository
{
    public IlluminationRepository(StreetLightningDatabaseContext dbContext) : base(dbContext)
    {
    }
}