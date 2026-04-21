using System.Collections;
using Kochk.Application.Common.Interfaces.Reopsitories;
using Kochk.Application.Common.Interfaces.UnitOfWork;
using Kochk.Domain.Entities;
using Kochk.Infrastructure.Data;
using Kochk.Infrastructure.Repositories;

namespace Kochk.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly KochkDbContext _dbContext;
    private Hashtable _repos { get; set; }

    public UnitOfWork(KochkDbContext appDbContext)
    {
        _dbContext = appDbContext;
        _repos = [];
    }

    public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();

    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    public IWriteRepository<TEntity> GetRepo<TEntity>()
        where TEntity : BaseEntity
    {
        var key = typeof(TEntity).Name;
        if (!_repos.ContainsKey(key))
        {
            var repo = new WriteRepository<TEntity>(_dbContext);
            _repos.Add(key, repo);
        }
        return (_repos[key] as IWriteRepository<TEntity>)!;
    }
}
