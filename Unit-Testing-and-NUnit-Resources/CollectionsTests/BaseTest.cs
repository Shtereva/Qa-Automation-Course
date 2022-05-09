namespace CollectionsTests
{
    using Collections;
    using NUnit.Framework;
    public class BaseTest
    {
        private Collection<int> numbers;

        public Collection<int> Numbers
        {
            get => this.numbers;
            set
            {
                this.numbers = value;
            }
        }


        [SetUp]
        public void TestInitialize()
        {
            this.Numbers = new Collection<int>();
        }
    }
}
