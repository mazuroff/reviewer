using System;
using System.Collections.Generic;
using System.Text;

namespace Reviewer.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>()
            where T : class;

        void SaveChanges();

        IDbTransaction BeginTransaction();
    }
}
