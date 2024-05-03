using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Common.Core
{
    public interface IBaseGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> FindAsync(int id);
        Task<int> AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression);
        Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    }
}
