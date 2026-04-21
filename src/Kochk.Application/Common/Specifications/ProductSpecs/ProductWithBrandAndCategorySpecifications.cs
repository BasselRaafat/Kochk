using Kochk.Domain.Entities;

namespace Kochk.Application.Common.Specifications.ProductSpecs;

public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
{
    public ProductWithBrandAndCategorySpecifications(
        ProductSpecsParam specsParams,
        bool isCount = false
    )
        : base(P =>
            (!specsParams.CategoryId.HasValue || P.CategoryId == specsParams.CategoryId.Value)
            && (!specsParams.BrandId.HasValue || P.BrandId == specsParams.BrandId.Value)
            && (
                string.IsNullOrEmpty(specsParams.Search)
                || P.Name.Contains(specsParams.Search, StringComparison.CurrentCultureIgnoreCase)
            )
        )
    {
        Includes.Add(P => P.Brand);
        Includes.Add(P => P.Category);
        if (!string.IsNullOrEmpty(specsParams.OrderBy))
        {
            switch (specsParams.OrderBy)
            {
                case "nameDec":
                    AddOrderByDec(P => P.Name);
                    break;
                case "priceAsc":
                    AddOrderBy(P => P.Price);
                    break;
                case "priceDec":
                    AddOrderByDec(P => P.Price);
                    break;
                default:
                    AddOrderBy(P => P.Name);
                    break;
            }
        }
        else
        {
            AddOrderBy(P => P.Name);
        }

        ApplyPagination((specsParams.PageIndex - 1) * specsParams.PageSize, specsParams.PageSize);
    }

    public ProductWithBrandAndCategorySpecifications(Guid id)
        : base(P => P.Id == id)
    {
        Includes.Add(P => P.Brand);
        Includes.Add(P => P.Category);
    }
}
