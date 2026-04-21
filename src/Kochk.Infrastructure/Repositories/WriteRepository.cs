using Kochk.Application.Common.Interfaces.Reopsitories;
using Kochk.Application.Common.Interfaces.Specifications;
using Kochk.Domain.Entities;
using Kochk.Infrastructure.Data;
using Kochk.Infrastructure.Evaluator;
using Microsoft.EntityFrameworkCore;

namespace Kochk.Infrastructure.Repositories;

public class WriteRepository<TEntity> : IWriteRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly KochkDbContext _dbContext;

    public WriteRepository(KochkDbContext appDbContext)
    {
        _dbContext = appDbContext;
    }

    public async Task CreateAsync(TEntity entity, CancellationToken ct = default)
    {
        await _dbContext.AddAsync(entity, ct);
    }

    public void Update(TEntity entity)
    {
        _dbContext.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbContext.Remove(entity);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _dbContext.Set<TEntity>().FindAsync([id], cancellationToken: ct);
    }

    public async Task<TEntity?> GetByIdsAsync(Guid[] id, CancellationToken ct = default)
    {
        return await _dbContext.Set<TEntity>().FindAsync([.. id], cancellationToken: ct);
    }

    public async Task<TEntity?> GetWithSpecAsync(
        ISpecifications<TEntity> spec,
        CancellationToken ct = default
    )
    {
        return await BuildQuery(spec).FirstOrDefaultAsync(cancellationToken: ct);
    }

    public async Task<IReadOnlyList<TEntity>> GetAllWithSpecAsync(
        ISpecifications<TEntity> spec,
        CancellationToken ct = default
    )
    {
        return await BuildQuery(spec).ToListAsync(cancellationToken: ct);
    }

    private IQueryable<TEntity> BuildQuery(ISpecifications<TEntity> spec)
    {
        return SpecificationEvaluator<TEntity>.CreateQuery(_dbContext.Set<TEntity>(), spec);
    }

    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        _dbContext.UpdateRange(entities);
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        _dbContext.RemoveRange(entities);
    }

    public async Task CreateRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken ct = default
    )
    {
        await _dbContext.AddRangeAsync(entities, ct);
    }

    public async Task<bool> ExistAsync(Guid id)
    {
        return await _dbContext.Set<TEntity>().AnyAsync(entity => entity.Id == id);
    }
}
