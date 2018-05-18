using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Reviewer.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Set { get; }

        T Insert(T entity);

        IEnumerable<T> InsertRange(IEnumerable<T> entities);

        T Update(T entity);

        void Delete(T entity);

        void DeleteRange(IEnumerable<T> entities);

        IQueryable<T> Include(params Expression<Func<T, object>>[] include);
    }
}
