using MediatR;
using Street.Lightning.DTO.Features.City;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Application.Contracts.Persistence.Features.City.Queries.GetCityYearlyUsageInDays;

public record GetYearlyUsageInDaysQuery(int cityId) : IRequest<ResponseResult<IEnumerable<DailyPowerUsageDto>>>;