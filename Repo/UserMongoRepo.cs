using MongoDB.Bson;
using MongoDB.Driver;

namespace SimpleBot.Repo
{
    public class UserMongoRepo : IUserRepo
    {
        private static MongoClient _cliente = new MongoClient();

        public UserMongoRepo()
        {
        }

        public void RecordMessages(Message message, string botResponse)
        {
            var db = _cliente.GetDatabase("BotRecordMessage");

            var newDocument = new BsonDocument()
            {
                { $"{message.User}", new BsonDocument(){
                        { "userMessage", $"{message.Text}" },
                        { "botMessage", botResponse },
                    }
                }

            };


            var col = db.GetCollection<BsonDocument>("Messages");

            col.InsertOne(newDocument);
        }
    }
}