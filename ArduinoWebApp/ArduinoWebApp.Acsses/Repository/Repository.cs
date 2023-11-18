﻿using ArduinoWebApp.Acsses.Data;
using ArduinoWebApp.Acsses.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ArduinoWebApp.Acsses.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region dependency injection
        private readonly ArduinoAppDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ArduinoAppDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        #endregion

        #region Add
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
        #endregion

        #region Get
        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query;
            if (tracked)
            {
                query = dbSet;
            }
            else
            {
                query = dbSet.AsNoTracking();
            }

            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.FirstOrDefault();
        }
        #endregion

        #region GetAll
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.ToList();
        }
        #endregion

        #region Remove
        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
        #endregion

        #region RemoveRange
        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
        #endregion

        #region AddRange
        public void AddRange(IEnumerable<T> entities)
        {
            dbSet.AddRange(entities);
        }
        #endregion
    }
}
