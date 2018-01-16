using Grumpy.Common.Interfaces;

namespace Grumpy.Common.Threading
{
    /// <inheritdoc />
    public class TaskFactory : ITaskFactory
    {
        private readonly System.Threading.Tasks.TaskFactory _taskFactory;

        /// <inheritdoc />
        public TaskFactory()
        {
            _taskFactory = new System.Threading.Tasks.TaskFactory();
        }

        /// <inheritdoc />
        public ITask Create()
        {
            return new Task(_taskFactory);
        }
    }
}