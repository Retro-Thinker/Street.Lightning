namespace Street.Lightning.DTO.Features.Common.Illumination;

public class IlluminationDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string IlluminationProvider { get; set; } = string.Empty;
    public double Power { get; set; }
}