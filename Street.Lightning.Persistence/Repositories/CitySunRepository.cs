using System.Web;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Street.Lightning.Application.Contracts.Persistence;
using Street.Lightning.Domain;
using Street.Lightning.DTO.Features.Common.SunriseSunsetAPI;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Persistence.Repositories;

public class CitySunRepository : ICitySunRepository
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly ICityRepository _cityRepository;
    private readonly HttpClient _client = new();

    public CitySunRepository(IMapper mapper, IConfiguration configuration, ICityRepository cityRepository)
    {
        _mapper = mapper;
        _configuration = configuration;
        _cityRepository = cityRepository;
    }
    
    public async Task<ResponseResult<SunriseSunsetResponseDto>> FetchCitySunLightDetails(IEnumerable<CityIlluminationDetails> cityLights)
    {
        var cityIllumination = cityLights.FirstOrDefault();

        if (cityIllumination is null) 
            return null;

        // Basic Information about the location
        var latitude = cityIllumination.City.Latitude.ToString();
        var longitude = cityIllumination.City.Longitude.ToString();
        
        var apiUri = ConstructUriAddress(latitude, longitude);
        var sunLightDataResponse = await FetchSunlightData<SunriseSunsetResponseDto>(apiUri);

        return sunLightDataResponse;
    }

    public async Task<ResponseResult<IEnumerable<SunriseSunsetResponseDto>>> FetchCityYearlySunLightDetails(int cityId, DateTime? startingDate = null, DateTime? endingDate = null)
    {
        var city = await _cityRepository.GetCityWithDetails(cityId);
        var uri = ConstructUriAddress(city.Latitude.ToString(), city.Longitude.ToString(),
            new DateTime(DateTime.UtcNow.Year, 1, 1), new DateTime(DateTime.UtcNow.Year, 12, 21));

        var sunLightDataResponse = await FetchSunlightData<IEnumerable<SunriseSunsetResponseDto>>(uri, true);

        return sunLightDataResponse;
    }

    private Uri ConstructUriAddress(string latitude, string longitude, DateTime? startingDate = null, DateTime? endingDate = null)
    {
        var sunsetAPI = String.Concat(_configuration["API"], "/json?");

        var queryParameters = HttpUtility.ParseQueryString(string.Empty);
        queryParameters["lat"] = latitude;
        queryParameters["lng"] = longitude;
        queryParameters["timezone"] = "UTC";

        if (startingDate.HasValue && endingDate.HasValue)
        {
            queryParameters["date_start"] = startingDate.Value.ToString("yyyy-MM-dd");
            queryParameters["date_end"] = endingDate.Value.ToString("yyyy-MM-dd");
        }
        else
        {
            queryParameters["date"] = DateTime.UtcNow.ToString("yyyy-MM-dd");
        }
        

        var uri = new UriBuilder(sunsetAPI)
        {
            Query = queryParameters.ToString(),
        }.Uri;

        return uri;
    }
    
    private async Task<ResponseResult<T>> FetchSunlightData<T>(Uri uri, bool isComplex = false)
    {
        var response = await _client.GetStringAsync(uri);

        if (response == "")
        {
            return new ResponseResult<T>
            {
                Data = default,
                Message = "Api error call",
                OperationStatus = ResultEnums.InternalError
            };
        }

        JObject jsonResponse = JObject.Parse(response);
        var sunriseSunsetDetails = ParseSunsetApiResponse<T>(jsonResponse["results"], isComplex);

        return new ResponseResult<T>
        {
            Data = sunriseSunsetDetails,
            OperationStatus = ResultEnums.Success
        };
    }

    private T ParseSunsetApiResponse<T>(JToken jResponse, bool isComplex = false)
    {
        if (isComplex)
        {
            return _mapper.Map<T>(jResponse.Children<JObject>().ToList());
        }
        return _mapper.Map<T>(jResponse);
    }

    public double CalculateLightsPowerInCity(IEnumerable<CityIlluminationDetails> cityLights)
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