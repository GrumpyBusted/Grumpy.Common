using FluentAssertions;
using Grumpy.Common.Interfaces;
using Grumpy.Common.Threading;
using Xunit;

namespace Grumpy.Common.UnitTests
{
    public class TaskFactoryTests
    {
        [Fact]
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