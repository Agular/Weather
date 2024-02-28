using MongoDB.Driver.Linq;
using MongoDB.Driver;
using WebApi.Models.Base.Interfaces;

namespace WebApi.Database.Repositories;

public interface ICommonRepository {
	IMongoQueryable<TDocument> GetAsQueryable<TDocument>() where TDocument : IDocument;
	IMongoCollection<TDocument> GetCollection<TDocument>() where TDocument : IDocument;
	Task<TDocument> Get<TDocument>(FilterDefinition<TDocument> filter, FindOptions<TDocument>? findOptions = null) where TDocument : IDocument;
	Task<List<TDocument>> GetMany<TDocument>(FilterDefinition<TDocument> filter, FindOptions<TDocument>? findOptions = null) where TDocument : IDocument;
	Task<TDocument> Add<TDocument>(TDocument document) where TDocument : IDocument;
	Task AddMany<TDocument>(List<TDocument> documents) where TDocument : IDocument;
}