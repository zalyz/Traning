using System.Collections.Generic;

namespace DatabaseAccess
{
    /// <summary>
    /// Defines methods for managing entities in the database.
    /// </summary>
    /// <typeparam name="T">Class that represent database entity model.</typeparam>
    public interface IDatabaseAccess<T>
        where T : class
    {
        /// <summary>
        /// Adds entity to the database.
        /// </summary>
        /// <param name="entity">Entity for adding.</param>
        public void Add(T entity);

        /// <summary>
        /// Updates entity in the database.
        /// </summary>
        /// <param name="entity">Entity for updating.</param>
        public void Update(T entity);

        /// <summary>
        /// Reads all entitys from database.
        /// </summary>
        /// <returns>Collection of the entitys.</returns>
        public IEnumerable<T> ReadAll();

        /// <summary>
        /// Removes entity from the database.
        /// </summary>
        /// <param name="entity">Entity for removing.</param>
        public void Delete(T entity);
    }
}
