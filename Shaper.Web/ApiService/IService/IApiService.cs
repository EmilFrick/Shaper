using System.Linq.Expressions;

namespace Shaper.Web.ApiService.IService
{
    public interface IApiService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(string url, string token = "");
        Task<T> GetFirstOrDefaultAsync(int id, string url, string token = "");
        Task AddAsync(T entity, string url, string token = "");
        Task Update(int id, T entity, string url, string token = "");
        void Remove(T entity, string url, string token = "");
    }
}
