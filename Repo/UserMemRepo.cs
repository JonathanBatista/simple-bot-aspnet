using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot.Repo
{
    public class UserMemRepo : IUserRepo
    {

        private static Dictionary<string, UserProfile> _perfil = new Dictionary<string, UserProfile>();

        public void RecordMessages(Message message, string botResponse)
        {
            throw new NotImplementedException();
        }
    }
}