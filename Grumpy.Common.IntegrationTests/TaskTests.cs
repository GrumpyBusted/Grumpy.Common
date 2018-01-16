using System;
using System.Diagnostics;
using System.Threading;
using FluentAssertions;
using Grumpy.Common.Interfaces;
using Grumpy.Common.Threading;
using Xunit;

namespace Grumpy.Common.IntegrationTests
{
    public class TaskTests : IDisposable
    {
        private readonly ITask _cut;

        public TaskTests()
        {
            _cut = new Task(new System.Threading.Tasks.TaskFactory());
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
            _cut.Start(() => ++i);
            TimerUtility.WaitForIt(() => i > 0, 100);
            _cut.IsCompleted.Should().BeTrue();
            i.Should().Be(1);
        }

        [Fact]
        public void CanStartWithCancellationTokenTask()
        {
            var i = 0;
            _cut.Start(() => ++i, new CancellationToken());
            TimerUtility.WaitForIt(() => i > 0, 100);
            _cut.IsCompleted.Should().BeTrue();
            i.Should().Be(1);
        }

        [Fact]
        public void CanStartWithState()
        {
            var i = 0;
            _cut.Start(a => i = (int)a, 1, new CancellationToken());
            _cut.Wait().Should().BeTrue();
            i.Should().Be(1);
            _cut.AsyncState.GetType().Should().Be(typeof(int));
            _cut.Exception.Should().BeNull();
        }

        [Fact]
        public void CanStopTask()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            _cut.Start(() => Thread.Sleep(1000));
            _cut.Stop();
            stopwatch.Stop();
            stopwatch.ElapsedMilliseconds.Should().BeLessThan(1000);
            _cut.IsFaulted.Should().BeFalse();
        }
    }
}