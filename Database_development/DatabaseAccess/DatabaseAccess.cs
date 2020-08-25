﻿using System;
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
    public class DatabaseAccess<T> : IDatabaseAccess<T>, ITableCreator, IDisposable
        where T : class, new ()
    {
        private SqlConnection _sqlConnection;

        private static DatabaseAccess<T> _databaseAccess;

        private DatabaseAccess(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
        }

        public void Add(T entity)
        {
            OpenConnectionAndExecute(entity, (entity) => AddEntity(entity));
        }

        public void Delete(T entity)
        {
            OpenConnectionAndExecute(entity, (entity) => DeleteEntity(entity));
        }

        public void Update(T entity)
        {
            OpenConnectionAndExecute(entity, (entity) => UpdateEntity(entity));
        }

        public IEnumerable<T> ReadAll()
        {
            return OpenConnectionAndExecute(() => ReadAllEntities());
        }

        private void OpenConnectionAndExecute(T entity, Action<T> action)
        {
            _sqlConnection.Open();

            action.Invoke(entity);

            _sqlConnection.Close();
        }

        private IEnumerable<T> OpenConnectionAndExecute(Func<IEnumerable<T>> func)
        {
            _sqlConnection.Open();
            var result = func.Invoke();
            _sqlConnection.Close();
            return result;
        }

        private void AddEntity(T entity)
        {
            if (!IsTableExist())
            {
                CreateTable();
            }

            var addCommands = GetAddCommand(typeof(T));
            var complexTypeObjects = GetComplexTypeObjects(entity);
            for (int index = 0; index < addCommands.Count(); index++)
            {
                if (!IsComplexTypeObjectExist(complexTypeObjects.ElementAt(index)))
                {
                    using (var command = new SqlCommand(addCommands.ElementAt(index), _sqlConnection))
                    {
                        command.Parameters.Clear();
                        SetProperiesValues(complexTypeObjects.ElementAt(index), command);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private void DeleteEntity(T entity)
        {
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

        private IEnumerable<T> ReadAllEntities()
        {
            if (!IsTableExist())
            {
                CreateTable();
            }

            var listOfTableValues = new List<T>();
            var selectAllCommand = GetSelectAllCommand(typeof(T));
            using (var command = new SqlCommand(selectAllCommand, _sqlConnection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        var entity = ReadEntityFromDatabase(typeof(T), reader);
                        listOfTableValues.Add(entity);
                    }
                }
            }

            return listOfTableValues;
        }

        private void UpdateEntity(object entity)
        {
            if (!IsTableExist())
            {
                CreateTable();
            }
            
            var properties = entity.GetType().GetProperties().Where(e => e.GetCustomAttribute<ForeignKeyAttribute>() != null);
            foreach (var property in properties)
            {
                UpdateEntity(property.GetValue(entity));
            }

            var updateCommand = GetUpdateCommand(entity);
            using (var sqlCommand = new SqlCommand(updateCommand, _sqlConnection))
            {
                SetProperiesValues(entity, sqlCommand);
                sqlCommand.ExecuteNonQuery();
            }
        }

        private string GetUpdateCommand(object entity)
        {
            var updateCommand = new StringBuilder();
            updateCommand.Append($"UPDATE {entity.GetType().Name} SET ");
            var properties = entity.GetType().GetProperties().Where(e => e.GetCustomAttribute<ForeignKeyAttribute>() == null);
            for (int index = 0; index < properties.Count(); index++)
            {
                if (index < properties.Count() - 1)
                {
                    updateCommand.Append(properties.ElementAt(index).Name + "=@" + properties.ElementAt(index).Name + ", ");
                }
                else
                {
                    updateCommand.Append(properties.ElementAt(index).Name + "=@" + properties.ElementAt(index).Name + " ");
                }
            }
            var idProperty = entity.GetType().GetProperty(entity.GetType().Name + "Id");
            updateCommand.Append($"WHERE {entity.GetType().Name}Id = {idProperty.GetValue(entity)} ");

            return updateCommand.ToString();
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

        public void CreateTable()
        {
            _sqlConnection.Open();

            if (!IsTableExist())
            {
                var commands = GetCreateCommands(typeof(T));
                foreach (var command in commands)
                {
                    using (var sqlCommand = new SqlCommand(command, _sqlConnection))
                    {
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }

            _sqlConnection.Close();
        }

        public void CreateTable(string scriptFilePath)
        {
            _sqlConnection.Open();
            if (!IsTableExist())
            {
                var command = File.ReadAllText(scriptFilePath);
                using (var sqlCommand = new SqlCommand(command, _sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }
            }

            _sqlConnection.Close();
        }

        //----------------------------------------------------------------------------------------------

        private T ReadEntityFromDatabase(Type type, SqlDataReader dataReader)
        {
            var entity = Activator.CreateInstance(type);
            var props = entity.GetType().GetProperties();
            foreach (var property in props)
            {
                if (property.GetCustomAttribute<ForeignKeyAttribute>() != null)
                {
                    property.SetValue(entity, ReadEntityFromDatabase(property.PropertyType, dataReader));
                }
                else
                {
                    var propertyValue = ParseDatabaseValue(property.PropertyType, property.Name, dataReader);
                    property.SetValue(entity, propertyValue);
                }
            }

            return (T)entity;
        }

        private bool IsComplexTypeObjectExist(object entity)
        {
            if (IsTableExist())
            {
                var selectCommand = new StringBuilder();
                var idProperty = entity.GetType().GetProperty($"{entity.GetType().Name}Id");
                selectCommand.Append($"SELECT * FROM {entity.GetType().Name} WHERE {entity.GetType().Name}Id = {idProperty.GetValue(entity)};");
                using (var sqlCommand = new SqlCommand(selectCommand.ToString(), _sqlConnection))
                {
                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            return true;
                        }

                        return false;
                    }
                }
            }

            return false;
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
                return dataReader.GetDateTime(columnName);
            }

            throw new InvalidCastException("Cant set a property with type " + propertyType.DeclaringType.Name);
        }

        private string GetSelectAllCommand(Type type)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(GetSelectCommand(type));
            stringBuilder.Append(";");
            return stringBuilder.ToString();
        }

        private string GetSelectCommand(Type type)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("SELECT ");
            GetPropsNames(type, stringBuilder);

            var complexTypeProps = type.GetProperties().Where(e => e.GetCustomAttribute<ForeignKeyAttribute>() != null);
            if (complexTypeProps.Any())
            {
                stringBuilder.Append("(");
                foreach (var property in complexTypeProps)
                {
                    stringBuilder.Append(GetSelectCommand(property.PropertyType));
                }
                stringBuilder.Append(")");
            }

            stringBuilder.Append(" FROM " + typeof(T).Name);

            return stringBuilder.ToString();
        }

        private void GetPropsNames(Type type, StringBuilder stringBuilder)
        {
            var properties = type.GetProperties();
            for (int index = 0; index < properties.Length; index++)
            {
                if (properties[index].GetCustomAttribute<ForeignKeyAttribute>() == null)
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

        private string GetDeleteCommand(Type type)
        {
            if (type.GetProperties().Where(e => e.Name.ToUpper() == (type.Name + "Id").ToUpper()).Any())
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"DELETE FROM {type.Name} WHERE {type.Name}Id = @{type.Name}Id;");
                return stringBuilder.ToString();
            }

            throw new IdPropertyNotFoundException("Class " + type.Name + " dont contain the " + type.Name + "Id Property");
        }

        private IEnumerable<object> GetComplexTypeObjects(object obj)
        {
            var complexTypeObjects = new List<object>()
            {
                obj
            };
            var innerComplexTypes = obj.GetType().GetProperties().Where(e => e.GetCustomAttribute<ForeignKeyAttribute>() != null);
            foreach (var complexType in innerComplexTypes)
            {
                complexTypeObjects.Add(complexType.GetValue(obj));
                complexTypeObjects.AddRange(GetComplexTypeObjects(complexTypeObjects[^1]));
            }

            return complexTypeObjects;
        }

        private void SetProperiesValues(object entity, SqlCommand sqlCommand)
        {
            var properies = entity.GetType().GetProperties().Where(e => e.GetCustomAttribute<ForeignKeyAttribute>() == null);
            foreach (var property in properies)
            {
                sqlCommand.Parameters.AddWithValue("@" + property.Name, property.GetValue(entity));
            }
        }

        private IEnumerable<string> GetAddCommand(Type type)
        {
            var addCommands = new List<string>();
            var createEntityCommand = GetInsertCommand(type);
            createEntityCommand.Append(GetValuesString(type));
            createEntityCommand.Append(";");
            addCommands.Add(createEntityCommand.ToString());
            var complexTypeProps = type.GetProperties().Where(e => e.GetCustomAttribute<ForeignKeyAttribute>() != null);
            foreach (var complexType in complexTypeProps)
            {
                addCommands.AddRange(GetAddCommand(complexType.PropertyType));
            }

            return addCommands;
        }

        private StringBuilder GetInsertCommand(Type type)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("INSERT " + type.Name + " (");
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
                if (properties[index].GetCustomAttribute<ForeignKeyAttribute>() == null)
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

            stringBuilder.Append(")");
            return stringBuilder.ToString();
        }

        private IEnumerable<string> GetCreateCommands(Type type)
        {
            var stringBuilder = new StringBuilder();
            var commands = new List<string>();
            var properties = type.GetProperties();
            var primitivePropties = properties.Where(e => e.GetCustomAttribute<ForeignKeyAttribute>() == null);
            var complexTypeProps = properties.Except(primitivePropties);
            commands.Add(GetCreateTableCommand(primitivePropties));
            foreach (var property in complexTypeProps)
            {
                commands.AddRange(GetCreateCommands(property.PropertyType));
            }

            return commands;
        }

        private string GetCreateTableCommand(IEnumerable<PropertyInfo> properties)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Create Table " + typeof(T).Name);
            stringBuilder.Append("\n(\n");
            foreach (var property in properties)
            {
                var propertyType = GetSqlTypeOfProperty(property.PropertyType);
                stringBuilder.Append(property.Name + " " + propertyType + ",\n");
            }
            stringBuilder.Append(");");

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
                return "Date";
            }

            throw new InvalidCastException("Cant create a column with type " + propertyType.DeclaringType.Name);
        }

        private bool IsTableExist()
        {
            DataTable dTable = _sqlConnection.GetSchema("TABLES",
                        new string[] { null, null, typeof(T).Name });

            return dTable.Rows.Count > 0;
        }
    }
}
