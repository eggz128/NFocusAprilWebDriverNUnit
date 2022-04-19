using NUnit.Framework;

namespace uk.co.edgewords.webdrivernunitdemo
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestOne()
        {
            Assert.Fail("This fails the test");
        }
    }
}