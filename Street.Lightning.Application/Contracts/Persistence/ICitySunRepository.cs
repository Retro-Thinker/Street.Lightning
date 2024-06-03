using Street.Lightning.Domain;
using Street.Lightning.DTO.Features.Common.SunriseSunsetAPI;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Application.Contracts.Persistence;

public interface ICitySunRepository
{ 
    Task<ResponseResult<SunriseSunsetResponseDto>> FetchCitySunLightDetails(IEnumerable<CityIlluminationDetails> cityLights);

    Task<ResponseResult<IEnumerable<SunriseSunsetResponseDto>>> FetchCityYearlySunLightDetails(City city);
    double CalculateLightsPowerInCity(IEnumerable<CityIlluminationDetails> cityLights);
}