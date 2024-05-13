using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Street.Lightning.Application.Contracts.Persistence.Features.City.Queries.GetAllCities;
using Street.Lightning.Application.Contracts.Persistence.Features.City.Queries.GetCity;
using Street.Lightning.Application.Contracts.Persistence.Features.City.Queries.GetCityDailyUsage;
using Street.Lightning.DTO.Features.City;
using Street.Lightning.DTO.Features.Common.City;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class CityController : ControllerBase
{
    private readonly IMediator _mediator;

    public CityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("cities")]
    public async Task<ResponseResult<IEnumerable<CityDto>>> GetCities()
    {
        var citiesQueryResult = await _mediator.Send(new GetAllCitiesQuery());

        if (citiesQueryResult.OperationStatus != ResultEnums.Success)
        {
            return null;
        }
        
        return citiesQueryResult;
    }

    [HttpGet("getCityById/{cityId}")]
    public async Task<ResponseResult<CityDto>> GetCity(int cityId)
    {
        var cityQueryResult = await _mediator.Send(new GetCityByIdQuery(cityId));

        if (cityQueryResult.OperationStatus != ResultEnums.Success)
        {
            return null;
        }

        return cityQueryResult;
    }

    [HttpGet("getCityDailyPowerUsage/{cityId}")]
    public async Task<ResponseResult<DailyPowerUsageDto>> GetCityDailyPowerUsage(int cityId)
    {
        var powerUsageQueryResult = await _mediator.Send(new GetCityDailyPowerUsageQuery(cityId));

        if (powerUsageQueryResult.OperationStatus != ResultEnums.Success)
        {
            return null;
        }

        return powerUsageQueryResult;
    }

}