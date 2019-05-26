// <copyright file="GenericRepository.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using Mistletoe.DAL.Interfaces;
    using Mistletoe.Entities;

    /// <summary>
    ///     Repository Implementation
    /// </summary>
    /// <typeparam name="TEntity">Entity class</typeparam>
    public class GenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        ///     Database context
        /// </summary>
        private MistletoeDataEntities context;

        /// <summary>
        ///     Database set
        /// </summary>
        private DbSet<TEntity> dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">Database context</param>
        public GenericRepository(MistletoeDataEntities context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        /// <inheritdoc/>
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = this.dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(
                new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        /// <inheritdoc/>
        public virtual TEntity GetByID(object id)
        {
            return this.dbSet.Find(id);
        }

        /// <inheritdoc/>
        public virtual void Insert(TEntity entity)
        {
            this.dbSet.Add(entity);
        }

        /// <inheritdoc/>
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = this.dbSet.Find(id);
            this.Delete(entityToDelete);
        }

        /// <inheritdoc/>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (this.context.Entry(entityToDelete).State == EntityState.Detached)
            {
                this.dbSet.Attach(entityToDelete);
            }

            this.dbSet.Remove(entityToDelete);
        }

        /// <inheritdoc/>
        public virtual void Update(TEntity entityToUpdate)
        {
            this.dbSet.Attach(entityToUpdate);
            this.context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
