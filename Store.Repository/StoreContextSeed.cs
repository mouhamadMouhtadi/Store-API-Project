using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Data.Context;
using Store.Data.Entity;

namespace Store.Repository
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync (StoreDbContext context , ILoggerFactory loggerFactory)
        {
			try
			{
				if (context.ProductTypes != null && !context.ProductTypes.Any())
				{
					var typeData = File.ReadAllText("../Store.Repository/SeedData/types.json");
					var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);
                    if (types is not null)
                    {
                        await context.ProductTypes.AddRangeAsync(types);
                        await context.SaveChangesAsync();
                    }
				if (context.ProductBrands != null && !context.ProductBrands.Any())
				{
					var brandData = File.ReadAllText("../Store.Repository/SeedData/brands.json");
					var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
					if (brands is not null)
					{
                            await context.ProductBrands.AddRangeAsync(brands);
                            await context.SaveChangesAsync();
                        }
				}

                }
				if (context.Products != null && !context.Products.Any())
				{
					var productsData = File.ReadAllText("../Store.Repository/SeedData/products.json");
					var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    if (products is not null)
                    {
                        foreach (var product in products)
                        {
                            product.BrandId = context.ProductBrands.First(b => b.Name == "Grill Master").Id; // Example for "Grill Master"
                            product.TypeId = context.ProductTypes.First(t => t.Name == "Grilled Chicken").Id;  // Example for "Grilled Chicken"
                        }
                        await context.Products.AddRangeAsync(products);
                        await context.SaveChangesAsync();
                    }

                }
				if (context.deliveryMethods != null && !context.deliveryMethods.Any())
				{
					var deliveryMethod = File.ReadAllText("../Store.Repository/SeedData/delivery.json");
					var products = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethod);
                    if (products is not null)
                    {
                        await context.deliveryMethods.AddRangeAsync(products);
                        await context.SaveChangesAsync();
                    }

                }
            }
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<StoreDbContext>();
				logger.LogError(ex.Message);
			}
        }
    }
}
