using System.ComponentModel.DataAnnotations.Schema;
using Street.Lightning.Domain.Common;

namespace Street.Lightning.Domain;

public class Illumination : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string IlluminationProvider { get; set; } = string.Empty;
    public double Power { get; set; }
}