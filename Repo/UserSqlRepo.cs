using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SimpleBot.Repo
{
    public class UserSqlRepo : IUserRepo
    {
        private readonly string _connectionString;

        public UserSqlRepo(string connectionString)
        {
            _connectionString = connectionString;
        }


        public void RecordMessages(Message message, string botResponse)
        {

            try
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
            catch (Exception ex)
            {
                throw;
            }          
        }
    }
}