using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Specifications;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using ProductDb.DataClasses;

namespace DAL.Repository.Products
{
    public static class Repository_Products
    {
        public static async Task<IReadOnlyList<Product>> GetProductsWithSpecification(this Repo repo, ProductFilterSpecification specification)
        {
            if (specification is null)
                return await repo.Context.Products.ToListAsync();

            var query = repo.Context.Products
                                .Include(p => p.ProductBrand)
                                .Include(p => p.ProductType)
                                .Include(p => p.Category)
                                .Where(specification.Criteria is null? (p => true) : specification.Criteria);

            if (specification.OrderByDescending is not null)
                query = query.OrderByDescending(specification.OrderByDescending);
            else 
                query = query.OrderBy(specification.OrderBy);


            IReadOnlyList<Product> products;
            if (specification.Take is 0)
                products = await query.ToListAsync();
            else 
                products = await query.AsNoTracking()
                            .Skip(specification.Skip)
                            .Take(specification.Take)
                            .ToListAsync();

            return products;
        }

        public static async Task<Product> GetProductById(this Repo repo, int id)
        {
            var product = await repo.Context.Products.AsNoTracking()
                                .Include(p => p.ProductBrand)
                                .Include(p => p.ProductType)
                                .Include(p => p.Category)
                            .FirstOrDefaultAsync(p => p.Id == id);

            return product;
        }

        public static async Task<bool> CreateProduct(this Repo repo, Product product)
        {
            if (product is null)
                return false;

            await repo.Context.Products.AddAsync(product);
            var changes = await repo.Context.SaveChangesAsync();
            return changes > 0;
        }

        public static async Task<bool> RemoveProduct(this Repo repo, int id)
        {
            var productToDelete = repo.GetProductById(id);
            if (productToDelete is null)
                return false;

            repo.Context.Remove(productToDelete);
            var changes = await repo.Context.SaveChangesAsync();
            return changes > 0;
        }

        public static async Task<bool> UpdateProduct(this Repo repo, Product product)
        {
            var productToUpdate = await repo.GetProductById(product.Id);
            if (productToUpdate is null)
                return false;

            //repo.Context.Products.Attach(product);
            var entry = repo.Context.Entry(product);
            entry.State = EntityState.Modified;
            var changes = await repo.Context.SaveChangesAsync();
            return changes > 0;
        }
    }
}