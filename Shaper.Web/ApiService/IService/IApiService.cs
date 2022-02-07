using System.Linq.Expressions;

namespace Shaper.Web.ApiService.IService
{
    public interface IApiService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(string url, string token = "");
        Task<T> GetFirstOrDefaultAsync(string url, string token = "");
        Task<bool> AddAsync(T entity, string url, string token = "");
        Task<bool> UpdateAsync(T entity, string url, string token = "");
        Task<bool> RemoveAsync(string url, string token = "");
    }
}
