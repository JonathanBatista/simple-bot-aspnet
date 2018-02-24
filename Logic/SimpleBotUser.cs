using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot
{
    public class SimpleBotUser
    {
        private static MongoClient _cliente = new MongoClient();

        private static readonly IMongoDatabase _db = _cliente.GetDatabase("BotRecordMessage");

        private static Dictionary<string, UserProfile> _perfil = new Dictionary<string, UserProfile>();

        

        public static string Reply(Message message)
        {

            string userId = message.Id;

            var perfil = GetProfile(userId);

            perfil.Visitas += 1;

            SetProfile(userId, perfil);

            var botResponse = $"{message.User} conversou {perfil.Visitas} vezes";

            return botResponse;
        }

        public static UserProfile GetProfile(string id)
        {
            UserProfile userProfile = null;

            try
            {
                var col = _db.GetCollection<UserProfile>("Perfil");

                var filtro = Builders<UserProfile>.Filter.Where(x => x.Id.Equals(id));

                userProfile = col.Find(filtro).FirstOrDefault();

                if (userProfile == null)
                {
                    userProfile = new UserProfile()
                    {
                        Id = id,
                        Visitas = 0
                    };

                    SalvarNovoProfile(userProfile);
                }

                AtualizarProfile(id, ++userProfile.Visitas);
            }
            catch (Exception ex)
            {
                throw;
            }            

            
            return userProfile;
        }

        public static void SetProfile(string id, UserProfile profile)
        {
            _perfil[id] = profile;
        }


        private static bool SalvarNovoProfile(UserProfile profile)
        {
            var col = _db.GetCollection<UserProfile>("Perfil");

            col.InsertOne(profile);

            return true;
        }

        private static bool AtualizarProfile(string id, int qtdeVisitas)
        {
            var col = _db.GetCollection<UserProfile>("Perfil");

            var update = Builders<UserProfile>.Update.Set(x => x.Visitas, qtdeVisitas);
            var filtro = Builders<UserProfile>.Filter.Where(x => x.Id.Equals(id));

            col.UpdateOne(filtro, update);

            return true;
        }


        private static void RecordMessages(Message message, string botResponse)
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