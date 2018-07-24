using MongoDB.Driver;
using Logger.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace Logger
{
    public class LoggerContext
    {
        IMongoDatabase database;

        public LoggerContext(string connectionString)
        {
            database = new MongoClient(connectionString).GetDatabase(new MongoUrlBuilder(connectionString).DatabaseName);
        }

        private IMongoCollection<CommandHistory> CommandsHistories
        {
            get
            {
                return database.GetCollection<CommandHistory>("CommandsHistories");
            }
        }

        public async Task AddCommandAsync(Guid key, string uri, object before, object after, string userId, DateTime date)
        {
            var command = new CommandHistory {
                Id = key.ToString(),
                CommandUri = uri,
                BeforeState = JsonConvert.SerializeObject(before),
                AfterState = JsonConvert.SerializeObject(before),
                UserId = userId,
                DateTime = date
            };
            await CommandsHistories.InsertOneAsync(command);
        }

        public async Task<CommandHistory> GetCommandAsync(Guid key)
        {
            return await CommandsHistories.Find(new BsonDocument("_id", new ObjectId(key.ToString()))).FirstOrDefaultAsync();
        }
    }
}
