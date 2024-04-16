using MediatR;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Application.Contracts.Persistence.Features.City.Commands.CreateCity;

public class CreateCityCommand : IRequest<ResponseResult<Unit>>
{
    public string CityName { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}