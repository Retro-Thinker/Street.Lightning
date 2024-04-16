using MediatR;
using Street.Lightning.DTO.Features.City;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Application.Contracts.Persistence.Features.City.Queries.GetCityDailyUsage;

public record GetCityDailyPowerUsageQuery(int cityId) : IRequest<ResponseResult<DailyPowerUsageDto>>;