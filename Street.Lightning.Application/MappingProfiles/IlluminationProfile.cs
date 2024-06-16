using AutoMapper;
using Street.Lightning.Application.Contracts.Persistence.Features.Illumination.Commands.CreateIlluminationProvider;
using Street.Lightning.Domain;
using Street.Lightning.DTO.Features.Common.Illumination;

namespace Street.Lightning.Application.MappingProfiles;

public class IlluminationProfile : Profile
{
    public IlluminationProfile()
    {
        CreateMap<Illumination, IlluminationDto>().ReverseMap();
        CreateMap<CreateIlluminationProviderCommand, Illumination>();
    }
}