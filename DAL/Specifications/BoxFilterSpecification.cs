using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using ProductDb.DataClasses;

namespace DAL.Specifications
{
    public class BoxFilterSpecification : BaseSpecification<Box>
    {
        

        public BoxFilterSpecification(ProductParams productParams)
        {
            Params = productParams;

            Criteria = ((b) =>  
                (string.IsNullOrEmpty(Params.Search) || b.Products.Any(product => product.Name.ToLower().Contains(Params.Search.ToLower())))
                && (!productParams.BrandId.HasValue || b.Products.Any(product => product.ProductBrandId == Params.BrandId))
                && (!productParams.TypeId.HasValue || b.Products.Any(product => product.ProductTypeId == Params.TypeId))
                && (!Params.CategoryId.HasValue || b.Products.Any(product => product.CategoryId == Params.CategoryId))
                );

            Skip = productParams.PageSize * (productParams.PageIndex - 1);
            Take = productParams.PageSize;

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        OrderBy = (p => p.Price);
                        break;
                    case "priceDesc":
                        OrderByDescending = (p => p.Price);
                        break;
                    default:
                        OrderBy = (p => p.Name);
                        break;
                }
            }    
        }

        private ExpressionStarter<Box> CreateCriteria()
        {
            var predicates = new List<Expression<Func<Box, bool>>>();

            if (!string.IsNullOrEmpty(Params.Search))
                predicates.Add(box => box.Products.Any(product => product.Name.ToLower().Contains(Params.Search.ToLower())));

            if (Params.TypeId != null)
                predicates.Add(box => box.Products.Any(product => product.ProductTypeId == Params.TypeId));

            if (Params.BrandId != null)
                predicates.Add(box => box.Products.Any(product => product.ProductBrandId == Params.BrandId));

            if (Params.CategoryId != null)
                predicates.Add(box => box.Products.Any(product => product.CategoryId == Params.CategoryId));

            var predicate = PredicateBuilder.New<Box>();

            predicate = predicates.Aggregate(predicate, (current, pred) => current.And(pred));

            return predicate;
        }
    }
}