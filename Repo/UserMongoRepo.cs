using MongoDB.Bson;
using MongoDB.Driver;

namespace SimpleBot.Repo
{
    public class UserMongoRepo : IUserRepo
    {
        private static MongoClient _cliente = new MongoClient();

        private readonly IMongoDatabase _db;
        

        public UserMongoRepo(string dbName)
        {
            _db = _cliente.GetDatabase(dbName);
        }

        public bool AtualizarProfile(int qtdeVisitas, string id)
        {
            try
            {
                var col = _db.GetCollection<UserProfile>("Perfil");

                var update = Builders<UserProfile>.Update.Set(x => x.Visitas, qtdeVisitas);
                var filtro = Builders<UserProfile>.Filter.Where(x => x.Id.Equals(id));

                col.UpdateOne(filtro, update);

                return true;
            }
            catch (System.Exception)
            {
                // Salvar erro
                return false;
            }
            
        }

        public UserProfile GetProfile(string id)
        {
            var col = _db.GetCollection<UserProfile>("Perfil");

            var filtro = Builders<UserProfile>.Filter.Where(x => x.Id.Equals(id));

            return col.Find(filtro).FirstOrDefault();
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

        public bool SalvarProfile(UserProfile userProfile)
        {
            try
            {
                var col = _db.GetCollection<UserProfile>("Perfil");

                col.InsertOne(userProfile);

                return true;
            }
            catch (System.Exception)
            {
                // Salvar erro
                return false;
            }
            
        }
    }
}