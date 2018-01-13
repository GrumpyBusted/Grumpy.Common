using FluentAssertions;
using Grumpy.Common.Threading;
using Xunit;

namespace Grumpy.Common.UnitTests
{
    public class TimerUtilityTests
    {
        [Fact]
        public void CanWaitForExpression()
        {
            var i = 0;
            // ReSharper disable once UnusedVariable
            var res = TimerUtility.WaitForIt(() => { ++i; return i > 2; }, 100);
        }

        [Fact]
        public void CanWaitForItTimeout()
        {
            TimerUtility.WaitForIt(() => false, 100).Should().BeFalse();
        }

        [Fact]
        public void CanWaitForItWithoutTimeout()
        {
            TimerUtility.WaitForIt(() => true);
        }
    }
}