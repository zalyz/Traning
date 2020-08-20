using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess
{
    public interface IDatabaseAccess<T>
        where T : class
    {
        public void CreateEntity(T entity);

        public void Update(T entityToReplace, T substituteEntity);

        public IEnumerable<T> ReadAll();

        public void Delete(T entity);

        public void CreateTable();
    }
}
