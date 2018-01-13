namespace Grumpy.Common.Interfaces
{
    /// <summary>
    /// Async Task Factory - This can be used in classes that need a task and need to be able to stub this in unit test cases
    /// </summary>
    public interface ITaskFactory
    {
        /// <summary>
        /// Create a Task 
        /// </summary>
        /// <returns>The Task</returns>
        ITask Create();
    }
}