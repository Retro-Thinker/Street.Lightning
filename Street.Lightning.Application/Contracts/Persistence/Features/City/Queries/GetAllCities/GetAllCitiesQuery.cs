using MediatR;
using Street.Lightning.DTO.Features.Common.City;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Application.Contracts.Persistence.Features.City.Queries.GetAllCities;

public record GetAllCitiesQuery : IRequest<ResponseResult<IEnumerable<CityDto>>>;