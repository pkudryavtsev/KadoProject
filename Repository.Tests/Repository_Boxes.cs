using System;
using Xunit;

namespace Repository.Tests
{
    public class Repository_Boxes
    {
        [Fact]
        public void DummyTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        [Fact]
        public void SecondDummyTest() 
        {
            Assert.Equal(5, Add(3, 3));
        }

        int Add(int a, int b) => a + b;
    }
}
