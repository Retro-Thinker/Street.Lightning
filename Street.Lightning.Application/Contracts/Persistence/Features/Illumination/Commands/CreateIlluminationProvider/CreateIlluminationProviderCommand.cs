using MediatR;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Application.Contracts.Persistence.Features.Illumination.Commands.CreateIlluminationProvider;

public class CreateIlluminationProviderCommand : IRequest<ResponseResult<Unit>>
{
    public string Name { get; set; }
    public string IlluminationProvider { get; set; }
    public double Power { get; set; }
}