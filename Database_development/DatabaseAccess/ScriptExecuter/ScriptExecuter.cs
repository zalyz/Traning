using System.Data.SqlClient;
using System.IO;

namespace DatabaseAccess.ScriptExecuter
{
    /// <summary>
    /// Contains method for the script executing.
    /// </summary>
    public static class ScriptExecuter
    {
        /// <summary>
        /// Executes sql script.
        /// </summary>
        /// <param name="filePath">Script file path.</param>
        /// <param name="connectionString">Connection string of the database.</param>
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
