using Street.Lightning.DTO.Features.Common.City;
using Street.Lightning.DTO.Features.Common.Illumination;

namespace Street.Lightning.DTO.Features.Common.CityIllumination;

public class CityIlluminationDto
{
    public int CityId { get; set; }
    public int IlluminationId { get; set; }
    public int QuantityOfBulbs { get; set; }
}