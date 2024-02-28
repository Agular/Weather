namespace WebApi.Database.Configuration; 

public class MongoDbConfiguration {
	public const string SectionKey = "MongoDb";
	public string ConnectionString { get; set; }
	public string DatabaseName { get; set; }
}