using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Specifications;
using Microsoft.EntityFrameworkCore;
using ProductDb.DataClasses;

namespace DAL.Repository.Boxes
{
    public static class Repository_Boxes
    {
        public static async Task<IReadOnlyList<Box>> GetBoxesWithSpecification(this Repo repo, ISpecification<Box> specification)        
        {
            var query = repo.Context.Boxes
                                    .Include(b => b.Products)
                                        .ThenInclude(p => p.ProductBrand)
                                    .Include(b => b.Products)
                                        .ThenInclude(p => p.ProductType)
                                    .Include(b => b.Products)
                                        .ThenInclude(p => p.Category)
                                    .Where(specification.Criteria);
            
            if (specification.OrderByDescending is not null)
                query = query.OrderByDescending(specification.OrderByDescending);
            else
                query = query.OrderBy(specification.OrderBy);
            
            var boxes = await query
                        .Skip(specification.Skip)
                        .Take(specification.Take)
                        .ToListAsync();
            
            return boxes;
        }
    }
}