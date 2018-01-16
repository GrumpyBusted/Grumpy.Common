using System;
using System.Threading;
using FluentAssertions;
using Grumpy.Common.Interfaces;
using Grumpy.Common.Threading;
using Xunit;

namespace Grumpy.Common.IntegrationTests
{
    public class TimerTaskTests : IDisposable
    {
        private readonly ITimerTask _cut;

        public TimerTaskTests()
        {
            _cut = new TimerTask();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
                _cut.Dispose();
        }

        [Fact]
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