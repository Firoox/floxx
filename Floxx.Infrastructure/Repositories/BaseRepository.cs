using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Floxx.Core.Entities;
using Floxx.Infrastructure.Contexts;
using Floxx.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Floxx.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly FloxxContext dbContext;

        public BaseRepository(FloxxContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<T> Entities => dbContext.Set<T>();

        public virtual async Task<T> AddAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<List<T>> ListAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<List<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public virtual Task UpdateAsync(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;

            return dbContext.SaveChangesAsync();
        }

        public virtual Task DeleteAsync(T entity)
        {
            dbContext.Set<T>().Remove(entity);

            return dbContext.SaveChangesAsync();
        }
    }
}
