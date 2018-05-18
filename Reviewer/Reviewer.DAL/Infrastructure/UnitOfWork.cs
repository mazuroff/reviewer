using Microsoft.EntityFrameworkCore.Storage;
using Reviewer.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reviewer.DAL.Infrastructure
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ReviewerDbContext _dbContext;
        private bool _disposed;
        private Dictionary<string, object> _repositories;

        public UnitOfWork(ReviewerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<T> Repository<T>()
            where T : class
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _dbContext);
                _repositories.Add(type, repositoryInstance);
            }
            return (Repository<T>)_repositories[type];
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public IDbTransaction BeginTransaction()
        {
            IDbContextTransaction transaction = _dbContext.Database.BeginTransaction();
            return new ReviewerDbTransaction(transaction);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
