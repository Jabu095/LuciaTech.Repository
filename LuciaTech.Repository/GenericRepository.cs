using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuciaTech.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
        where T : class, new()
    {
        protected DbContext DbContext { get; set; }
        public GenericRepository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<T> GetAsync(int id)
        {
            return await DbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetAsync(string id)
        {
            return await DbContext.Set<T>().FindAsync(id);
        }

        public async Task<ICollection<T>> GetByIdsAsync(ICollection<int> ids)
        {
            var entities = new List<T>();
            foreach (var id in ids)
            {
                entities.Add(await GetAsync(id));
            }

            return entities;
        }

        public async Task<ICollection<T>> GetByIdsAsync(ICollection<string> ids)
        {
            var entities = new List<T>();
            foreach (var id in ids)
            {
                entities.Add(await GetAsync(id));
            }

            return entities;
        }

        public IQueryable<T> Query()
        {
            return DbContext.Set<T>().AsQueryable();
        }

        public async Task<T> InsertAsync(T entity)
        {
            await DbContext.Set<T>().AddAsync(entity);
            var result = await DbContext.SaveChangesAsync();
            return result > 0 ? entity : null;
        }

        public async Task<ICollection<T>> InsertRangeAsync(ICollection<T> entity)
        {
            await DbContext.Set<T>().AddRangeAsync(entity);
            var result = await DbContext.SaveChangesAsync();
            return result > 0 ? entity : null;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            var result = await DbContext.SaveChangesAsync();
            return result > 0 ? entity : null;
        }

        public async Task<ICollection<T>> UpdateRangeAsync(ICollection<T> entities)
        {
            DbContext.Set<T>().UpdateRange(entities);
            var result = await DbContext.SaveChangesAsync();
            return result > 0 ? entities : null;
        }

        public async Task<int> DeleteAsync(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            var result = await DbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteRangeAsync(ICollection<T> entities)
        {
            DbContext.Set<T>().RemoveRange(entities);
            var result = await DbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteByIdsRangeAsync(ICollection<int> ids)
        {
            var entities = new List<T>();
            foreach (var id in ids)
            {
                entities.Add(GetAsync(id).Result);
            }
            var result = await DeleteRangeAsync(entities);

            return result;
        }

        public async Task<int> DeleteByIdsRangeAsync(ICollection<string> ids)
        {
            var entities = new List<T>();
            foreach (var id in ids)
            {
                entities.Add(GetAsync(id).Result);
            }
            var result = await DeleteRangeAsync(entities);

            return result;
        }
    }
}
