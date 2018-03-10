using System.Data.SqlClient;

namespace SimpleBot.Repo
{
    public class UserSqlRepo : IUserRepo
    {
        private readonly string _connectionString;

        public UserSqlRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool AtualizarProfile(int qtdeVisitas, string id)
        {
            throw new System.NotImplementedException();
        }

        public UserProfile GetProfile(string id)
        {
            throw new System.NotImplementedException();
        }

        public void RecordMessages(Message message, string botResponse)
        {
            using (var sqlConn = new SqlConnection(_connectionString))
            {
                using (var sqlCmd = new SqlCommand())
                {
                    sqlConn.Open();

                    sqlCmd.Connection = sqlConn;

                    sqlCmd.Parameters.AddWithValue("@UserId", message.Id);
                    sqlCmd.Parameters.AddWithValue("@UserMessage", message.Text);
                    sqlCmd.Parameters.AddWithValue("@BotMessage", botResponse);


                    sqlCmd.CommandText = "INSERT INTO dbo.Messages(UserId, UserMessage, BotMessage) VALUES(@UserId, @UserMessage, @BotMessage)";

                    sqlCmd.ExecuteNonQuery();

                    sqlConn.Close();
                }
            }         
        }

        public bool SalvarProfile(UserProfile userProfile)
        {
            throw new System.NotImplementedException();
        }
    }
}