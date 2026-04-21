using System.Linq.Expressions;
using Kochk.Application.Common.Interfaces.Reopsitories;
using Kochk.Application.Common.Interfaces.Specifications;
using Kochk.Domain.Entities;
using Kochk.Infrastructure.Data;
using Kochk.Infrastructure.Evaluator;
using Microsoft.EntityFrameworkCore;

namespace Kochk.Infrastructure.Repositories;

public class ReadRepository<TEntity> : IReadRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly KochkDbContext _dbContext;

    public ReadRepository(KochkDbContext appDbContext)
    {
        _dbContext = appDbContext;
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _dbContext.Set<TEntity>().FindAsync([id], ct);
    }

    public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default)
    {
        return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync(ct);
    }

    public async Task<TEntity?> GetByIdAsync(Guid[] id, CancellationToken ct = default)
    {
        return await _dbContext.Set<TEntity>().FindAsync([.. id], cancellationToken: ct);
    }

    public async Task<TEntity?> GetWithSpecAsync(
        ISpecifications<TEntity> spec,
        CancellationToken ct = default
    )
    {
        return await BuildQuery(spec).FirstOrDefaultAsync(ct);
    }

    public async Task<IReadOnlyList<TEntity>> GetAllWithSpecAsync(
        ISpecifications<TEntity> spec,
        CancellationToken ct
    )
    {
        return await BuildQuery(spec).ToListAsync(ct);
    }

    public async Task<int> GetCountAsync(
        ISpecifications<TEntity> spec,
        CancellationToken ct = default
    )
    {
        return await BuildQuery(spec).CountAsync(ct);
    }

    public async Task<TResult?> GetWithSpecAndProjectionAsync<TResult>(
        ISpecifications<TEntity> spec,
        Expression<Func<TEntity, TResult>> selector,
        CancellationToken ct = default
    )
    {
        return await BuildQueryWithProjection(spec, selector)
            .FirstOrDefaultAsync(cancellationToken: ct);
    }

    public async Task<IReadOnlyList<TResult>> GetAllWithSpecAndProjectionAsync<TResult>(
        ISpecifications<TEntity> spec,
        Expression<Func<TEntity, TResult>> selector,
        CancellationToken ct = default
    )
    {
        return await BuildQueryWithProjection(spec, selector).ToListAsync(cancellationToken: ct);
    }

    public async Task<IReadOnlyList<TResult>> GetAllWithProjection<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        CancellationToken ct
    )
    {
        return await _dbContext
            .Set<TEntity>()
            .AsNoTracking()
            .Select(selector)
            .ToListAsync(cancellationToken: ct);
    }

    public async Task<TResult?> GetByIdWithProject<TResult>(
        Guid id,
        Expression<Func<TEntity, TResult>> selector,
        CancellationToken ct = default
    )
    {
        return await _dbContext
            .Set<TEntity>()
            .Where(e => e.Id == id)
            .Select(selector)
            .FirstOrDefaultAsync(cancellationToken: ct);
    }

    private IQueryable<TEntity> BuildQuery(ISpecifications<TEntity> spec)
    {
        return SpecificationEvaluator<TEntity>.CreateQuery(_dbContext.Set<TEntity>(), spec);
    }

    private IQueryable<TResult> BuildQueryWithProjection<TResult>(
        ISpecifications<TEntity> spec,
        Expression<Func<TEntity, TResult>> selector
    )
    {
        var query = SpecificationEvaluator<TEntity>.CreateQuery(_dbContext.Set<TEntity>(), spec);
        return SpecificationEvaluator<TEntity>.Project(query, selector);
    }

    public async Task<bool> ExistAsync(Guid id)
    {
        return await _dbContext.Set<TEntity>().AnyAsync(entity => entity.Id == id);
    }
}
