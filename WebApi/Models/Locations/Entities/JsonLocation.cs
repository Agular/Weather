namespace WebApi.Models.Locations.Entities;

public class JsonLocation {
	public string Name { get; set; }
	public string State { get; set; }
	public string Country { get; set; }
	public JsonLocationCoords Coord { get; set; }
}

public class JsonLocationCoords {
	public double Lat { get; set; }
	public double Lon { get; set; }
}