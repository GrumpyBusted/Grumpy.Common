using FluentAssertions;
using Grumpy.Common.Interfaces;
using Xunit;

namespace Grumpy.Common.UnitTests
{
    public class UniqueKeyTests
    {
        [Fact]
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