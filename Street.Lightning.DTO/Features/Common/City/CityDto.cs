namespace Street.Lightning.DTO.Features.Common.City;

public class CityDto
{
    public int Id { get; set; }
    public string CityName { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int CountryId { get; set; }
    public string CountryName { get; set; }
}