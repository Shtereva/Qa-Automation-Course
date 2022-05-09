using System.Collections.ObjectModel;

namespace CollectionsTests
{
    using NUnit.Framework;
    public class Tests : BaseTest
    {
        [Test]
        [Timeout(1000)]
        public void Test_Collections_1MillionItems()
        {
            // Arrange
            var numbers = new Collection<int>();

            // Act


            // Assert
            Assert.Pass();
        }
    }
}