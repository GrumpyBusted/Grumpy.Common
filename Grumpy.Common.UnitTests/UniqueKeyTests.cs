using FluentAssertions;
using Grumpy.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grumpy.Common.UnitTests
{
    [TestClass]
    public class UniqueKeyTests
    {
        [TestMethod]
        public void CanGenerateUniqueKey()
        {
            var cut = CreateUniqueKey();

            cut.Generate().Length.Should().BeGreaterThan(10);
        }

        private static IUniqueKey CreateUniqueKey()
        {
            return new UniqueKey();
        }
    }
}