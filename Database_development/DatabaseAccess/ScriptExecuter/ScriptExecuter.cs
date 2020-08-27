using System.Data.SqlClient;
using System.IO;

namespace DatabaseAccess.ScriptExecuter
{
    public static class ScriptExecuter
    {
        public static void ExecuteScript(string filePath, string connectionString)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var command = File.ReadAllText(filePath);
                using (var sqlCommand = new SqlCommand(command, sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
