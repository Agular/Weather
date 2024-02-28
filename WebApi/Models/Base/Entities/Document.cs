using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WebApi.Models.Base.Interfaces;

namespace WebApi.Models.Base.Entities;

public class Document : IDocument {
	[BsonRepresentation(BsonType.ObjectId)]
	[BsonIgnoreIfDefault]
	public string Id { get; set; }
}