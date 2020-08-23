using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using DatabaseAccess.Attributes;
using DatabaseAccess.DatabaseExceptions;

namespace DatabaseAccess
{
    public class DatabaseAccess<T> : IDatabaseAccess<T>, IDisposable
        where T : class, new ()
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

            var createEntityCommand = GetInsertCommand(typeof(T));
            createEntityCommand.Append(GetValuesString(typeof(T)));
            using (var command = new SqlCommand(createEntityCommand.ToString(), _sqlConnection))
            {
                command.Parameters.Clear();
                SetProperiesValues(entity, command);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(T entity)
        {
            OpenConnectionIfItClosed();
            if (!IsTableExist())
            {
                throw new TableNotFoundException("Table " + entity.GetType().Name + "is not found at " + _sqlConnection.Database);
            }

            var createEntityCommand = GetDeleteCommand(typeof(T));
            using (var command = new SqlCommand(createEntityCommand.ToString(), _sqlConnection))
            {
                command.Parameters.Clear();
                SetProperiesValues(entity, command);
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<T> ReadAll()
        {
            OpenConnectionIfItClosed();
            if (!IsTableExist())
            {
                CreateTable();
            }

            var listOfTableValues = new List<T>();
            var selectAllCommand = GetSelectAllCommand();
            using (var command = new SqlCommand(selectAllCommand, _sqlConnection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        var entity = ReadEntityFromDatabase(reader);
                        listOfTableValues.Add(entity);
                    }
                }
            }

            return listOfTableValues;
        }

        public void Update(T entityToReplace, T substituteEntity)
        {
            OpenConnectionIfItClosed();
            if (!IsTableExist())
            {
                CreateTable();
            }

            throw new NotImplementedException();
        }

        public void CreateTable()
        {
            OpenConnectionIfItClosed();
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
            OpenConnectionIfItClosed();
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

        //----------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------

        private T ReadEntityFromDatabase(SqlDataReader dataReader)
        {
            var entity = Activator.CreateInstance(typeof(T));
            var props = entity.GetType().GetProperties();
            foreach (var property in props)
            {
                if (property.GetCustomAttribute<ComplexTypeAttribute>() != null)
                {
                    property.SetValue(entity, SetInnerComplexTypePropertyValue(property.PropertyType, dataReader));
                }
                else
                {
                    var propertyValue = ParseDatabaseValue(property.PropertyType, property.Name, dataReader);
                    property.SetValue(entity, propertyValue);
                }
            }

            return (T)entity;
        }

        private object ParseDatabaseValue(Type propertyType, string columnName, SqlDataReader dataReader)
        {
            if (propertyType == typeof(short))
            {
                return dataReader.GetInt16(columnName);
            }

            if (propertyType == typeof(int))
            {
                return dataReader.GetInt32(columnName);
            }

            if (propertyType == typeof(long))
            {
                return dataReader.GetInt64(columnName);
            }

            if (propertyType == typeof(double))
            {
                return dataReader.GetDouble(columnName);
            }

            if (propertyType == typeof(string))
            {
                return dataReader.GetString(columnName);
            }

            if (propertyType == typeof(char))
            {
                return dataReader.GetChar(columnName);
            }

            if (propertyType == typeof(DateTime))
            { 
                var date = dataReader.GetString(columnName);
                return DateTime.Parse(date);
            }

            throw new InvalidCastException("Cant set a property with type " + propertyType.DeclaringType.Name);
        }

        private object SetInnerComplexTypePropertyValue(Type innerComplexType, SqlDataReader dataReader)
        {
            var innerComplexObject = Activator.CreateInstance(innerComplexType);
            var props = innerComplexType.GetProperties();
            foreach (var property in props)
            {
                if (property.GetCustomAttribute<ComplexTypeAttribute>() != null)
                {
                    property.SetValue(innerComplexObject, SetInnerComplexTypePropertyValue(property.PropertyType, dataReader));
                }
                else
                {
                    var propertyValue = ParseDatabaseValue(property.PropertyType, innerComplexType.Name + "_" + property.Name, dataReader);
                    property.SetValue(innerComplexObject, propertyValue);
                }
            }

            return innerComplexType;
        }

        private string GetSelectAllCommand()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("SELECT ");
            GetPropsNames(typeof(T), stringBuilder);
            stringBuilder.Append(" FROM " + typeof(T).Name + ";");
            return stringBuilder.ToString();
        }

        private string GetDeleteCommand(Type type)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"DELETE FROM {type.Name} WHERE ");
            var properties = type.GetProperties();
            for (int index = 0; index < properties.Length; index++)
            {
                if (properties[index].GetCustomAttribute<ComplexTypeAttribute>() != null)
                {
                    var innerComplexTypeRemoveCommand = GetInnerComplexTypeDeleteCommand(properties[index].PropertyType);
                    stringBuilder.Append(innerComplexTypeRemoveCommand);
                }
                else
                {
                    if (index < properties.Length - 1)
                    {
                        stringBuilder.Append(properties[index].Name + "=@" + properties[index].Name + " AND ");
                    }
                    else
                    {
                        stringBuilder.Append(properties[index].Name + "=@" + properties[index].Name);
                    }
                }
            }

            stringBuilder.Append(";");
            return stringBuilder.ToString();
        }

