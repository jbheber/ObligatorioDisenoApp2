using Stockapp.Data.Access;
using Stockapp.Data.Extensions;
using Stockapp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Stockapp.Data.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, ISoftDelete, Identificable
    {
        internal Context context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(Context context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Get all non deleted records
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet.GetNonDeleted<TEntity>();

            return PrivateGet(query, filter, orderBy, includeProperties);
        }

        /// <summary>
        /// Get all records from database, including deleted ones.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            return PrivateGet(query, filter, orderBy, includeProperties);
        }

        /// <summary>
        /// Gets all records from query.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        private IEnumerable<TEntity> PrivateGet(
            IQueryable<TEntity> query,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            if (orderBy != null)
                return orderBy(query).ToList();
            else
                return query.ToList();
        }

        public virtual TEntity GetById(object id)
        {
            var searchedEntity = dbSet.GetNonDeleted().Where(e => e.Id == (long)id);
            return searchedEntity.IsNotEmpty() ? searchedEntity.SingleOrDefault() : null;
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = this.GetById(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            entityToDelete.IsDeleted = true;
            Update(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
