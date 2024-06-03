using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Street.Lightning.DTO.Features.City;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Application.Contracts.Persistence.Features.City.Queries.GetCityDailyUsage;

public class GetCityDailyPowerUsageQueryHandler : IRequestHandler<GetCityDailyPowerUsageQuery, ResponseResult<DailyPowerUsageDto>>
{
    private readonly ICityRepository _cityRepository;
    private readonly ICitySunRepository _citySunRepository;

    public GetCityDailyPowerUsageQueryHandler(IMapper mapper, ICityRepository cityRepository,
        ICitySunRepository citySunRepository, IConfiguration configuration)
    {
        _cityRepository = cityRepository;
        _citySunRepository = citySunRepository;
    }

    public async Task<ResponseResult<DailyPowerUsageDto>> Handle(GetCityDailyPowerUsageQuery request, CancellationToken cancellationToken)
    {
        var cityWithIlluminationDetails = await _cityRepository
            .GetCityWithIlluminationDetails(request.cityId);

        var cityLightsConsumption = await _citySunRepository.FetchCitySunLightDetails(cityWithIlluminationDetails);
        if (cityLightsConsumption.OperationStatus != ResultEnums.Success)
        {
            return new ResponseResult<DailyPowerUsageDto>
            {
                Data = null,
                Message = cityLightsConsumption.Message,
                OperationStatus = cityLightsConsumption.OperationStatus
            };
        }

        var powerConsumption = _citySunRepository.CalculateLightsPowerInCity(cityWithIlluminationDetails);
        var calculatedDailyPower = powerConsumption * cityLightsConsumption.Data!.StreetLightsOnDuration;

        var queryResult = new ResponseResult<DailyPowerUsageDto>
        {
            OperationStatus = ResultEnums.Success,
            Message = string.Empty,
            Data = new DailyPowerUsageDto
            {
                PowerUsage = calculatedDailyPower
            }
        };
        
        return queryResult;
    }
}