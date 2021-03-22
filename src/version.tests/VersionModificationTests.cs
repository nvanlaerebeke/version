using NUnit.Framework;
using System.Collections.Generic;

namespace version.tests
{
    [TestFixture]
    public class VersionModificationTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void VerifyTest()
        {
            var values = new List<string>() { "1.0.blaat", "abc", "1.2.3.4.5" };
            foreach(var value in values) {
                Assert.IsNull(new VersionModification().Verify(value));
            }
        }
    }
}