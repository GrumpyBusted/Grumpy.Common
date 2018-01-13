using Grumpy.Common.Interfaces;

namespace Grumpy.Common.Threading
{
    public class TaskFactory : ITaskFactory
    {
        private readonly System.Threading.Tasks.TaskFactory _taskFactory;

        public TaskFactory()
        {
            _taskFactory = new System.Threading.Tasks.TaskFactory();
        }

        public ITask Create()
        {
            return new Task(_taskFactory);
        }
    }
}