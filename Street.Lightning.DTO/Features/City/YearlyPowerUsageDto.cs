using System.Runtime.InteropServices.JavaScript;

namespace Street.Lightning.DTO.Features.City;

public class YearlyPowerUsageDto
{
    public IDictionary<DateTime, long> MonthlyPowerUsage { get; set; }
}