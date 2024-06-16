using AutoMapper;
using Street.Lightning.Application.Contracts.Persistence.Features.City.Commands.CreateCity;
using Street.Lightning.Domain;
using Street.Lightning.DTO.Features.Common.City;
using Street.Lightning.DTO.Features.Common.Country;

namespace Street.Lightning.Application.MappingProfiles;

public class CountryProfile : Profile
{
    public CountryProfile()
    {
        CreateMap<Country, CountryDto>().ReverseMap();
        CreateMap<CreateCityCommand, Country>();
    }
}