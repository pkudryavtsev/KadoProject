using System.Collections.Generic;
using System.Threading.Tasks;
using DAL;
using DAL.Repository.Products;
using DAL.Repository.ProductTypes;
using NUnit.Framework;
using ProductDb.DataClasses;
using Repository.Tests;

namespace Repository.ProductTypes.Tests
{
    [TestFixture]
    public class RepositoryProductTypesTests
    {
        [Test]
        public async Task Products_GetProductTypes_ReturnsListOfTypes()
        {
            using (var context =  TestHelper.CreateContext())
            {
                var repository = new Repo(context);

                var productTypes = await repository.GetProductTypes();

                Assert.NotZero(productTypes.Count);
                Assert.IsInstanceOf<IReadOnlyList<ProductType>>(productTypes);
            }
        }  
    }
}