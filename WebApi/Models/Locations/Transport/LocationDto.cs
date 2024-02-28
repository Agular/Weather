namespace WebApi.Models.Locations.Transport;

public class LocationDto {
	public string Name { get; set; }
	public string State { get; set; }
	public string Country { get; set; }
	public double Latitude { get; set; }
	public double Longitude { get; set; }
}