using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Street.Lightning.Domain.Common;

namespace Street.Lightning.Domain;

public class City : BaseEntityOneKey
{
    [ForeignKey("CountryId")]
    public int CountryId { get; set; }

    public Country Country { get; set; }
    public string CityName { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    
    public virtual IEnumerable<CityIlluminationDetails> CityIlluminationDetails { get; set; }
}