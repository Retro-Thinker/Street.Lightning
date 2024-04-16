using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Street.Lightning.Domain.Common;

namespace Street.Lightning.Domain;

public class CityIlluminationDetails : BaseEntity
{
    [Key, Column(Order = 0), ForeignKey("CityId")] 
    public int CityId { get; set; }
    [Key, Column(Order = 1), ForeignKey("IlluminationId")]
    public int IlluminationId { get; set; }

    public City City { get; set; }
    public Illumination IlluminationDetails { get; set; }

    public int QuantityOfBulbs { get; set; }
}