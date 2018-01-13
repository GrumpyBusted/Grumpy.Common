using System;
using System.Threading;

namespace Grumpy.Common.Interfaces
{
    /// <inheritdoc />
    /// <summary>
    /// Timer Task for running a task at intervals
    /// </summary>
    public interface ITimerTask : IDisposable
    {
        /// <summary>
        /// Start the Timer task
        /// </summary>
        /// <param name="action">Action to execute at intervals</param>
        /// <param name="millisecondsIntervals">Execution interval in Milliseconds</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        void Start(Action action, int millisecondsIntervals, CancellationToken cancellationToken);

        /// <summary>
        /// Stop the timer task
        /// </summary>
        void Stop();
    }
}