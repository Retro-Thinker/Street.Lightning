using AutoMapper;
using MediatR;
using Street.Lightning.DTO.Features.City;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Application.Contracts.Persistence.Features.City.Queries.GetCityYearlyUsage;

public class GetYearlyCityUsageQueryHandler : IRequestHandler<GetYearlyCityUsageQuery, ResponseResult<YearlyPowerUsageDto>>
{
    private readonly IMapper _mapper;
    private readonly ICitySunRepository _sunRepository;
    private readonly ICityRepository _cityRepository;

    public GetYearlyCityUsageQueryHandler(IMapper mapper, ICitySunRepository sunRepository, 
        ICityRepository cityRepository)
    {
        _mapper = mapper;
        _sunRepository = sunRepository;
        _cityRepository = cityRepository;
    }
    
    public async Task<ResponseResult<YearlyPowerUsageDto>> Handle(GetYearlyCityUsageQuery request, CancellationToken cancellationToken)
    {
        // https://api.sunrisesunset.io/json?lat=38.907192&lng=-77.036873&date_start=1990-05-01&date_end=1990-07-01
        var cityIlluminationData = await _cityRepository.GetCityWithDetails(request.cityId);

        var firstDateOfYear = new DateTime(DateTime.UtcNow.Year, 1, 1);
        var lastDateOfYear = new DateTime(DateTime.UtcNow.Year, 12, 31);
        
        
        return null;
    }
}