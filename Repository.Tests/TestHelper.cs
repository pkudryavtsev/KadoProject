using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductDb;
using ProductDb.DataClasses;
using ProductDb.DataClasses.Enums;

namespace Repository.Tests
{
    public static class TestHelper
    {
        public static ProductDbContext CreateContext()
        {
            string ConnStr = "PresentBox_Test";
            DirectoryInfo info = new DirectoryInfo(Directory.GetCurrentDirectory());
			DirectoryInfo temp = info.Parent.Parent.Parent.Parent;
			string CurDir = Path.Combine(temp.ToString(), "API");
			var configuration =  new ConfigurationBuilder().SetBasePath(CurDir).AddJsonFile("appsettings.json").Build();
			var builder = new DbContextOptionsBuilder<ProductDbContext>();
			string connectionString = configuration.GetConnectionString(ConnStr);
            builder.UseSqlServer(connectionString);
			return new ProductDbContext(builder.Options) {Configuration = configuration};
        }

        public static void SeedTestDb()
        {
            using (var context = CreateContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var productBrands = new List<ProductBrand> {
                    new ProductBrand { Name = "Product brand 1"},
                    new ProductBrand { Name = "Product brand 2"},
                    new ProductBrand { Name = "Product brand 3"},
                };
                context.ProductBrands.AddRange(productBrands);
                context.SaveChanges();

                var productTypes = new List<ProductType> {
                    new ProductType { Name = "Product type 1"},
                    new ProductType { Name = "Product type 2"},
                    new ProductType { Name = "Product type 3"},
                };
                context.ProductTypes.AddRange(productTypes);
                context.SaveChanges();

                var categories = new List<Category> {
                    new Category { Name = "Category 1"},
                    new Category { Name = "Category 2"},
                    new Category { Name = "Category 3"},
                };
                context.Categories.AddRange(categories);
                context.SaveChanges();

                var boxes = new List<Box> {
                    new Box { Name = "Box 1", Size = BoxSize.Small, Color = BoxColor.Red},
                    new Box { Name = "Box 2", Size = BoxSize.Medium, Color = BoxColor.Black},
                    new Box { Name = "Box 3", Size = BoxSize.Large, Color = BoxColor.Green},
                    new Box { Name = "Box 4", Size = BoxSize.Small, Color = BoxColor.Blue},
                };
                context.Boxes.AddRange(boxes);
                context.SaveChanges();

                var products = new List<Product> {
                    new Product { ProductBrandId = 1, ProductTypeId = 1, CategoryId = 1, BoxId = 1, Name = "Product 1"}, 
                    new Product { ProductBrandId = 2, ProductTypeId = 2, CategoryId = 2, BoxId = 1, Name = "Product 2"}, 
                    new Product { ProductBrandId = 3, ProductTypeId = 3, CategoryId = 3, BoxId = 1, Name = "Product 3"}, 
                    new Product { ProductBrandId = 1, ProductTypeId = 1, CategoryId = 1, BoxId = 2, Name = "Product 4"}, 
                    new Product { ProductBrandId = 2, ProductTypeId = 2, CategoryId = 2, BoxId = 2, Name = "Product 5"}, 
                    new Product { ProductBrandId = 3, ProductTypeId = 3, CategoryId = 3, BoxId = 2, Name = "Product 6"}, 
                    new Product { ProductBrandId = 1, ProductTypeId = 1, CategoryId = 1, BoxId = 3, Name = "Product 7"}, 
                    new Product { ProductBrandId = 2, ProductTypeId = 2, CategoryId = 2, BoxId = 3, Name = "Product 8"}, 
                    new Product { ProductBrandId = 3, ProductTypeId = 3, CategoryId = 3, BoxId = 3, Name = "Product 9"}, 
                    new Product { ProductBrandId = 1, ProductTypeId = 1, CategoryId = 1, BoxId = 4, Name = "Product 10"}, 
                    new Product { ProductBrandId = 2, ProductTypeId = 2, CategoryId = 2, BoxId = 4, Name = "Product 11"}, 
                    new Product { ProductBrandId = 3, ProductTypeId = 3, CategoryId = 3, BoxId = 4, Name = "Product 12"},
                };
                context.Products.AddRange(products);
                context.SaveChanges();
            };
        }
    }
}