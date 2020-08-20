using System;
using System.Configuration;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using DatabaseAccess.Attributes;

namespace DatabaseAccess
{
    public class DatabaseAccess<T> : IDatabaseAccess<T>
        where T : class
    {
        private readonly string _connectionString;

        private static DatabaseAccess<T> databaseAccess;

        private DatabaseAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CreateEntity(T entity)
        {
            if (!IsTableExist())
            {
                CreateTable();
            }

           /* using (var sqlConnection = new SqlConnection("Data Source=../../../../TrainDB.db; Version=3;"))
            {
                sqlConnection.Open();
                var sb = new StringBuilder();

                Type type = typeof(T);
                var a = type.GetMembers(BindingFlags.GetProperty);

                //Add new Entitys to database
                sb.Clear();
                sb.Append("INSERT INTO Trains (TrainNumber, TrainName, Category, ArrivalTime_Hour, ArrivalTime_Minutes, DepartureTime_Hour, DepartureTime_Minutes) ");
                sb.Append("VALUES (@TrainNumber, @TrainName, @Category, @ArrivalTime_Hour, @ArrivalTime_Minutes, @DepartureTime_Hour, @DepartureTime_Minutes);");
                using (var command = new SqlCommand(sb.ToString(), sqlConnection))
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@TrainNumber", entity.TrainNumber.ToString());
                    command.Parameters.AddWithValue("@TrainName", entity.TrainName);
                    command.Parameters.AddWithValue("@Category", entity.Category);
                    command.Parameters.AddWithValue("@ArrivalTime_Hour", entity.ArrivalTime.Hour.ToString());
                    command.Parameters.AddWithValue("@ArrivalTime_Minutes", entity.ArrivalTime.Minutes.ToString());
                    command.Parameters.AddWithValue("@DepartureTime_Hour", entity.DepartureTime.Hour.ToString());
                    command.Parameters.AddWithValue("@DepartureTime_Minutes", entity.DepartureTime.Minutes.ToString());
                    command.ExecuteNonQuery();
                }
            }*/
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> ReadAll()
        {
            throw new NotImplementedException();
        }

        public void Update(T entityToReplace, T substituteEntity)
        {
            throw new NotImplementedException();
        }

        public void CreateTable()
        {
            if (!IsTableExist())
            {
                using (var sqlConnnection = new SqlConnection(_connectionString))
                {
                    sqlConnnection.Open();
                    var command = GetCreateTableCommand();

                    using (var sqlCommand = new SqlCommand(command, sqlConnnection))
                    {
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        public void CreateTable(string scriptFilePath)
        {
            if (!IsTableExist())
            {
                using (var sqlConnnection = new SqlConnection(_connectionString))
                {
                    sqlConnnection.Open();
                    var command = File.ReadAllText(scriptFilePath);

                    using (var sqlCommand = new SqlCommand(command, sqlConnnection))
                    {
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        public static DatabaseAccess<T> Factory(string connectionString)
        {
            if (databaseAccess == null)
            {
                databaseAccess = new DatabaseAccess<T>(connectionString);
            }

            return databaseAccess;
        }

        private string GetCreateTableCommand()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Create Table " + typeof(T).Name);
            stringBuilder.Append("\n(\n");
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                if (property.GetCustomAttribute<ComplexTypeAttribute>() != null)
                {
                    stringBuilder.Append(GetInnerComplexTypeProperties(property.PropertyType));
                }
                else
                {
                    var propertyType = GetSqlTypeOfProperty(property.PropertyType);
                    stringBuilder.Append(property.Name + " " + propertyType + ",\n");
                }
            }
            stringBuilder.Append(")");
            return stringBuilder.ToString();
        }

        private string GetInnerComplexTypeProperties(Type type)
        {
            var stringBuilder = new StringBuilder();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property.GetCustomAttribute<ComplexTypeAttribute>() != null)
                {
                    stringBuilder.Append(GetInnerComplexTypeProperties(property.PropertyType));
                }
                else
                {
                    var propertyType = GetSqlTypeOfProperty(property.PropertyType);
                    stringBuilder.Append(type.Name + "_" + property.Name + " " + propertyType + ",\n");
                }
            }

            return stringBuilder.ToString();
        }

        private bool IsTableExist()
        {
            using (var sqlConnnection = new SqlConnection(_connectionString))
            {
                sqlConnnection.Open();

                DataTable dTable = sqlConnnection.GetSchema("TABLES",
                           new string[] { null, null, typeof(T).Name });

                return dTable.Rows.Count > 0;
            }
        }

        private string GetSqlTypeOfProperty(Type propertyType)
        {
            if (propertyType == typeof(short))
            {
                return "smallint";
            }

            if (propertyType == typeof(int))
            {
                return "int";
            }

            if (propertyType == typeof(long))
            {
                return "Bigint";
            }

            if (propertyType == typeof(double))
            {
                return "Float";
            }

            if (propertyType == typeof(string))
            {
                return "varchar(100)";
            }

            if (propertyType == typeof(char))
            {
                return "Char";
            }

            if (propertyType == typeof(DateTime))
            {
                return "varchar(30)";
            }

            throw new ArgumentException();
        }
    }
}
