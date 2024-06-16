using MediatR;
using Street.Lightning.DTO.Features.Common.Country;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Application.Contracts.Persistence.Features.Country.Commands.CreateCountry;

public class CreateCountryCommand : IRequest<ResponseResult<Unit>>
{
    public int Id { get; set; }
    public string CountryName { get; set; } = string.Empty;
}