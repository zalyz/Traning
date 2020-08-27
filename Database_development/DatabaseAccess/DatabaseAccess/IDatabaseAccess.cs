using System.Collections.Generic;

namespace DatabaseAccess
{
    public interface IDatabaseAccess<T>
        where T : class
    {
        public void Add(T entity);

        public void Update(T entity);

        public IEnumerable<T> ReadAll();

        public void Delete(T entity);
    }
}
