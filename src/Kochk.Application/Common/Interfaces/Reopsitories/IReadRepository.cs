using System.Linq.Expressions;
using Kochk.Application.Common.Interfaces.Specifications;

namespace Kochk.Application.Common.Interfaces.Reopsitories;

public interface IReadRepository<TEntity>
    where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid[] id, CancellationToken ct = default);
    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<TResult>> GetAllWithProjection<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        CancellationToken ct = default
    );
    Task<IReadOnlyList<TResult>> GetAllWithSpecAndProjectionAsync<TResult>(
        ISpecifications<TEntity> spec,
        Expression<Func<TEntity, TResult>> selector,
        CancellationToken ct = default
    );
    Task<IReadOnlyList<TEntity>> GetAllWithSpecAsync(
        ISpecifications<TEntity> spec,
        CancellationToken ct = default
    );
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<TResult?> GetByIdWithProject<TResult>(
        Guid id,
        Expression<Func<TEntity, TResult>> selector,
        CancellationToken ct = default
    );
    Task<bool> ExistAsync(Guid id);
    Task<int> GetCountAsync(ISpecifications<TEntity> spec, CancellationToken ct = default);
    Task<TEntity?> GetWithSpecAsync(ISpecifications<TEntity> spec, CancellationToken ct = default);
    Task<TResult?> GetWithSpecAndProjectionAsync<TResult>(
        ISpecifications<TEntity> spec,
        Expression<Func<TEntity, TResult>> selector,
        CancellationToken ct = default
    );
}
