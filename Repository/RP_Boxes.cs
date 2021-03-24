using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductDb;
using ProductDb.DataClasses;

namespace Repository
{
    public static class RP_Boxes
    {
        public static async Task<List<Box>> GetAllBoxes(this RP repo)
        {
            var boxes = await repo.ProductContext.Boxes
                        .Include(b => b.Products)
                            .ThenInclude(p => p.ProductBrand)
                        .Include(b => b.Products)
                            .ThenInclude(p => p.ProductType)
                        .Include(b => b.Products)
                            .ThenInclude(p => p.Category)
                        .ToListAsync();

            return boxes;
        }

        public static async Task<List<Box>> GetBoxesWithParams(this RP repo, ProductParams productParams)
        {
            var boxes = repo.ProductContext.Boxes
                        .Include(b => b.Products)
                            .ThenInclude(p => p.ProductBrand)
                        .Include(b => b.Products)
                            .ThenInclude(p => p.ProductType)
                        .Include(b => b.Products)
                            .ThenInclude(p => p.Category);

            var result = new List<Box>();
            foreach (var box in boxes)
            {
               bool isSearch = false, isType = false, isBrand = false, isCategory = false;
               foreach (var product in box.Products)
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
               }
               if (isSearch && isType && isBrand && isCategory)
                   result.Add(box);

            }

            return result;
        }

        public static async Task<Box> GetBoxById(this RP repo, int id)
        {
            var box = await repo.ProductContext.Boxes.FirstOrDefaultAsync(b => b.Id == id);

            return box;
        }
    }
}