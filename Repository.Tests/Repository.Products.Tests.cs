using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Repository.ProductBrands;
using DAL.Repository.Products;
using DAL.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using ProductDb;
using ProductDb.DataClasses;

namespace Repository.Tests
{
    public class RepositoryProductsTests
    {
        [SetUp]
        public void Setup()
        {
            TestHelper.SeedTestDb();
        }     

        [Test]
        public void DatabaseIsSeeded()
        {
            using (var context = TestHelper.CreateContext())
            {
                Assert.True(context.ProductBrands.Any());
                Assert.True(context.ProductTypes.Any());
                Assert.True(context.Categories.Any());
                Assert.True(context.Boxes.Any());
                Assert.True(context.Products.Any());
            }
        }

        [Test]
        [TestCaseSource(nameof(ProductFilterSpecificationCases))]
        public async Task Products_GetProductsWithSpecification_ReturnsListOfProducts(ValueTuple<ProductFilterSpecification, int> testParams)
        {
            using (var context =  TestHelper.CreateContext())
            {
                //Arrange
                (ProductFilterSpecification caseSpecification, int expectedCount) = testParams;
                var repository = new Repo(context);

                //Act
                var products = await repository.GetProductsWithSpecification(caseSpecification);

                //Assert
                Assert.NotNull(products);
                Assert.IsInstanceOf<List<Product>>(products);
                Assert.AreEqual(expectedCount, products.Count);
            
                foreach(var product in products)
                {
                    if (caseSpecification.Params.BrandId is not null) Assert.AreEqual(product.ProductBrandId, caseSpecification.Params.BrandId);
                    if (caseSpecification.Params.TypeId is not null) Assert.AreEqual(product.ProductTypeId, caseSpecification.Params.TypeId);
                    if (caseSpecification.Params.CategoryId is not null) Assert.AreEqual(product.CategoryId, caseSpecification.Params.CategoryId);
                    if (caseSpecification.Params.Search is not null) Assert.IsTrue(product.Name.ToLower().Contains(caseSpecification.Params.Search));
                    Assert.IsInstanceOf<Product>(product);
                }   
            }
        }

        [Test]
        public async Task Products_GetProductsWithNullSpecification_ReturnsAllProducts()
        {
            using (var context =  TestHelper.CreateContext())
            {
                //Arrange
                var repository = new Repo(context);

                //Act
                var products = await repository.GetProductsWithSpecification(null);

                //Assert
                Assert.IsInstanceOf<IReadOnlyList<Product>>(products);
                Assert.AreEqual(products.Count, 12);
            }
        }      

        [Test]
        public async Task Products_CreateProduct_ReturnsTrueIfSucess()
        {
            using (var context = TestHelper.CreateContext())
            {
                var repository = new Repo(context);
                var product = new Product { ProductBrandId = 1, ProductTypeId = 1, CategoryId = 1, Name = "New"};

                var isSuccess = await repository.CreateProduct(product);

                Assert.IsTrue(isSuccess);
            }
        }

        [Test]
        public async Task Products_CreateProduct_ReturnsFalseIfFail()
        {
            using (var context = TestHelper.CreateContext())
            {
                var repository = new Repo(context);
                Product product = null;

                var isSuccess = await repository.CreateProduct(product);

                Assert.IsFalse(isSuccess);
            }
        }

        [Test]
        public async Task Products_UpdateProduct_ReturnsTrueIfSuccess()
        {
            using (var context = TestHelper.CreateContext())
            {
                var repository = new Repo(context);
                var product = new Product { Id = 1, Name = "Changed"};

                var isSuccess = await repository.UpdateProduct(product);

                Assert.IsTrue(isSuccess);
            }
        }

        

        static IEnumerable<(ProductFilterSpecification, int)> ProductFilterSpecificationCases()
        {
            yield return (new ProductFilterSpecification(new ProductParams { BrandId = 1, PageSize = 0}), 4);
            yield return (new ProductFilterSpecification(new ProductParams { BrandId = 2, PageSize = 0}), 4);
            yield return (new ProductFilterSpecification(new ProductParams { BrandId = 3, PageSize = 0}), 4);
            yield return (new ProductFilterSpecification(new ProductParams { TypeId = 1, PageSize = 0}), 4);
            yield return (new ProductFilterSpecification(new ProductParams { TypeId = 2, PageSize = 0}), 4);
            yield return (new ProductFilterSpecification(new ProductParams { TypeId = 3, PageSize = 0}), 4);
            yield return (new ProductFilterSpecification(new ProductParams { CategoryId = 1, PageSize = 0}), 4);
            yield return (new ProductFilterSpecification(new ProductParams { CategoryId = 2, PageSize = 0}), 4);
            yield return (new ProductFilterSpecification(new ProductParams { CategoryId = 3, PageSize = 0}), 4);
            yield return (new ProductFilterSpecification(new ProductParams { Search = "Product", PageSize = 0}), 12);
            yield return (new ProductFilterSpecification(new ProductParams { Search = "Product" }), 6);
            yield return (new ProductFilterSpecification(new ProductParams { Search = "Dummy" }), 0);
            yield return (new ProductFilterSpecification(new ProductParams { BrandId = 1, TypeId = 1, CategoryId = 1, PageSize = 0}), 4);
            yield return (new ProductFilterSpecification(new ProductParams { BrandId = 1, TypeId = 2, CategoryId = 1, PageSize = 0}), 0);
            yield return (new ProductFilterSpecification(new ProductParams { BrandId = 2, TypeId = 2, CategoryId = 2, PageSize = 0}), 4);
            yield return (new ProductFilterSpecification(new ProductParams { BrandId = 2, TypeId = 3, CategoryId = 2, PageSize = 0}), 0);
            yield return (new ProductFilterSpecification(new ProductParams { BrandId = 1, TypeId = 1, CategoryId = 1, Search = "Product", PageSize = 0 }), 4);
            yield return (new ProductFilterSpecification(new ProductParams { BrandId = 1, TypeId = 1, CategoryId = 1, Search = "Product 1", PageSize = 0 }), 2);
            yield return (new ProductFilterSpecification(new ProductParams { BrandId = 1, TypeId = 1, CategoryId = 1, Search = "Product 2", PageSize = 0 }), 0);
            yield return (new ProductFilterSpecification(new ProductParams { BrandId = 1, TypeId = 1, CategoryId = 1, Search = "10", PageSize = 0 }), 1);
            yield return (new ProductFilterSpecification(new ProductParams { BrandId = 2, TypeId = 1, CategoryId = 1, Search = "10", PageSize = 0 }), 0);
            yield return (new ProductFilterSpecification(new ProductParams { BrandId = 3, TypeId = 3, CategoryId = 3, Search = "12", PageSize = 0 }), 1);
        }
    }
}