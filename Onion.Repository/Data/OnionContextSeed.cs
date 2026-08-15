using Onion.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Onion.Repository.Data
{
    public class OnionContextSeed
    {
        public static async Task SeedAsync(OnionContext dbContext)
        {
            // Brands
            if (!dbContext.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("../Onion.Repository/Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands?.Count > 0)
                {
                    foreach (var brand in brands)
                    {
                        await dbContext.Set<ProductBrand>().AddAsync(brand);
                    }
                    
                    await dbContext.SaveChangesAsync();
                }
            }

            // Types
            if (!dbContext.ProductTypes.Any())
            {
                var typesData = File.ReadAllText("../Onion.Repository/Data/DataSeed/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                if (types?.Count > 0)
                {

                    foreach (var type in types)
                    {
                        await dbContext.Set<ProductType>().AddAsync(type);
                    }

                    await dbContext.SaveChangesAsync();
                }
            }

            // Products
            if (!dbContext.Products.Any())
            {
                var productsData = File.ReadAllText("../Onion.Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products?.Count > 0)
                {
                    foreach (var product in products)
                    {
                        await dbContext.Set<Product>().AddAsync(product);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}




