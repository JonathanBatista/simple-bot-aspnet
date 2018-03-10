using SimpleBot.Repo;
using System.Configuration;

namespace SimpleBot
{
    public class SimpleBotUser
    {
        private static IUserRepo _mongoRepo = new UserMongoRepo(ConfigurationManager.AppSettings["mongodb"]);

        private static IUserRepo _sqlRepositorio = new UserSqlRepo(ConfigurationManager.AppSettings["sql"]);
                

        public static string Reply(Message message)
        {

            string userId = message.Id;

            var perfil = GetProfile(userId);

            perfil.Visitas += 1;           

            var botResponse = $"{message.User} conversou {perfil.Visitas} vezes";

            _sqlRepositorio.RecordMessages(message, botResponse);

            return botResponse;
        }

        public static UserProfile GetProfile(string id)
        {
            UserProfile userProfile = null;
            
            userProfile = _mongoRepo.GetProfile(id);

            if (userProfile == null)
            {
                userProfile = new UserProfile()
                {
                    Id = id,
                    Visitas = 0
                };

                SalvarNovoProfile(userProfile);
            }

            SetProfile(id, userProfile);

            
            return userProfile;
        }

        public static void SetProfile(string id, UserProfile profile)
        {
            AtualizarProfile(id, ++profile.Visitas);
        }


        private static bool SalvarNovoProfile(UserProfile profile)
        {
            return _mongoRepo.SalvarProfile(profile);
        }

        private static bool AtualizarProfile(string id, int qtdeVisitas)
        {
            return _mongoRepo.AtualizarProfile(qtdeVisitas, id);
        }


        
    }
}