using MediatR;
using Street.Lightning.DTO.Features.Common.CityIllumination;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Application.Contracts.Persistence.Features.CityIllumination.Queries.GetAllCityIlluminations;

public record GetAllCityIlluminationsQuery : IRequest<ResponseResult<IEnumerable<CityIlluminationDto>>>;