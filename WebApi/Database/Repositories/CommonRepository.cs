using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using WebApi.Database.Configuration;
using WebApi.Models.Base.Attributes;
using WebApi.Models.Base.Interfaces;

namespace WebApi.Database.Repositories;

public class CommonRepository : ICommonRepository {
	private readonly IMongoDatabase _db;

	public CommonRepository(IMongoClient mongoClient, IOptions<MongoDbConfiguration> mongoDbOptions) {
		_db = mongoClient.GetDatabase(mongoDbOptions.Value.DatabaseName);
	}

	private protected static string GetCollectionName(Type documentType) {
		var collectionName = ((BsonCollectionAttribute)documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault())?.CollectionName;
		if (collectionName != null) {
			return collectionName;
		}

		throw new Exception($"No BsonCollection attribute was found for document of type {documentType}");
	}

	public IMongoCollection<TDocument> GetCollection<TDocument>() where TDocument : IDocument {
		return _db.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
	}

	public IMongoQueryable<TDocument> GetAsQueryable<TDocument>() where TDocument : IDocument {
		return GetCollection<TDocument>().AsQueryable();
	}

	public async Task<TDocument> Get<TDocument>(FilterDefinition<TDocument> filter, FindOptions<TDocument>? findOptions = null) where TDocument : IDocument {
		var asyncCursor = await GetCollection<TDocument>().FindAsync(filter, findOptions);
		var entity = await asyncCursor.FirstOrDefaultAsync();
		return entity;
	}

	public async Task<List<TDocument>> GetMany<TDocument>(FilterDefinition<TDocument> filter, FindOptions<TDocument>? findOptions = null) where TDocument : IDocument {
		var asyncCursor = await GetCollection<TDocument>().FindAsync(filter, findOptions);
		var entities = await asyncCursor.ToListAsync();
		return entities;
	}

	public async Task<TDocument> Add<TDocument>(TDocument document) where TDocument : IDocument {
		await GetCollection<TDocument>().InsertOneAsync(document);
		return document;
	}

	public async Task AddMany<TDocument>(List<TDocument> documents) where TDocument : IDocument {
		await GetCollection<TDocument>().InsertManyAsync(documents);
	}
}