using System;
using System.Data.SqlClient;

namespace SampleVirtualEntity
{
    public static class Connection
    {
        public static SqlConnection GetConnection()
        {
            try
            {
                //sample database to connect to 
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                //builder.DataSource = "Enter name or network address of the SQL Server";
                //builder.UserID = "Enter User Name";
                //builder.Password = "Enter password";
                //builder.InitialCatalog = "Enter database details";
                string connectionString = "Data Source=cicchatbotbotlogsdbserver.database.windows.net;Initial Catalog=CICChatbotBotLogsDB;User ID=trudata;Password=Metro#123";

                SqlConnection connection = new SqlConnection(connectionString);
                return connection;
            }
            catch (SqlException e)
            {
                Console.WriteLine("Erorr while Connecting Sql server");
                Console.WriteLine(e.ToString());
                throw;
            }
        }
    }
}
