using Kochk.Application.Common.Specifications.ProductSpecs;
using Kochk.Domain.Entities;

namespace Kochk.Application.Common.Interfaces.Services;

public interface IProductService
{
    public Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecsParam specParam);
    public Task<Product> GetProductAsync(Guid id);
    public Task<int> GetCountAsync(ProductSpecsParam productSpecs);
}
