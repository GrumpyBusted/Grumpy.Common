using System.Threading;
using FluentAssertions;
using Grumpy.Common.Extensions;
using Grumpy.Common.UnitTests.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grumpy.Common.UnitTests
{
    [TestClass]
    public class WaitHandleExtensionsTests
    {
        [TestMethod]
        public void CanCastWaitHandleToTask()
        {
            var waitHandle = new MyWaitHandle();

            var task = waitHandle.ToTask(new CancellationToken());

            task.Status.Should().NotBeNull();
        }
    }
}