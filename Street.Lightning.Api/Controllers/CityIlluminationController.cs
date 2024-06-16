using MediatR;
using Microsoft.AspNetCore.Mvc;
using Street.Lightning.Application.Contracts.Persistence.Features.CityIllumination.Commands.CreateCityIllumination;
using Street.Lightning.Application.Contracts.Persistence.Features.CityIllumination.Queries.GetAllCityIlluminations;
using Street.Lightning.DTO.Features.Common.CityIllumination;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class CityIlluminationController : ControllerBase
{
    private readonly IMediator _mediator;

    public CityIlluminationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("getAllCityIllumination")]
    public async Task<ResponseResult<IEnumerable<CityIlluminationDto>>> GetAllCityIlluminations()
    {
        var response = await _mediator.Send(new GetAllCityIlluminationsQuery());

        if (response.OperationStatus != ResultEnums.Success)
        {
            return null;
        }

        return response;
    }

    [HttpPost("addCityIllumination")]
    public async Task<ResponseResult<Unit>> CreateCityIllumination(CityIlluminationDto cityIlluminationDto)
    {
        var request = new CreateCityIlluminationCommand
        {
            CityId = cityIlluminationDto.CityId,
            IlluminationId = cityIlluminationDto.IlluminationId,
            QuantityOfBulbs = cityIlluminationDto.QuantityOfBulbs
        };

        var cityIlluminationCommand = await _mediator.Send(request);

        if (cityIlluminationCommand.OperationStatus != ResultEnums.Success)
        {
            return null;
        }

        return cityIlluminationCommand;
    }
}