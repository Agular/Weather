using WebApi.Models.Base.Attributes;
using WebApi.Models.Base.Constants;
using WebApi.Models.Base.Entities;

namespace WebApi.Models.Locations.Entities;

[BsonCollection(DocumentCollections.Location)]
public class Location : Document {
	public string Name { get; set; }
	public string State { get; set; }
	public string Country { get; set; }
	public double Latitude { get; set; }
	public double Longitude { get; set; }
}