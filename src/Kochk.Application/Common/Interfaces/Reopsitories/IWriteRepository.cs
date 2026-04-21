using Kochk.Application.Common.Interfaces.Specifications;
using Kochk.Domain.Entities;

namespace Kochk.Application.Common.Interfaces.Reopsitories;

public interface IWriteRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<TEntity>> GetAllWithSpecAsync(
        ISpecifications<TEntity> spec,
        CancellationToken ct = default
    );
    Task<TEntity?> GetWithSpecAsync(ISpecifications<TEntity> spec, CancellationToken ct = default);
    Task CreateAsync(TEntity entity, CancellationToken ct = default);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    void DeleteRange(IEnumerable<TEntity> entities);
    void UpdateRange(IEnumerable<TEntity> entities);
    Task CreateRangeAsync(IEnumerable<TEntity> entities, CancellationToken ct = default);
    Task<TEntity?> GetByIdsAsync(Guid[] id, CancellationToken ct = default);
}
