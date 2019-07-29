using FluentAssertions;
using Grumpy.Common.Threading;
using Xunit;

namespace Grumpy.Common.IntegrationTests
{
    public class TimerUtilityTests
    {
        [Fact]
        public void CanWaitForExpression()
        {
            var i = 0;
            TimerUtility.WaitForIt(() => { ++i; return i > 2; }, 100).Should().BeTrue();
        }

        [Fact]
        public void CanWaitForItTimeout()
        {
            TimerUtility.WaitForIt(() => false, 100).Should().BeFalse();
        }

        [Fact]
        public void CanWaitForItWithoutTimeout()
        {
            const bool res = true;

            TimerUtility.WaitForIt(() => true);

            res.Should().BeTrue();
        }
    }
}