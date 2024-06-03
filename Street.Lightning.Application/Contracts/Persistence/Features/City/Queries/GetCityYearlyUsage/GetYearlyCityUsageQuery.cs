using MediatR;
using Street.Lightning.DTO.Features.City;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Application.Contracts.Persistence.Features.City.Queries.GetCityYearlyUsage;

public record GetYearlyCityUsageQuery(int cityId) : IRequest<ResponseResult<YearlyPowerUsageDto>>;