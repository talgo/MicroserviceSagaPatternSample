using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Common.Core
{
    public class BaseGenericRepository<TEntity> : IBaseGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext dbContext;
        protected DbSet<TEntity> entity => dbContext.Set<TEntity>();
        public BaseGenericRepository(DbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
        }

        public virtual async Task<int> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await this.entity.AddAsync(entity, cancellationToken);
            return await dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            this.entity.Update(entity);
            return await dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<TEntity> FindAsync(int id)
        {
            return await entity.FindAsync(id);
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await entity.FirstOrDefaultAsync(expression);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
        {
            return await entity.AnyAsync(expression, cancellationToken);
        }
    }
}
