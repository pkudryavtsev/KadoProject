using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductDb.DataClasses;

namespace DAL.Repository.ProductTypes
{
    public static class RepositoryProductTypes
    {
        public static async Task<IReadOnlyList<ProductType>> GetProductTypes(this Repo repo)
        {
            return await repo.Context.ProductTypes.ToListAsync();
        } 

        private static async Task<ProductType> GetProductTypeById(this Repo repo,int id)
        {
            return await repo.Context.ProductTypes.FirstOrDefaultAsync(pt => pt.Id == id);
        }

        public static async Task<bool> CreateProductType(this Repo repo, ProductType productType)
        {
            await repo.Context.ProductTypes.AddAsync(productType);
            var changes = await repo.Context.SaveChangesAsync();
            return changes > 0;
        }

        public static async Task<bool> UpdateProductType(this Repo repo, ProductType productType)
        {
            var typeToUpdate = repo.GetProductTypeById(productType.Id);
            if (typeToUpdate.IsCompleted && typeToUpdate is null)
                return false;

            repo.Context.ProductTypes.Update(productType);
            var changes = await repo.Context.SaveChangesAsync();
            return changes > 0;
        }

        public static async Task<bool> DeleteProductType(this Repo repo, int id)
        {
            var typeToDelete = repo.GetProductTypeById(id);
            if (typeToDelete.IsCompleted && typeToDelete.Result is null)
                return false;

            repo.Context.ProductTypes.Remove(await typeToDelete);
            var changes = await repo.Context.SaveChangesAsync();
            return changes > 0;
        }
    }
}