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

        public static string Reply(Message message)
        {
            var botResponse = $"{message.User} disse '{message.Text}'";
            RecordMessages(message, botResponse: botResponse);
            return botResponse;
        }

        public static UserProfile GetProfile(string id)
        {
            return null;
        }

        public static void SetProfile(string id, UserProfile profile)
        {
        }


        private static void RecordMessages(Message message, string botResponse)
        {
            var db = _cliente.GetDatabase("BotRecordMessage");

            var newDocument = new BsonDocument()
            {
                { "userMessages", new BsonDocument(){
                        { $"{message.User}", new BsonDocument(){
                                { "userMessage", $"{message.Text}" },
                                { "botMessage", botResponse },
                            }
                        }
                    }
                }
            };


            var col = db.GetCollection<object>("Messages");

            col.InsertOne(newDocument);
        }
    }
}