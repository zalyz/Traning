using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DatabaseAccess.Attributes;
using DatabaseAccess.DatabaseExceptions;

namespace DatabaseAccess
{
    /// <summary>
    /// Allows to access the database.
    /// </summary>
    /// <typeparam name="T">Class to manipulate.</typeparam>
    public class DatabaseAccess<T> : IDatabaseAccess<T>, IDisposable
        where T : class, new ()
    {
        /// <summary>
        /// Instance of database connection.
        /// </summary>
        private SqlConnection _sqlConnection;

        /// <summary>
        /// Instance of DatabaseAccess for singleton.
        /// </summary>
        private static DatabaseAccess<T> _databaseAccess;

        /// <summary>
        /// Creates instance of DatabaseAccess class.
        /// </summary>
        /// <param name="connectionString">Connection string to database.</param>
        private DatabaseAccess(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
        }

        /// <inheritdoc/>
        public void Add(T entity)
        {
            OpenConnectionAndExecute(entity, (entity) => AddEntity(entity));
        }

        /// <inheritdoc/>
        public void Delete(T entity)
        {
            OpenConnectionAndExecute(entity, (entity) => DeleteEntity(entity));
        }

        /// <inheritdoc/>
        public void Update(T entity)
        {
            OpenConnectionAndExecute(entity, (entity) => UpdateEntity(entity));
        }

        /// <inheritdoc/>
        public IEnumerable<T> ReadAll()
        {
            return OpenConnectionAndExecute(() => ReadAllEntities());
        }

        /// <summary>
        /// Opens connection to the database and execute action with it.
        /// </summary>
        /// <param name="entity">Entity for action.</param>
        /// <param name="action">ACtion with database.</param>
        private void OpenConnectionAndExecute(T entity, Action<T> action)
        {
            _sqlConnection.Open();

            action.Invoke(entity);

            _sqlConnection.Close();
        }

        /// <summary>
        /// Opens connection to the database and execute action with it.
        /// </summary>
        /// <param name="func">Function for Executing</param>
        /// <returns>Collection of function results.</returns>
        private IEnumerable<T> OpenConnectionAndExecute(Func<IEnumerable<T>> func)
        {
            _sqlConnection.Open();
            var result = func.Invoke();
            _sqlConnection.Close();
            return result;
        }

        /// <summary>
        /// Adds entity to database.
        /// </summary>
        /// <param name="entity">Entity for adding.</param>
        private void AddEntity(T entity)
        {
            if (!IsTableExist())
            {
                CreateTable();
            }

            var addCommands = GetAddCommand(typeof(T));
            var complexTypeObjects = GetComplexTypeObjects(entity);
            for (int index = addCommands.Count() - 1; index >= 0; index--)
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

        /// <summary>
        /// Deletes entity from database.
        /// </summary>
        /// <param name="entity">Entity for removing.</param>
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

        /// <summary>
        /// Reads all entitys from database.
        /// </summary>
        /// <returns>Collection of entitys.</returns>
        private IEnumerable<T> ReadAllEntities()
        {
            if (!IsTableExist())
            {
                throw new TableNotFoundException("Table " + typeof(T).Name + "is not found at " + _sqlConnection.Database);
            }

            var listOfTableValues = new List<T>();
            var selectAllCommand = GetSelectAllCommand(typeof(T));
            using (var command = new SqlCommand(selectAllCommand, _sqlConnection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        var entity = (T)ReadEntityFromDatabase(typeof(T), reader);
                        listOfTableValues.Add(entity);
                    }
                }
            }

            return listOfTableValues;
        }

        /// <summary>
        /// Updates entitu in database.
        /// </summary>
        /// <param name="entity">Entity for update.</param>
        private void UpdateEntity(object entity)
        {
            if (!IsTableExist())
            {
                throw new TableNotFoundException("Table " + entity.GetType().Name + "is not found at " + _sqlConnection.Database);
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

        /// <summary>
        /// Gets update command string.
        /// </summary>
        /// <param name="entity">Entity for update.</param>
        /// <returns>Command string.</returns>
        private string GetUpdateCommand(object entity)
        {
            var updateCommand = new StringBuilder();
            var entityType = entity.GetType();
            updateCommand.Append($"UPDATE {entityType.Name} SET ");
            var properties = entityType.GetProperties().Where(e => e.GetCustomAttribute<ForeignKeyAttribute>() == null).ToList();
            var idProperty = entityType.GetProperty($"{entityType.Name}Id");
            properties.RemoveAt(properties.IndexOf(idProperty));
            
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
            updateCommand.Append($"WHERE {entityType.Name}Id = {idProperty.GetValue(entity)} ");

            return updateCommand.ToString();
        }

        /// <summary>
        /// Returnes instance of Databaseaccess class.
        /// </summary>
        /// <param name="connectionString">Connection string to the database.</param>
        /// <returns>Instance of DatabaseAccess class.</returns>
        public static DatabaseAccess<T> Factory(string connectionString)
        {
            if (_databaseAccess == null)
            {
                _databaseAccess = new DatabaseAccess<T>(connectionString);
            }

            return _databaseAccess;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _sqlConnection.Dispose();
        }

        /// <summary>
        /// Creates table in database for entity.
        /// </summary>
        private void CreateTable()
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

        /// <summary>
        /// Reads entity from database.
        /// </summary>
        /// <param name="type">Type of entity for read.</param>
        /// <param name="dataReader">Data reader.</param>
        /// <returns>Entity from the database.</returns>
        private object ReadEntityFromDatabase(Type type, SqlDataReader dataReader)
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

            return entity;
        }

        /// <summary>
        /// Checks whether the table is in the database.
        /// </summary>
        /// <param name="entity">Entity for checking.</param>
        /// <returns>True if entity exist, False otherwise.</returns>
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

        /// <summary>
        /// Translates database types into premitive types.
        /// </summary>
        /// <param name="propertyType">Type of property.</param>
        /// <param name="columnName">Column name for reading.</param>
        /// <param name="dataReader">Data reader.</param>
        /// <returns>Translated value from database.</returns>
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

        /// <summary>
        /// Gets command for read all entities.
        /// </summary>
        /// <param name="type">Entity type.</param>
        /// <returns>Select all command.</returns>
        private string GetSelectAllCommand(Type type)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(GetSelectCommand(type));
            stringBuilder.Append(";");
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Gets select command.
        /// </summary>
        /// <param name="type">Type of entity.</param>
        /// <returns>Select command string.</returns>
        private string GetSelectCommand(Type type)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("SELECT * FROM " + type.Name + " ");
            GetJoinCommand(type, stringBuilder);

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Gets join string for getting field values from database.
        /// </summary>
        /// <param name="type">Type of object for getting fields values from database..</param>
        /// <param name="stringBuilder">StringBuilder for appending join command.</param>
        private static void GetJoinCommand(Type type, StringBuilder stringBuilder)
        {
            var complexTypeProps = type.GetProperties().Where(e => e.GetCustomAttribute<ForeignKeyAttribute>() != null).ToList();
            if (complexTypeProps.Any())
            {
                for (int index = 0; index < complexTypeProps.Count; index++)
                {
                    var foreignKey = complexTypeProps[index].GetCustomAttribute<ForeignKeyAttribute>().KeyName;
                    stringBuilder.Append($"JOIN {complexTypeProps[index].PropertyType.Name} ON {type.Name}.{foreignKey} = {complexTypeProps[index].PropertyType.Name}.{complexTypeProps[index].PropertyType.Name}Id ");
                    GetJoinCommand(complexTypeProps[index].PropertyType, stringBuilder);
                }
            }
        }

        /// <summary>
        /// Gets all Property names.
        /// </summary>
        /// <param name="type">Type for getting names.</param>
        /// <param name="stringBuilder">String for appending names.</param>
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

        /// <summary>
        /// Gets delete commmand.
        /// </summary>
        /// <param name="type">Type for removing.</param>
        /// <returns>Delete command line.</returns>
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

        /// <summary>
        /// Gets collection of inner complex type objects.
        /// </summary>
        /// <param name="obj">Owner of inner complex type.</param>
        /// <returns>Collection of complex type objects.</returns>
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
                GetComplexTypeObjects(complexTypeObjects[^1]);
            }

            return complexTypeObjects;
        }

