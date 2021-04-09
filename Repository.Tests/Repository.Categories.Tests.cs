using System.Collections.Generic;
using System.Threading.Tasks;
using DAL;
using DAL.Repository.Categories;
using DAL.Repository.Products;
using NUnit.Framework;
using ProductDb.DataClasses;
using Repository.Tests;

namespace Repository.Categories.Tests
{
    [TestFixture]
    public class RepositoryCategoriesTests
    {
        [Test]
        public async Task Products_GetCategories_ReturnsListOfCategories()
        {
            using (var context =  TestHelper.CreateContext())
            {
                var repository = new Repo(context);

                var categories = await repository.GetCategories();

                Assert.NotZero(categories.Count);
                Assert.IsInstanceOf<IReadOnlyList<Category>>(categories);
            }
        }
    }
}