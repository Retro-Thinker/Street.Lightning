using MediatR;
using Street.Lightning.DTO.Features.Common.City;
using Street.Lightning.DTO.Features.Common.Illumination;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Application.Contracts.Persistence.Features.CityIllumination.Commands.CreateCityIllumination;

public class CreateCityIlluminationCommand : IRequest<ResponseResult<Unit>>
{
    public int CityId { get; set; }
    public int IlluminationId { get; set; }
    public int QuantityOfBulbs { get; set; }
}