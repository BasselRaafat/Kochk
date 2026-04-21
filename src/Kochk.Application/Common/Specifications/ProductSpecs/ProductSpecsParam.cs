namespace Kochk.Application.Common.Specifications.ProductSpecs;

public class ProductSpecsParam
{
    public int? Id { get; set; }
    public string? OrderBy { set; get; }
    public Guid? CategoryId { set; get; }
    public Guid? BrandId { set; get; }
    public int PageSize
    {
        set => pageSize = value > maxPageSize ? maxPageSize : value;
        get => pageSize;
    }
    public int PageIndex { set; get; } = 1;
    private int pageSize = 5;
    private const int maxPageSize = 10;

    public string? Search { set; get; }
}
