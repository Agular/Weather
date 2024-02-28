using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebApi.Models.Base.Interfaces; 

public interface IDocument {
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	[BsonIgnoreIfDefault]
	public string Id { get; set; }
}