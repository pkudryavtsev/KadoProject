using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductDb;
using ProductDb.DataClasses;

namespace Repository
{
    public static class RP_Products
    {
        public static async Task<IReadOnlyList<Product>> GetAllProducts(this RP repo) 
        {
            return await repo.ProductContext.Products
                            .Include(p => p.ProductBrand)
                            .Include(p => p.ProductType)
                            .Include(p => p.Category)
                            .ToListAsync();
        }

        public static async Task<IReadOnlyList<Product>> GetProductsWithParams(this RP repo, ProductParams productParams)
        {
            var products = repo.ProductContext.Products
                            .Include(p => p.ProductBrand)
                            .Include(p => p.ProductType)
                            .Include(p => p.Category);

            var result = new List<Product>();

            bool isSearch = false, isType = false, isBrand = false, isCategory = false;
            foreach (var product in products)
            {
                if (!string.IsNullOrEmpty(productParams.Search))
                {
                    if (product.Name.ToLower().Contains(productParams.Search.ToLower()))
                        isSearch = true;
                }
                else
                    isSearch = true;

                if (productParams.TypeId != null)
                {
                    if (product.ProductTypeId == productParams.TypeId)
                        isType = true;
                }
                else
                    isType = true;

                if (productParams.BrandId != null)
                {
                    if (product.ProductBrandId == productParams.BrandId)
                        isBrand = true;
                }
                else
                    isBrand = true;

                if (productParams.CategoryId != null)
                {
                    if (product.CategoryId == productParams.CategoryId)
                        isCategory = true;
                }
                else
                    isCategory = true;
                if (isSearch && isType && isBrand && isCategory)
                    result.Add(product);
                isSearch = isType = isBrand = isCategory = false;
            }  

            return result;
        }
                
        public static async Task<IReadOnlyList<ProductBrand>> GetProductBrands(this RP repo)
        {
            return await repo.ProductContext.ProductBrands.ToListAsync();
        }
        
        public static async Task<IReadOnlyList<ProductType>> GetProductTypes(this RP repo)
        {
            return await repo.ProductContext.ProductTypes.ToListAsync();
        }
        
        public static async Task<IReadOnlyList<Category>> GetProductCategories(this RP repo)
        {
            return await repo.ProductContext.Categories.ToListAsync();
        }
    }
}