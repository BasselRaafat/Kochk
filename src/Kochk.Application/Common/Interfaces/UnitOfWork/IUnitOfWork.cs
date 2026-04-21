using Kochk.Application.Common.Interfaces.Reopsitories;
using Kochk.Domain.Entities;

namespace Kochk.Application.Common.Interfaces.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    IWriteRepository<TEntity> GetRepo<TEntity>()
        where TEntity : BaseEntity;
    Task<int> SaveChangesAsync();
}
