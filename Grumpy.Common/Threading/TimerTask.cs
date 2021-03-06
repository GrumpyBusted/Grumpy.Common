﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Grumpy.Common.Interfaces;

namespace Grumpy.Common.Threading
{
    /// <inheritdoc />
    [SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
    public class TimerTask : ITimerTask
    {
        private CancellationTokenSource _cancellationTokenSource;
        private CancellationTokenRegistration _cancellationTokenRegistration;
        private bool _disposed;

        /// <inheritdoc />
        public void Start(Action action, int millisecondsIntervals, CancellationToken cancellationToken)
        {
            if (_cancellationTokenSource != null)
                throw new ArgumentException("Cannot Start Already running TimerTask");

            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationTokenRegistration = cancellationToken.Register(Stop);

            System.Threading.Tasks.Task.Run(() => Process(action, millisecondsIntervals), _cancellationTokenSource.Token);
        }

        private void Process(Action action, int millisecondsIntervals)
        {
            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                action();

                _cancellationTokenSource.Token.WaitHandle.WaitOne(millisecondsIntervals);
            }
        }

        /// <inheritdoc />
        public void Stop()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenRegistration.Dispose();
            _cancellationTokenSource = null;
        }

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