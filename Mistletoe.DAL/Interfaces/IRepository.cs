// <copyright file="IRepository.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.DAL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    ///     Repository Interface
    /// </summary>
    /// <typeparam name="TEntity">Entity class</typeparam>
    public interface IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        ///     Get item
        /// </summary>
        /// <param name="filter">Filter rule</param>
        /// <param name="orderBy">Order By rule</param>
        /// <param name="includeProperties">Include properties</param>
        /// <returns>Item satisfying rules</returns>
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        /// <summary>
        ///     Get item by ID
        /// </summary>
        /// <param name="id">Item ID</param>
        /// <returns>Item with ID</returns>
        TEntity GetByID(object id);

        /// <summary>
        ///     Insert Item
        /// </summary>
        /// <param name="entity">Item to be inserted</param>
        void Insert(TEntity entity);

        /// <summary>
        ///     Delete item
        /// </summary>
        /// <param name="id">Item ID</param>
        void Delete(object id);

        /// <summary>
        ///     Delete item
        /// </summary>
        /// <param name="entityToDelete">Item to delete</param>
        void Delete(TEntity entityToDelete);

        /// <summary>
        ///     Update item
        /// </summary>
        /// <param name="entityToUpdate">Item to update</param>
        void Update(TEntity entityToUpdate);
    }
}
