using FluentAssertions;
using Grumpy.Common.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grumpy.Common.UnitTests
{
    [TestClass]
    public class TimerUtilityTests
    {
        [TestMethod]
        public void CanWaitForExpression()
        {
            var i = 0;
            TimerUtility.WaitForIt(() => { ++i; return i > 2; }, 100).Should().BeTrue();
        }

        [TestMethod]
        public void CanWaitForItTimeout()
        {
            TimerUtility.WaitForIt(() => false, 100).Should().BeFalse();
        }

        [TestMethod]
        public void CanWaitForItWithoutTimeout()
        {
            TimerUtility.WaitForIt(() => true);
        }
    }
}