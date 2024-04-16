using MediatR;
using Street.Lightning.DTO.Features.Common.City;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Application.Contracts.Persistence.Features.City.Queries.GetCity;

public record GetCityByIdQuery(int Id) : IRequest<ResponseResult<CityDto>>;