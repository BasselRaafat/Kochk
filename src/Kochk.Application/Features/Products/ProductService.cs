using Kochk.Application.Common.Interfaces.Reopsitories;
using Kochk.Application.Common.Interfaces.Services;
using Kochk.Application.Common.Interfaces.UnitOfWork;
using Kochk.Application.Common.Specifications.ProductSpecs;
using Kochk.Domain.Entities;

namespace Kochk.Application.Products;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReadRepository<Product> _productRepo;

    public ProductService(IUnitOfWork unitOfWork, IReadRepository<Product> productRepo)
    {
        _unitOfWork = unitOfWork;
        _productRepo = productRepo;
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecsParam specParams)
    {
        var spec = new ProductWithBrandAndCategorySpecifications(specParams);
        return await _unitOfWork.GetRepo<Product>().GetAllWithSpecAsync(spec);
    }

    public async Task<int> GetCountAsync(ProductSpecsParam specParams)
    {
        var countSpec = new ProductWithFilterAndCountSpecifications(specParams);
        return await _productRepo.GetCountAsync(countSpec);
    }

    public async Task<Product> GetProductAsync(Guid id)
    {
        var spec = new ProductWithBrandAndCategorySpecifications(id);
        return await _unitOfWork.GetRepo<Product>().GetWithSpecAsync(spec);
    }
}
