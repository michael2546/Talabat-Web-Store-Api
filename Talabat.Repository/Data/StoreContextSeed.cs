using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext dbContext)
        {

            if (!dbContext.productBrands.Any())
            {
                //D:\Route\.NET\material & assignments\08 ASP.NET Core Web APIs\Session 01\new project eng aliaa\Talabat.Solution\Talabat.Repository\Data\DataSeed\brands.json
                var BrandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                if (Brands?.Count > 0)  //Brands? => Brands is not null?
                {
                    foreach (var Brand in Brands)
                        await dbContext.Set<ProductBrand>().AddAsync(Brand);

                    await dbContext.SaveChangesAsync();
                } 
            }

            if (!dbContext.ProductTypes.Any())
            {
                var TypesData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                if (Types?.Count > 0)
                {
                    foreach (var type in Types)
                        await dbContext.Set<ProductType>().AddAsync(type);

                    await dbContext.SaveChangesAsync();
                } 
            }

            if (!dbContext.Products.Any())
            {
                var ProductsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                if (Products?.Count > 0)
                {
                    foreach (var product in Products)
                        await dbContext.Set<Product>().AddAsync(product);

                    await dbContext.SaveChangesAsync();
                } 
            }


        }
    }
}
