﻿using HotelListing.Data;
using HotelListing.IRepository;
using HotelListing.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X.PagedList;

namespace HotelListing.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DatabaseContext _dbConetext;
        private readonly DbSet<T> _db;

        public GenericRepository(DatabaseContext dbContext)
        {
            _dbConetext = dbContext;
            _db = _dbConetext.Set<T>();
        }

        public async Task Delete(int id)
        {
            var entity = await _db.FindAsync(id);
            if (entity != null)
            {
                _db.Remove(entity);
                // _dbConetext.SaveChanges();
            }
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> query = _db;

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = _db;

            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IPagedList<T>> GetAll(RequestParams requestParams, List<string> includes = null)
        {
            IQueryable<T> query = _db;

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AsNoTracking().ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
        }

        public async Task Insert(T entity)
        {
            await _db.AddAsync(entity);
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
            await _db.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _db.Attach(entity);
            _dbConetext.Entry(entity).State = EntityState.Modified;
        }
    }
}