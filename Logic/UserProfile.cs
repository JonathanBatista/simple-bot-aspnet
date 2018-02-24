using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot
{
    [BsonIgnoreExtraElements]
    public class UserProfile
    {
        public string Id { get; set; }
        public int Visitas { get; set; }
    }
}