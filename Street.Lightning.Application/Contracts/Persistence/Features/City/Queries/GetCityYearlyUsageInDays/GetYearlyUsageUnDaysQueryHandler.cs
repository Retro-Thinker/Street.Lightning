using AutoMapper;
using MediatR;
using Street.Lightning.DTO.Features.City;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Application.Contracts.Persistence.Features.City.Queries.GetCityYearlyUsageInDays;

public class GetYearlyUsageUnDaysQueryHandler : IRequestHandler<GetYearlyUsageInDaysQuery, ResponseResult<IEnumerable<DailyPowerUsageDto>>>
{
    private readonly IMapper _mapper;
    private readonly ICitySunRepository _citySunRepository;
    private readonly ICityRepository _cityRepository;

    public GetYearlyUsageUnDaysQueryHandler(IMapper mapper, ICitySunRepository citySunRepository, ICityRepository cityRepository)
    {
        _mapper = mapper;
        _citySunRepository = citySunRepository;
        _cityRepository = cityRepository;
    }
    
    
    public async Task<ResponseResult<IEnumerable<DailyPowerUsageDto>>> Handle(GetYearlyUsageInDaysQuery request, CancellationToken cancellationToken)
    {
        var cityIlluminationData = await _cityRepository.GetCityWithIlluminationDetails(request.cityId);
        
        var firstDateOfYear = new DateTime(DateTime.UtcNow.Year, 1, 1);
        var lastDateOfYear = new DateTime(DateTime.UtcNow.Year, 12, 31);
        
        var cityLightsConsumptionResponse = await _citySunRepository
            .FetchCityYearlySunLightDetails(request.cityId, firstDateOfYear, lastDateOfYear);
        
        var powerConsumption = _citySunRepository.CalculateLightsPowerInCity(cityIlluminationData);

        var cityLightsConsumption = _mapper.Map<IEnumerable<DailyPowerUsageDto>>(cityLightsConsumptionResponse.Data);
        
        foreach (var dailyPowerTracking in cityLightsConsumption)
        {
            dailyPowerTracking.PowerUsage = dailyPowerTracking.StreetLightsOnDuration * powerConsumption;
        }

        return new ResponseResult<IEnumerable<DailyPowerUsageDto>>
        {
            OperationStatus = ResultEnums.Success,
            Data = cityLightsConsumption
        };
    }
}