using MediatR;
using Microsoft.AspNetCore.Mvc;
using Street.Lightning.Application.Contracts.Persistence.Features.City.Queries.GetAllCities;
using Street.Lightning.Application.Contracts.Persistence.Features.Country.Commands.CreateCountry;
using Street.Lightning.Application.Contracts.Persistence.Features.Country.Queries.GetAllCountries;
using Street.Lightning.DTO.Features.Common.City;
using Street.Lightning.DTO.Features.Common.Country;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class CountryController : ControllerBase
{
    private readonly IMediator _mediator;


    public CountryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("countries")]
    public async Task<ResponseResult<IEnumerable<CountryDto>>> GetCountry()
    {
        var countriesQueryResult = await _mediator.Send(new GetAllCountriesQuery());

        if (countriesQueryResult.OperationStatus != ResultEnums.Success)
        {
            return null;
        }

        return countriesQueryResult;
    }

    [HttpPost("addCountry")]
    public async Task<ResponseResult<Unit>> CreateCountry(CountryDto countryDto)
    {
        var request = new CreateCountryCommand
        {
            CountryName = countryDto.CountryName
        };

        var countryCommandResult = await _mediator.Send(request);

        if (countryCommandResult.OperationStatus != ResultEnums.Success)
        {
            return null;
        }

        return countryCommandResult;
    }
}