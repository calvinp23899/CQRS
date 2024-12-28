using Ecommerce.Core.Models;
using System.Linq.Expressions;

namespace Ecommerce.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IQueryable<T>> GetAllAsync(bool? isGetAll = false);
        Task<IQueryable<T>> SearchAsync(Expression<Func<T, bool>>? filter = null, bool? isGetAll = false);
        Task<T?> GetByIdAsync(int id, List<string>? relatedProperties = null, bool? isGetAll = false);
        Task<T> InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task SoftDeletedAsync(int id);
        Task HardDeleteAsync(int id);
        Task HardDeleteAllAsync();
        Task SaveAsync();
    }
}
