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
            if (_perfil.ContainsKey(id))
                return _perfil[id];



            return new UserProfile
            {
                Id = id,
                Visitas = 0
            };
        }

        public static void SetProfile(string id, UserProfile profile)
        {
            _perfil[id] = profile;
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