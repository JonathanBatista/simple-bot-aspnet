using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot.Repo
{
    public class UserMemRepo : IUserRepo
    {

        private static Dictionary<string, UserProfile> _perfil = new Dictionary<string, UserProfile>();

        public bool AtualizarProfile(int qtdeVisitas, string id)
        {
            throw new NotImplementedException();
        }

        public UserProfile GetProfile(string id)
        {
            throw new NotImplementedException();
        }

        public void RecordMessages(Message message, string botResponse)
        {
            throw new NotImplementedException();
        }

        public bool SalvarProfile(UserProfile userProfile)
        {
            throw new NotImplementedException();
        }
    }
}