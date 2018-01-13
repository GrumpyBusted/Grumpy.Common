using System.Threading;
using FluentAssertions;
using Grumpy.Common.Interfaces;
using Grumpy.Common.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grumpy.Common.UnitTests
{
    [TestClass]
    public class TimerTaskTests
    {
        private readonly ITimerTask _cut;

        public TimerTaskTests()
        {
            _cut = new TimerTask();
        }

        [TestMethod]
        public void CanStartTask()
        {
            var i = 0;
            _cut.Start(() => ++i, 10, new CancellationToken());
            TimerUtility.WaitForIt(() => i > 2, 100);
            _cut.Stop();
            i.Should().BeGreaterOrEqualTo(2);
        }
    }
}