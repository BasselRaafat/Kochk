using Kochk.Domain.Entities;

namespace Kochk.Application.Common.Specifications.ProductSpecs;

public class ProductWithFilterAndCountSpecifications : BaseSpecifications<Product>
{
    public ProductWithFilterAndCountSpecifications(ProductSpecsParam specsParams)
        : base(P =>
            (!specsParams.CategoryId.HasValue || P.CategoryId == specsParams.CategoryId.Value)
            && (!specsParams.BrandId.HasValue || P.BrandId == specsParams.BrandId.Value)
            && (
                string.IsNullOrEmpty(specsParams.Search)
                || P.Name.Contains(specsParams.Search, StringComparison.InvariantCultureIgnoreCase)
            )
        ) { }
}
