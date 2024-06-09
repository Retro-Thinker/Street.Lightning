using AutoMapper;
using MediatR;
using Street.Lightning.DTO.Features.City;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Application.Contracts.Persistence.Features.City.Queries.GetCityYearlyUsage;

public class GetYearlyCityUsageQueryHandler : IRequestHandler<GetYearlyCityUsageQuery, ResponseResult<YearlyPowerUsageDto>>
{
    private readonly IMapper _mapper;
    private readonly ICitySunRepository _citySunRepository;
    private readonly ICityRepository _cityRepository;

    public GetYearlyCityUsageQueryHandler(IMapper mapper, ICitySunRepository sunRepository, 
        ICityRepository cityRepository)
    {
        _mapper = mapper;
        _citySunRepository = sunRepository;
        _cityRepository = cityRepository;
    }
    
    public async Task<ResponseResult<YearlyPowerUsageDto>> Handle(GetYearlyCityUsageQuery request, CancellationToken cancellationToken)
    {
        // https://api.sunrisesunset.io/json?lat=38.907192&lng=-77.036873&date_start=1990-05-01&date_end=1990-07-01
        var cityIlluminationData = await _cityRepository.GetCityWithIlluminationDetails(request.cityId);

        var firstDateOfYear = new DateTime(DateTime.UtcNow.Year, 1, 1);
        var lastDateOfYear = new DateTime(DateTime.UtcNow.Year, 12, 31);

        var cityLightsConsumption = await _citySunRepository
            .FetchCityYearlySunLightDetails(request.cityId, firstDateOfYear, lastDateOfYear);
        
        var powerConsumption = _citySunRepository.CalculateLightsPowerInCity(cityIlluminationData);
        var calculatedPowerIntake = 0d;

        var yearlyPowerUsage = cityLightsConsumption.Data!
            .GroupBy(data => data.TrackingDate.Date.Month)
            .ToDictionary(group => group.Key,
                group => group.Sum(data => data.StreetLightsOnDuration * powerConsumption));
        
        foreach (var illuminationData in cityLightsConsumption.Data!)
        {
            calculatedPowerIntake += illuminationData.StreetLightsOnDuration * powerConsumption;
        }
        
        var queryResult = new ResponseResult<YearlyPowerUsageDto>
        {
            OperationStatus = ResultEnums.Success,
            Message = string.Empty,
            Data = new YearlyPowerUsageDto
            {
                MonthlyPowerUsage = yearlyPowerUsage
            }
        };
        
        return queryResult;
    }
}