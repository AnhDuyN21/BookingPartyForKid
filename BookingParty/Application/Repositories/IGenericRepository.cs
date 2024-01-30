using Application.Commons;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, string? includeProperties = null);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>>? filter = null, string? includeProperties = null);
        Task<TEntity?> GetByIdAsync(int id);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void UpdateRange(List<TEntity> entities);
        void SoftRemove(TEntity entity);
        Task AddRangeAsync(List<TEntity> entities);
        void SoftRemoveRange(List<TEntity> entities);
        void DeleteRange(List<TEntity> entities);

        Task<Pagination<TEntity>> ToPagination(int pageNumber = 0, int pageSize = 10);
    }
}
