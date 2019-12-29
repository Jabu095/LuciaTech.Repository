using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuciaTech.Repository
{
    public interface IGenericRepository<T>
    {
        Task<T> GetAsync(int id);
        Task<T> GetAsync(string id);
        Task<ICollection<T>> GetByIdsAsync(ICollection<int> ids);
        Task<ICollection<T>> GetByIdsAsync(ICollection<string> ids);
        IQueryable<T> Query();
        Task<T> InsertAsync(T entity);
        Task<ICollection<T>> InsertRangeAsync(ICollection<T> entity);
        Task<T> UpdateAsync(T entity);
        Task<ICollection<T>> UpdateRangeAsync(ICollection<T> entities);
        Task<int> DeleteAsync(T entity);
        Task<int> DeleteRangeAsync(ICollection<T> entities);
        Task<int> DeleteByIdsRangeAsync(ICollection<int> ids);
        Task<int> DeleteByIdsRangeAsync(ICollection<string> ids);
    }
}
