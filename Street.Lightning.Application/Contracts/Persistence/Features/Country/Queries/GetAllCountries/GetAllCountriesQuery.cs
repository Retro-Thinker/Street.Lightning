using MediatR;
using Street.Lightning.DTO.Features.Common.Country;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Application.Contracts.Persistence.Features.Country.Queries.GetAllCountries;

public record GetAllCountriesQuery : IRequest<ResponseResult<IEnumerable<CountryDto>>>;