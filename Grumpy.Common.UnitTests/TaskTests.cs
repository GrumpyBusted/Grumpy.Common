using System.Diagnostics;
using System.Threading;
using FluentAssertions;
using Grumpy.Common.Interfaces;
using Grumpy.Common.Threading;
using Xunit;

namespace Grumpy.Common.UnitTests
{
    public class TaskTests
    {
        private readonly ITask _cut;

        public TaskTests()
        {
            _cut = new Task(new System.Threading.Tasks.TaskFactory());
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
        }

        [Fact]
        public void CanStartWithState()
        {
            var i = 0;
            _cut.Start(a => i = (int)a, 1, new CancellationToken());
            _cut.Wait();
        }

        [Fact]
        public void CanStopTask()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            _cut.Start(() => Thread.Sleep(1000));
            _cut.Stop();
            stopwatch.Stop();
        }
    }
}