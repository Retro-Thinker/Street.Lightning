using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json.Serialization;
using System.Web;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Street.Lightning.Domain;
using Street.Lightning.DTO.Features.City;
using Street.Lightning.DTO.Features.Common.SunriseSunsetAPI;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Application.Contracts.Persistence.Features.City.Queries.GetCityDailyUsage;

public class GetCityDailyPowerUsageQueryHandler : IRequestHandler<GetCityDailyPowerUsageQuery, ResponseResult<DailyPowerUsageDto>>
{
    private readonly IMapper _mapper;
    private readonly ICityRepository _cityRepository;
    private readonly IConfiguration _configuration;
    private readonly HttpClient _client = new();

    public GetCityDailyPowerUsageQueryHandler(IMapper mapper, ICityRepository cityRepository,
        IConfiguration configuration)
    {
        _mapper = mapper;
        _cityRepository = cityRepository;
        _configuration = configuration;
    }
    
    public async Task<ResponseResult<DailyPowerUsageDto>> Handle(GetCityDailyPowerUsageQuery request, CancellationToken cancellationToken)
    {
        var cityWithIlluminationDetails = await _cityRepository
            .GetCityWithIlluminationDetails(request.cityId);

        var cityLightsConsumption = await FetchCitySunLightDetails(cityWithIlluminationDetails);
        if (cityLightsConsumption.OperationStatus != ResultEnums.Success)
        {
            return new ResponseResult<DailyPowerUsageDto>
            {
                Data = null,
                Message = cityLightsConsumption.Message,
                OperationStatus = cityLightsConsumption.OperationStatus
            };
        }

        var powerConsumption = CalculateLightsPowerInCity(cityWithIlluminationDetails);
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

    private async Task<ResponseResult<SunriseSunsetResponseDto>> FetchCitySunLightDetails(IEnumerable<CityIlluminationDetails> cityLights)
    {
        var cityIllumination = cityLights.FirstOrDefault();

        if (cityIllumination is null) 
            return null;

        // Basic Information about the location
        var latitude = cityIllumination.City.Latitude.ToString();
        var longitude = cityIllumination.City.Longitude.ToString();
        
        var apiUri = ConstructUriAddress(latitude, longitude);
        var sunLightDataResponse = await FetchSunlightData(apiUri);

        return sunLightDataResponse;
    }

    private Uri ConstructUriAddress(string latitude, string longitude)
    {
        var sunsetAPI = String.Concat(_configuration["API"], "/json?");

        var queryParameters = HttpUtility.ParseQueryString(string.Empty);
        queryParameters["lat"] = latitude;
        queryParameters["lng"] = longitude;
        queryParameters["timezone"] = "UTC";
        queryParameters["date"] = DateTime.UtcNow.ToString("yyyy-MM-dd");

        var uri = new UriBuilder(sunsetAPI)
        {
            Query = queryParameters.ToString(),
        }.Uri;

        return uri;
    }

    private async Task<ResponseResult<SunriseSunsetResponseDto>> FetchSunlightData(Uri uri)
    {
        var response = await _client.GetStringAsync(uri);

        if (response == "")
        {
            return new ResponseResult<SunriseSunsetResponseDto>
            {
                Data = null,
                Message = "Api error call",
                OperationStatus = ResultEnums.InternalError
            };
        }

        JObject jsonResponse = JObject.Parse(response);
        var sunriseSunsetDetails = _mapper.Map<SunriseSunsetResponseDto>(jsonResponse["results"]);

        var sunlightQueryResult = new ResponseResult<SunriseSunsetResponseDto>
        {
            Data = sunriseSunsetDetails,
            OperationStatus = ResultEnums.Success,
            Message = string.Empty
        };

        return sunlightQueryResult;
    }

    private double CalculateLightsPowerInCity(IEnumerable<CityIlluminationDetails> cityLights)
    {
        // Calculating the SUM_1-n(Bulb_n * QuantityOfBulbs_n)
        var powerConsumption = 0d;

        foreach (var cityLight in cityLights)
        {
            powerConsumption += cityLight.Illumination.Power * cityLight.QuantityOfBulbs;
        }

        return powerConsumption;
    }
}