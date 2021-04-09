using System.Collections.Generic;
using System.Threading.Tasks;
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
            return await repo.Context.ProductBrands.FirstOrDefaultAsync(pb => pb.Id == id);
        }
        public static async Task<bool> CreateProductBrand(this Repo repo, ProductBrand productBrand)
        {
            await repo.Context.ProductBrands.AddAsync(productBrand);
            var changes = await repo.Context.SaveChangesAsync();
            return changes > 0;
        }

        public static async Task<bool> UpdateProductBrand(this Repo repo, ProductBrand productBrand)
        {
            var brandToUpdate = repo.GetProductBrandById(productBrand.Id);
            if (brandToUpdate.IsCompleted && brandToUpdate is null)
                return false;

            repo.Context.Update(productBrand);
            var changes = await repo.Context.SaveChangesAsync();
            return changes > 0;
        }

        public static async Task<bool> DeleteProductBrand(this Repo repo, int id)
        {
            var brandToDelete = repo.GetProductBrandById(id);
            if (brandToDelete.IsCompleted && brandToDelete.Result is null)
                return false;

            repo.Context.ProductBrands.Remove(await brandToDelete);
            var changes = await repo.Context.SaveChangesAsync();
            return changes > 0;
        }
    }
}