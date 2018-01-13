using FluentAssertions;
using Grumpy.Common.Interfaces;
using Grumpy.Common.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grumpy.Common.UnitTests
{
    [TestClass]
    public class TaskFactoryTests
    {
        [TestMethod]
        public void CanCreateTask()
        {
            var cut = CreateTaskFactory();

            cut.Create().GetType().Should().Be(typeof(Task));
        }

        private static ITaskFactory CreateTaskFactory()
        {
            return new TaskFactory();
        }
    }
}