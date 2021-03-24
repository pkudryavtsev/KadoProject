using Microsoft.EntityFrameworkCore;
using ProductDb.DataClasses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductDb
{
    public static class ProductDbContext_DAL
	{
		public static async Task<List<Product>> GetAllProducts(this ProductDbContext context)
        {
			return await context.Products.ToListAsync();
        }
	}
}
