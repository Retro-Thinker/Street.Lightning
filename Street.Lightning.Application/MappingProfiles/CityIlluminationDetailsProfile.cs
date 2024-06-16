using AutoMapper;
using Street.Lightning.Application.Contracts.Persistence.Features.CityIllumination.Commands.CreateCityIllumination;
using Street.Lightning.Domain;
using Street.Lightning.DTO.Features.Common.CityIllumination;

namespace Street.Lightning.Application.MappingProfiles;

public class CityIlluminationDetailsProfile : Profile
{
    public CityIlluminationDetailsProfile()
    {
        CreateMap<CityIlluminationDetails, CityIlluminationDto>().ReverseMap();
        CreateMap<CreateCityIlluminationCommand, CityIlluminationDetails>();
    }
}