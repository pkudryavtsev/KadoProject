using System.Collections.Generic;
using System.Threading.Tasks;
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
            return await repo.Context.Categories.FirstOrDefaultAsync(pt => pt.Id == id);
        }


        public static async Task<bool> CreateCategory(this Repo repo, Category category)
        {
            await repo.Context.Categories.AddAsync(category);
            var changes = await repo.Context.SaveChangesAsync();
            return changes > 0;
        }

        public static async Task<bool> UpdateCategory(this Repo repo, Category category)
        {
            var categoryToUpdate = repo.GetCategoryById(category.Id);
            if (categoryToUpdate.IsCompleted && categoryToUpdate is null)
                return false;

            repo.Context.Categories.Update(category);
            var changes = await repo.Context.SaveChangesAsync();
            return changes > 0;
        }

        public static async Task<bool> DeleteCategory(this Repo repo, int id)
        {
            var categoryToDelete = repo.GetCategoryById(id);
            if (categoryToDelete.IsCompleted && categoryToDelete.Result is null)
                return false;

            repo.Context.Categories.Remove(await categoryToDelete);
            var changes = await repo.Context.SaveChangesAsync();
            return changes > 0;
        }
    }
}