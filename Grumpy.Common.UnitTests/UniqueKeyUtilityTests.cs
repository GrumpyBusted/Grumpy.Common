using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grumpy.Common.UnitTests
{
    [TestClass]
    public class UniqueKeyUtilityTests
    {
        [TestMethod]
        public void CanGenerateUniqueKey()
        {
            UniqueKeyUtility.Generate().Should().NotBe("");
        }
    }
}