using MediatR;
using Microsoft.AspNetCore.Mvc;
using Street.Lightning.Application.Contracts.Persistence.Features.Illumination.Commands.CreateIlluminationProvider;
using Street.Lightning.Application.Contracts.Persistence.Features.Illumination.Queries.GetAllProviders;
using Street.Lightning.DTO.Features.Common.Illumination;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class IlluminationProviderController : ControllerBase
{
    private readonly IMediator _mediator;

    public IlluminationProviderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("getAllIlluminationProviders")]
    public async Task<ResponseResult<IEnumerable<IlluminationDto>>> GetAllIlluminationProvider()
    {
        var response = await _mediator.Send(new GetAllProvidersQuery());

        if (response.OperationStatus != ResultEnums.Success)
        {
            return null;
        }

        return response;
    }

    [HttpPost("addIlluminationProvider")]
    public async Task<ResponseResult<Unit>> CreateIlluminationProvider(IlluminationDto illuminationDto)
    {
        var request = new CreateIlluminationProviderCommand
        {
            Name = illuminationDto.Name,
            Power = illuminationDto.Power,
            IlluminationProvider = illuminationDto.IlluminationProvider
        };

        var illuminationProviderResult = await _mediator.Send(request);

        if (illuminationProviderResult.OperationStatus != ResultEnums.Success)
        {
            return null;
        }

        return illuminationProviderResult;
    }

}