using System.Text.Json;
using Kochk.Domain.Entities;
using Kochk.Domain.Entities.OrderAggregate;

namespace Kochk.Infrastructure.Data;

public static class DataSeed
{
    public static async Task SeedAsync(KochkDbContext DbContext)
    {
        if (!DbContext.Set<ProductBrand>().Any())
        {
            var BrandsText = File.ReadAllText("../Kochk.Infrastructure/Data/DataSeed/Brands.json");
            var BrandList = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsText);
            if (BrandList?.Count > 0)
                await DbContext.AddRangeAsync(BrandList);
            DbContext.SaveChanges();
        }
        if (!DbContext.Set<ProductCategory>().Any())
        {
            var CategoriesText = File.ReadAllText(
                "../Kochk.Infrastructure/Data/DataSeed/Categories.json"
            );
            var CategoryList = JsonSerializer.Deserialize<List<ProductCategory>>(CategoriesText);
            if (CategoryList?.Count > 0)
                await DbContext.AddRangeAsync(CategoryList);
            DbContext.SaveChanges();
        }
        if (!DbContext.Set<Product>().Any())
        {
            var ProductsText = File.ReadAllText(
                "../Kochk.Infrastructure/Data/DataSeed/Products.json"
            );
            var ProductList = JsonSerializer.Deserialize<List<Product>>(ProductsText);
            if (ProductList?.Count > 0)
                await DbContext.AddRangeAsync(ProductList);
            DbContext.SaveChanges();
        }
        if (!DbContext.Set<DeliveryMethod>().Any())
        {
            var deliveryMethodText = File.ReadAllText(
                "../Kochk.Infrastructure/Data/DataSeed/Delivery.json"
            );
            var deliveryMethodList = JsonSerializer.Deserialize<List<DeliveryMethod>>(
                deliveryMethodText
            );
            if (deliveryMethodList?.Count > 0)
                await DbContext.AddRangeAsync(deliveryMethodList);
            DbContext.SaveChanges();
        }
    }
}
