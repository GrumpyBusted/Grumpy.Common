using System.Threading;
using FluentAssertions;
using Grumpy.Common.Extensions;
using Grumpy.Common.UnitTests.Helper;
using Xunit;

namespace Grumpy.Common.UnitTests
{
    public class WaitHandleExtensionsTests
    {
        [Fact]
        public void CanCastWaitHandleToTask()
        {
            var waitHandle = new MyWaitHandle();

            var task = waitHandle.ToTask(new CancellationToken());

            task.Status.Should().NotBeNull();
        }
    }
}