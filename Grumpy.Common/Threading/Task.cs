using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Grumpy.Common.Interfaces;
using Grumpy.Common.Extensions;

namespace Grumpy.Common.Threading
{
    [SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
    public class Task : ITask
    {
        private readonly System.Threading.Tasks.TaskFactory _taskFactory;
        private CancellationTokenSource _cancellationTokenSource;
        private CancellationTokenRegistration _cancellationTokenRegistration;
        private System.Threading.Tasks.Task _task;
        private bool _disposed;

        public Task(System.Threading.Tasks.TaskFactory taskFactory)
        {
            _taskFactory = taskFactory;
        }

        public void Start(Action action)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            _task = _taskFactory.StartNew(action, _cancellationTokenSource.Token);
        }

        public void Start(Action action, CancellationToken cancellationToken)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationTokenRegistration = cancellationToken.Register(Stop);

            _task = _taskFactory.StartNew(action, _cancellationTokenSource.Token);
        }

        public void Start(Action<object> action, object state, CancellationToken cancellationToken)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationTokenRegistration = cancellationToken.Register(Stop);

            _task = _taskFactory.StartNew(action, state, _cancellationTokenSource.Token);
        }

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

        public bool IsCompleted => _task?.IsCompleted ?? false;

        public bool IsFaulted => _task?.IsFaulted ?? false;

        public object AsyncState => _task?.AsyncState;

        public Exception Exception => _task?.Exception;

        public void Dispose()
        {
            Dispose(true);
        }

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