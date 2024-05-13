using AutoMapper;
using Street.Lightning.Domain;
using Street.Lightning.DTO.Features.Common.City;

namespace Street.Lightning.Application.MappingProfiles;

public class CityProfile : Profile
{
    public CityProfile()
    {
        CreateMap<City, CityDto>()
            .ForMember(dest => dest.CountryName, opt => 
                opt.MapFrom(src => src.Country.CountryName))
            .ReverseMap();
    }
}