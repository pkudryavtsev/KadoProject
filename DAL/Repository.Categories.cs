using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Repository.Products;
using Microsoft.EntityFrameworkCore;
using ProductDb.DataClasses;

namespace DAL.Repository.Categories
{
    public static class RepositoryCategories
    {
        public static async Task<IReadOnlyList<Category>> GetCategories(this Repo repo)
        {
            return await repo.Context.Categories.ToListAsync();
        } 

        private static async Task<Category> GetCategoryById(this Repo repo,int id)
        {
            return await repo.Context.Categories
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(c => c.Id == id);
        }

        public static async Task<bool> CreateCategory(this Repo repo, Category category)
        {
            if (category is null)
                return false;

            if (category.Id is not 0)
                return false;

            await repo.Context.Categories.AddAsync(category);
            var changes = await repo.Context.SaveChangesAsync();
            return changes > 0;
        }

        public static async Task<bool> UpdateCategory(this Repo repo, Category category)
        {
            if (category is null)
                return false;

            var typeToUpdate = await repo.GetCategoryById(category.Id);
            if (typeToUpdate is null)
                return false;

            var entry = repo.Context.Entry(category);
            entry.State = EntityState.Modified;
            var changes = await repo.Context.SaveChangesAsync();
            return changes > 0;
        }

        public static async Task<bool> RemoveCategory(this Repo repo, int id)
        {
            var products = await repo.GetAllProducts();
            foreach (var product in products)
            {
                if (product.CategoryId == id)
                    product.CategoryId = null;
            }

            var typeToDelete = await repo.GetCategoryById(id);
            if (typeToDelete is null)
                return false;

            repo.Context.Categories.Remove(typeToDelete);
            var changes = await repo.Context.SaveChangesAsync();
            return changes > 0;
        }
    }
}