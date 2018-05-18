using Microsoft.EntityFrameworkCore.Storage;
using Reviewer.DAL.Interfaces;

namespace Reviewer.DAL.Infrastructure
{
    internal sealed class ReviewerDbTransaction : IDbTransaction
    {
        private IDbContextTransaction _transaction;

        internal ReviewerDbTransaction(IDbContextTransaction transaction)
        {
            _transaction = transaction;
        }

        public void Commit() => _transaction.Commit();

        public void Rollback() => _transaction.Rollback();

        public void Dispose() => _transaction.Dispose();
    }
}
