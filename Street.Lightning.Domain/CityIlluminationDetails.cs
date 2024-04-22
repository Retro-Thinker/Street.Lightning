using System.ComponentModel.DataAnnotations.Schema;
using Street.Lightning.Domain.Common;

namespace Street.Lightning.Domain;

public class CityIlluminationDetails : BaseEntityComposite
{ 
    public int CityId { get; set; }
    public City City { get; set; }
    public int IlluminationId { get; set; }
    public Illumination Illumination { get; set; }
    public int QuantityOfBulbs { get; set; }
}