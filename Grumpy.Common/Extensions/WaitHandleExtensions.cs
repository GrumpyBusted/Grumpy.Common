using System.Threading;
using System.Threading.Tasks;

namespace Grumpy.Common.Extensions
{
    /// <summary>
    /// Extensions method for WaitHandle
    /// </summary>
    public static class WaitHandleExtensions
    {
        /// <summary>
        /// Type case a Wait Handle to a task to be use for asynchronous programming
        /// </summary>
        /// <param name="handle">Wait handle</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task</returns>
        public static Task ToTask(this WaitHandle handle, CancellationToken cancellationToken)
        {
            var taskCompletionSource = new TaskCompletionSource<object>();

            cancellationToken.Register(() => { taskCompletionSource.TrySetCanceled(); });

            var localVariableInitLock = new object();

            lock (localVariableInitLock)
            {
                RegisteredWaitHandle[] callbackHandle = {null};

                callbackHandle[0] = ThreadPool.RegisterWaitForSingleObject(handle, (state, timedOut) =>
                {
                    taskCompletionSource.TrySetResult(null);
                    lock (localVariableInitLock)
                    {
                        callbackHandle[0].Unregister(null); 
                    }
                }, null, Timeout.Infinite, true);
            }

            return taskCompletionSource.Task;
        }
    }
}