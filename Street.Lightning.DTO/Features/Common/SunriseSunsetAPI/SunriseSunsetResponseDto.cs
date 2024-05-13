using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.DTO.Features.Common.SunriseSunsetAPI;

public class SunriseSunsetResponseDto
{
    public DateTime TrackingDate { get; set; }
    public TimeSpan SunriseTime { get; set; }
    public TimeSpan SunsetTime { get; set; }
    public double StreetLightsOnDuration { get; set; }
    public ResultEnums OperationalStatus { get; set; }
}