using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Grumpy.Common.Interfaces;
using Grumpy.Common.Extensions;

namespace Grumpy.Common.Threading
{
    /// <inheritdoc />
    [SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
    public class Task : ITask
    {
        private readonly System.Threading.Tasks.TaskFactory _taskFactory;
        private CancellationTokenSource _cancellationTokenSource;
        private CancellationTokenRegistration _cancellationTokenRegistration;
        private System.Threading.Tasks.Task _task;
        private bool _disposed;

        /// <inheritdoc />
        public Task(System.Threading.Tasks.TaskFactory taskFactory)
        {
            _taskFactory = taskFactory;
        }

        /// <inheritdoc />
        public void Start(Action action)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            _task = _taskFactory.StartNew(action, _cancellationTokenSource.Token);
        }

        /// <inheritdoc />
        public void Start(Action action, CancellationToken cancellationToken)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationTokenRegistration = cancellationToken.Register(Stop);

            _task = _taskFactory.StartNew(action, _cancellationTokenSource.Token);
        }

        /// <inheritdoc />
        public void Start(Action<object> action, object state, CancellationToken cancellationToken)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationTokenRegistration = cancellationToken.Register(Stop);

            _task = _taskFactory.StartNew(action, state, _cancellationTokenSource.Token);
        }

        /// <inheritdoc />
        public void Stop()
        {
            if (_cancellationTokenSource != null)
            {
                lock (_cancellationTokenSource)
                {
                    if (!_cancellationTokenSource.IsCancellationRequested)
                        _cancellationTokenSource.Cancel();

                    _cancellationTokenSource?.Dispose();
                    _cancellationTokenRegistration.Dispose();
                    _cancellationTokenSource = null;
                }

                if (_task != null && _task.Status.In(TaskStatus.RanToCompletion, TaskStatus.Canceled, TaskStatus.Faulted))
                    _task.Dispose();
            }
        }

        /// <inheritdoc />
        public bool Wait()
        {
            if (_task != null)
            {
                try
                {
                    _task.Wait(_cancellationTokenSource.Token);
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        /// <inheritdoc />
        public bool IsCompleted => _task?.IsCompleted ?? false;

        /// <inheritdoc />
        public bool IsFaulted => _task?.IsFaulted ?? false;

        /// <inheritdoc />
        public object AsyncState => _task?.AsyncState;

        /// <inheritdoc />
        public Exception Exception => _task?.Exception;

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Dispose locale objects
        /// </summary>
        /// <param name="disposing"></param>
        [SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed")]  
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    Stop();

                _disposed = true;
            }
        }
    }
}