        /// <summary>
        /// Sets property values for adding entity to table.
        /// </summary>
        /// <param name="entity">Entity for adding.</param>
        /// <param name="sqlCommand">Command for executing.</param>
        private void SetProperiesValues(object entity, SqlCommand sqlCommand)
        {
            var properies = entity.GetType().GetProperties().Where(e => e.GetCustomAttribute<ForeignKeyAttribute>() == null);
            foreach (var property in properies)
            {
                sqlCommand.Parameters.AddWithValue("@" + property.Name, property.GetValue(entity));
            }
        }

        /// <summary>
        /// Gets command for adding.
        /// </summary>
        /// <param name="type">Type to add.</param>
        /// <returns>Collection of add command strings.</returns>
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

        /// <summary>
        /// Gets insert command.
        /// </summary>
        /// <param name="type">Type to insert.</param>
        /// <returns>Insert command line.</returns>
        private StringBuilder GetInsertCommand(Type type)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"SET IDENTITY_INSERT {type.Name} ON\n");
            stringBuilder.Append("INSERT " + type.Name + " (");
            GetPropsNames(type, stringBuilder);
            stringBuilder.Append(") ");
            return stringBuilder;
        }

        /// <summary>
        /// Gets values string for adding entity.
        /// </summary>
        /// <param name="type">Entity to add.</param>
        /// <returns>Values string.</returns>
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

        /// <summary>
        /// Gets collection of create table commands.
        /// </summary>
        /// <param name="type">Type to add.</param>
        /// <returns>Collection of commands.</returns>
        private IEnumerable<string> GetCreateCommands(Type type)
        {
            var stringBuilder = new StringBuilder();
            var commands = new List<string>();
            var properties = type.GetProperties();
            var primitivePropties = properties.Where(e => e.GetCustomAttribute<ForeignKeyAttribute>() == null);
            var complexTypeProps = properties.Except(primitivePropties);
            commands.Add(GetCreateTableCommand(type.Name, primitivePropties));
            foreach (var property in complexTypeProps)
            {
                commands.AddRange(GetCreateCommands(property.PropertyType));
            }

            return commands;
        }

        /// <summary>
        /// Gets create table command.
        /// </summary>
        /// <param name="tableName">Table name.</param>
        /// <param name="properties">Table columns name.</param>
        /// <returns>Create command line.</returns>
        private string GetCreateTableCommand(string tableName, IEnumerable<PropertyInfo> properties)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Create Table " + tableName);
            stringBuilder.Append("\n(\n");
            foreach (var property in properties)
            {
                var propertyType = GetSqlTypeOfProperty(property.PropertyType);
                stringBuilder.Append(property.Name + " " + propertyType + ",\n");
            }
            stringBuilder.Append(");");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Gets sql type for primitive type.
        /// </summary>
        /// <param name="propertyType">Primitive property type.</param>
        /// <returns>Sql type.</returns>
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

        /// <summary>
        /// Checks is table exist.
        /// </summary>
        /// <returns>True if table exist, False otherwise.</returns>
        private bool IsTableExist()
        {
            DataTable dTable = _sqlConnection.GetSchema("TABLES",
                        new string[] { null, null, typeof(T).Name });

            return dTable.Rows.Count > 0;
        }
    }
}
