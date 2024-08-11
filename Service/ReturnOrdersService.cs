using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using ReturnOrdersApi.Model;
using ReturnOrdersApi.Context;


namespace ReturnOrdersApi.Service
{
    public class ReturnOrdersService
    {
        private readonly IMongoCollection<ReturnOrder> _returnOrdersCollection;

        public ReturnOrdersService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _returnOrdersCollection = mongoDatabase.GetCollection<ReturnOrder>(mongoDbSettings.Value.CollectionName);
        }

        public async Task<List<ReturnOrder>> GetAsync() =>
            await _returnOrdersCollection.Find(_ => true).ToListAsync();

        public async Task<ReturnOrder> GetAsync(string id) =>
            await _returnOrdersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ReturnOrder newOrder) =>
            await _returnOrdersCollection.InsertOneAsync(newOrder);

        public async Task UpdateAsync(string id, ReturnOrder updatedOrder) =>
            await _returnOrdersCollection.ReplaceOneAsync(x => x.Id == id, updatedOrder);

        public async Task RemoveAsync(string id) =>
            await _returnOrdersCollection.DeleteOneAsync(x => x.Id == id);
    }
}
