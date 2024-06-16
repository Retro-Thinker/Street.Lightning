namespace Street.Lightning.DTO.Features.City;

public class DailyPowerUsageDto
{
    public DateTime TrackingDate { get; set; }
    public double StreetLightsOnDuration { get; set; }
    public double PowerUsage { get; set; }
}