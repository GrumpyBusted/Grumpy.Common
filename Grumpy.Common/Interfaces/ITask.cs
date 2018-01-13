using System;
using System.Threading;

namespace Grumpy.Common.Interfaces
{
    /// <inheritdoc />
    /// <summary>
    /// Async Task - This can be used in classes that need a task and need to be able to stub this in unit test cases
    /// </summary>
    public interface ITask : IDisposable
    {
        /// <summary>
        /// StartSync the Task
        /// </summary>
        /// <param name="action">Action to execute in Task</param>
        void Start(Action action);

        /// <summary>
        /// Start the Task
        /// </summary>
        /// <param name="action">Action to execute in Task</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        void Start(Action action, CancellationToken cancellationToken);

        /// <summary>
        /// Start the Task
        /// </summary>
        /// <param name="action">Action to execute in Task</param>
        /// <param name="state">Input state/object</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        void Start(Action<object> action, object state, CancellationToken cancellationToken);

        /// <summary>
        /// Wait for Task for complete
        /// </summary>
        bool Wait();

        /// <summary>
        /// Stop the Task
        /// </summary>
        void Stop();

        /// <summary>
        /// Indicate if the task ran to completion
        /// </summary>
        /// <returns></returns>
        bool IsCompleted { get; }

        /// <summary>
        /// Indicate if the task failed
        /// </summary>
        /// <returns></returns>
        bool IsFaulted { get; }

        /// <summary>
        /// State/Input object of the task
        /// </summary>
        /// <returns></returns>
        object AsyncState { get; }

        /// <summary>
        /// Return the Exception of failed task 
        /// </summary>
        /// <returns></returns>
        Exception Exception { get; }
    }
}