using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Repository.Products;
using Microsoft.EntityFrameworkCore;
using ProductDb.DataClasses;

namespace DAL.Repository.ProductBrands
{
    public static class Repository_ProductBrands
    {
        public static async Task<IReadOnlyList<ProductBrand>> GetProductBrands(this Repo repo)
        {
            return await repo.Context.ProductBrands.ToListAsync();
        } 
        
        private static async Task<ProductBrand> GetProductBrandById(this Repo repo, int id)
        {
            return await repo.Context.ProductBrands
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(pb => pb.Id == id);
        }
        public static async Task<bool> CreateProductBrand(this Repo repo, ProductBrand productBrand)
        {
            if (productBrand is null)
                return false;

            if (productBrand.Id is not 0)
                return false;

            await repo.Context.ProductBrands.AddAsync(productBrand);
            var changes = await repo.Context.SaveChangesAsync();
            return changes > 0;
        }

        public static async Task<bool> UpdateProductBrand(this Repo repo, ProductBrand productBrand)
        {
            if (productBrand is null)
                return false;

            var brandToUpdate = await repo.GetProductBrandById(productBrand.Id);
            if (brandToUpdate is null)
                return false;

            var entry = repo.Context.Entry(productBrand);
            entry.State = EntityState.Modified;
            var changes = await repo.Context.SaveChangesAsync();
            return changes > 0;
        }

        public static async Task<bool> RemoveProductBrand(this Repo repo, int id)
        {
            var products = await repo.GetAllProducts();
            foreach (var product in products)
            {
                if (product.ProductBrandId == id)
                    product.ProductBrandId = null;
            }

            var brandToDelete = await repo.GetProductBrandById(id);
            if (brandToDelete is null)
                return false;

            repo.Context.ProductBrands.Remove(brandToDelete);
            var changes = await repo.Context.SaveChangesAsync();
            return changes > 0;
        }
    }
}