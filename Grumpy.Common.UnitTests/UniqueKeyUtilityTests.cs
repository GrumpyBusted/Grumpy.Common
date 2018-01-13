using FluentAssertions;
using Xunit;

namespace Grumpy.Common.UnitTests
{
    public class UniqueKeyUtilityTests
    {
        [Fact]
        public void CanGenerateUniqueKey()
        {
            UniqueKeyUtility.Generate().Should().NotBe("");
        }
    }
}