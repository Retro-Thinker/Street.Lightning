using System.Runtime.InteropServices.JavaScript;

namespace Street.Lightning.DTO.Features.City;

public class YearlyPowerUsageDto
{
    public IDictionary<int, double> MonthlyPowerUsage { get; set; }
}