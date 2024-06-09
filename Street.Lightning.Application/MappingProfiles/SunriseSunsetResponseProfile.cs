using System.Globalization;
using AutoMapper;
using Newtonsoft.Json.Linq;
using Street.Lightning.DTO.Features.Common.SunriseSunsetAPI;

namespace Street.Lightning.Application.MappingProfiles;

public class SunriseSunsetResponseProfile : Profile
{
    public SunriseSunsetResponseProfile()
    {
        
        CreateMap<JObject, SunriseSunsetResponseDto>()
            .ForMember(dest => dest.SunriseTime,
                opt =>
                    opt.MapFrom(src =>
                        DateTime.ParseExact(src["sunrise"].ToString(), "h:mm:ss tt", CultureInfo.InvariantCulture)
                            .TimeOfDay))

            .ForMember(dest => dest.SunsetTime,
                opt =>
                    opt.MapFrom(src =>
                        DateTime.ParseExact(src["sunset"].ToString(), "h:mm:ss tt", CultureInfo.InvariantCulture)
                            .TimeOfDay))

            .ForMember(dest => dest.TrackingDate,
                opt =>
                    opt.MapFrom(src => DateTime.Parse(src["date"].ToString(), CultureInfo.InvariantCulture)))

            .ForMember(dest => dest.StreetLightsOnDuration,
                opt =>
                    opt.MapFrom(src => CountHoursDifference(src["sunrise"].ToString(), src["sunset"].ToString())));
        
        CreateMap<List<JToken>, IEnumerable<SunriseSunsetResponseDto>>();
    }

    private double CountHoursDifference(string sunrise, string sunset)
    {
        var sunriseStamp = DateTime.ParseExact(sunrise, "h:mm:ss tt", CultureInfo.InvariantCulture).TimeOfDay;
        var sunsetStamp = DateTime.ParseExact(sunset, "h:mm:ss tt", CultureInfo.InvariantCulture).TimeOfDay;

        if (sunsetStamp < sunriseStamp)
        { 
            return (sunriseStamp - sunsetStamp).TotalHours;
        }

        return (new TimeSpan(24, 0, 0) - sunsetStamp + sunriseStamp).TotalHours;
    }
}