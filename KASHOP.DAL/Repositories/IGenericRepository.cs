using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task CreateAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task RemoveAsync(T entity, CancellationToken cancellationToken = default);
        Task<List<T>> GetAsync(
            bool track = false,
            Expression<Func<T, bool>> filter = null, List<Expression<Func<T,
            object>>> includes = null,
            CancellationToken cancellationToken = default);
        Task<T?> GetOneAsync(
            bool track = false,
            Expression<Func<T, bool>> filter = null, List<Expression<Func<T,
            object>>> includes = null,
            CancellationToken cancellationToken = default);

    }


}
