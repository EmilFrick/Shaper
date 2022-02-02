using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.DataAccess.Repo.IRepo
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T,bool>> filter, string? includeProperties = null);
        Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null);
        Task AddAsync(T entity);
        void RemoveAsync(T entity);
        void RemoveRangeAsync(IEnumerable<T> entity);
    }
}
