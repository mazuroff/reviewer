using Microsoft.EntityFrameworkCore;
using Reviewer.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Reviewer.DAL.Infrastructure
{
    public sealed class Repository<T> : IRepository<T>
           where T : class
    {
        private readonly ReviewerDbContext _dbContext;
        private readonly DbSet<T> _entities;

        public Repository(ReviewerDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<T>();
        }

        public IQueryable<T> Set { get { return _entities; } }

        public T GetById(object id)
        {
            return this._entities.Find(id);
        }

        public T Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _entities.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public IEnumerable<T> InsertRange(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            _entities.AddRange(entities);
            _dbContext.SaveChanges();
            return entities;
        }

        public T Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _entities.Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _entities.Remove(entity);
            _dbContext.SaveChanges();
        }

        public void DeleteById(Guid id)
        {
            T entity = this._entities.Find(id);

            if (entity != null)
            {
                _entities.Remove(entity);
                _dbContext.SaveChanges();
            }
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            _entities.RemoveRange(entities);
            _dbContext.SaveChanges();
        }

        public IQueryable<T> Include(params Expression<Func<T, object>>[] include)
        {
            return include.Aggregate((IQueryable<T>)_entities, (current, inc) => current.Include(inc));
        }
    }
}
