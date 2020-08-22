using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using DatabaseAccess.Attributes;

namespace DatabaseAccess
{
    public class DatabaseAccess<T> : IDatabaseAccess<T>, IDisposable
        where T : class
    {
        private SqlConnection _sqlConnection;

        private static DatabaseAccess<T> _databaseAccess;

        private DatabaseAccess(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            _sqlConnection.Open();
        }

        public void CloseConnection()
        {
            _sqlConnection.Close();
        }

        public void CreateEntity(T entity)
        {
            OpenConnectionIfItClosed();

            if (!IsTableExist())
            {
                CreateTable();
            }

            var createEntityCommand = GetCreateEntityCommand(typeof(T));
            createEntityCommand.Append(GetPropertiesNames(typeof(T)));
            using (var command = new SqlCommand(createEntityCommand.ToString(), _sqlConnection))
            {
                command.Parameters.Clear();

                SetProperiesValues(entity, command);

                command.ExecuteNonQuery();

            }
        }

        private void SetProperiesValues(object entity, SqlCommand sqlCommand)
        {
            var properies = entity.GetType().GetProperties();
            foreach (var property in properies)
            {
                if (property.GetCustomAttribute<ComplexTypeAttribute>() != null)
                {
                    SetInnerComplexTypePropsValues(property.GetValue(entity), sqlCommand);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@" + property.Name, property.GetValue(entity));
                }
            }
        }

        private void SetInnerComplexTypePropsValues(object entity, SqlCommand sqlCommand)
        {
            var type = entity.GetType();
            var properies = type.GetProperties();
            foreach (var property in properies)
            {
                if (property.GetCustomAttribute<ComplexTypeAttribute>() != null)
                {
                    SetInnerComplexTypePropsValues(property.GetValue(entity), sqlCommand);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@" + type.Name + "_" + property.Name, property.GetValue(entity));
                }
            }
        }

        public void Delete(T entity)
        {
            OpenConnectionIfItClosed();

            throw new NotImplementedException();
        }

        public IEnumerable<T> ReadAll()
        {
            OpenConnectionIfItClosed();

            throw new NotImplementedException();
        }

        public void Update(T entityToReplace, T substituteEntity)
        {
            OpenConnectionIfItClosed();

            throw new NotImplementedException();
        }

        public void CreateTable()
        {
            if (!IsTableExist())
            {
                var command = GetCreateTableCommand();
                using (var sqlCommand = new SqlCommand(command, _sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public void CreateTable(string scriptFilePath)
        {
            if (!IsTableExist())
            {
                var command = File.ReadAllText(scriptFilePath);
                using (var sqlCommand = new SqlCommand(command, _sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public static DatabaseAccess<T> Factory(string connectionString)
        {
            if (_databaseAccess == null)
            {
                _databaseAccess = new DatabaseAccess<T>(connectionString);
            }

            return _databaseAccess;
        }

        public void Dispose()
        {
            _sqlConnection.Dispose();
        }

        private StringBuilder GetCreateEntityCommand(Type type)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("INSERT " + type.Name + "(");
            var properties = type.GetProperties();
            for (int index = 0; index < properties.Length; index++)
            {
                if (properties[index].GetCustomAttribute<ComplexTypeAttribute>() != null)
                {
                    stringBuilder.Append(GetInnerComplexTypeCreateCommand(properties[index].PropertyType));
                }
                else
                {
                    if (index < properties.Length - 1)
                    {
                        stringBuilder.Append(properties[index].Name + ", ");
                    }
                    else
                    {
                        stringBuilder.Append(properties[index].Name);
                    }
                }
            }

            stringBuilder.Append(") ");
            return stringBuilder;
        }

        private string GetInnerComplexTypeCreateCommand(Type type)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("INSERT " + type.Name + "(");
            var properties = type.GetProperties();
            for (int index = 0; index < properties.Length; index++)
            {
                if (properties[index].GetCustomAttribute<ComplexTypeAttribute>() != null)
                {
                    stringBuilder.Append(GetInnerComplexTypeCreateCommand(properties[index].PropertyType));
                }
                else
                {
                    if (index < properties.Length - 1)
                    {
                        stringBuilder.Append(type.Name + "_" + properties[index].Name + ", ");
                    }
                    else
                    {
                        stringBuilder.Append(type.Name + "_" + properties[index].Name);
                    }
                }
            }

            stringBuilder.Append(") ");
            return stringBuilder.ToString();
        }

        private string GetPropertiesNames(Type type)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("VALUES (");
            var properties = type.GetProperties();
            for (int index = 0; index < properties.Length; index++)
            {
                if (properties[index].GetCustomAttribute<ComplexTypeAttribute>() != null)
                {
                    stringBuilder.Append(GetInnerComplexTypePropertiesNames(properties[index].PropertyType));
                }
                else
                {
                    if (index < properties.Length - 1)
                    {
                        stringBuilder.Append("@" + properties[index].Name + ", ");
                    }
                    else
                    {
                        stringBuilder.Append("@" + properties[index].Name);
                    }
                }
            }
            stringBuilder.Append(");");
            return stringBuilder.ToString();
        }

        private string GetInnerComplexTypePropertiesNames(Type type)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("VALUES (");
            var properties = type.GetProperties();
            for (int index = 0; index < properties.Length; index++)
            {
                if (properties[index].GetCustomAttribute<ComplexTypeAttribute>() != null)
                {
                    stringBuilder.Append(GetInnerComplexTypePropertiesNames(properties[index].PropertyType));
                }
                else
                {
                    if (index < properties.Length - 1)
                    {
                        stringBuilder.Append("@" + type.Name + "_" + properties[index].Name + ", ");
                    }
                    else
                    {
                        stringBuilder.Append("@" + type.Name + "_" + properties[index].Name);
                    }
                }
            }
            stringBuilder.Append(");");
            return stringBuilder.ToString();
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
                    stringBuilder.Append(GetInnerComplexTypePropsForCreatingTable(property.PropertyType));
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

        private string GetInnerComplexTypePropsForCreatingTable(Type type)
        {
            var stringBuilder = new StringBuilder();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property.GetCustomAttribute<ComplexTypeAttribute>() != null)
                {
                    stringBuilder.Append(GetInnerComplexTypePropsForCreatingTable(property.PropertyType));
                }
                else
                {
                    var propertyType = GetSqlTypeOfProperty(property.PropertyType);
                    stringBuilder.Append(type.Name + "_" + property.Name + " " + propertyType + ",\n");
                }
            }

            return stringBuilder.ToString();
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

        private bool IsTableExist()
        {
                DataTable dTable = _sqlConnection.GetSchema("TABLES",
                           new string[] { null, null, typeof(T).Name });

                return dTable.Rows.Count > 0;
        }

        private void OpenConnectionIfItClosed()
        {
            if (_sqlConnection.State == ConnectionState.Closed)
            {
                OpenConnection();
            }
        }
    }
}