        private string GetInnerComplexTypeDeleteCommand(Type type)
        {
            var stringBuilder = new StringBuilder();
            var properties = type.GetProperties();
            for (int index = 0; index < properties.Length; index++)
            {
                if (properties[index].GetCustomAttribute<ComplexTypeAttribute>() != null)
                {
                    var innerComplexTypeRemoveCommand = GetInnerComplexTypeDeleteCommand(properties[index].PropertyType);
                    stringBuilder.Append(innerComplexTypeRemoveCommand);
                }
                else
                {
                    stringBuilder.Append(type.Name + "_" + properties[index].Name + "=@" + type.Name + "_" + properties[index].Name + " ");
                }
            }

            return stringBuilder.ToString();
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

        private void GetPropsNames(Type type, StringBuilder stringBuilder)
        {
            var properties = type.GetProperties();
            for (int index = 0; index < properties.Length; index++)
            {
                if (properties[index].GetCustomAttribute<ComplexTypeAttribute>() != null)
                {
                    GetInnerComplexTypePropsNames(properties[index].PropertyType, stringBuilder);
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
        }

        private void GetInnerComplexTypePropsNames(Type type, StringBuilder stringBuilder)
        {
            var properties = type.GetProperties();
            for (int index = 0; index < properties.Length; index++)
            {
                if (properties[index].GetCustomAttribute<ComplexTypeAttribute>() != null)
                {
                    GetInnerComplexTypePropsNames(properties[index].PropertyType, stringBuilder);
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
        }

        private StringBuilder GetInsertCommand(Type type)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("INSERT " + type.Name + "(");
            GetPropsNames(type, stringBuilder);
            stringBuilder.Append(") ");
            return stringBuilder;
        }

        private string GetValuesString(Type type)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("VALUES (");
            var properties = type.GetProperties();
            for (int index = 0; index < properties.Length; index++)
            {
                if (properties[index].GetCustomAttribute<ComplexTypeAttribute>() != null)
                {
                    stringBuilder.Append(GetInnerComplexTypeValueString(properties[index].PropertyType));
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

        private string GetInnerComplexTypeValueString(Type type)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("VALUES (");
            var properties = type.GetProperties();
            for (int index = 0; index < properties.Length; index++)
            {
                if (properties[index].GetCustomAttribute<ComplexTypeAttribute>() != null)
                {
                    stringBuilder.Append(GetInnerComplexTypeValueString(properties[index].PropertyType));
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

            throw new InvalidCastException("Cant create a column with type " + propertyType.DeclaringType.Name);
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
