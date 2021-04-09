using System.Collections.Generic;
using System.Threading.Tasks;
using DAL;
using DAL.Repository.ProductBrands;
using NUnit.Framework;
using ProductDb.DataClasses;
using Repository.Tests;

namespace Repository.ProductBrands.Tests
{
    [TestFixture]
    public class RepositoryProductBrandsTests
    {
        [Test]
        public async Task Products_GetProductBrands_ReturnsListOfBrands()
        {
            using (var context =  TestHelper.CreateContext())
            {
                var repository = new Repo(context);

                var productBrands = await repository.GetProductBrands();

                Assert.NotZero(productBrands.Count);
                Assert.IsInstanceOf<IReadOnlyList<ProductBrand>>(productBrands);
            }
        }

    }
}