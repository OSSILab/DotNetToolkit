/*************************************************************************************
* The MIT License(MIT)                                                               *
*                                                                                    *
* Copyright(c) Open Source Software Initiative Contributors                          *
*                                                                                    *
* Permission is hereby granted, free of charge, to any person obtaining a copy       *
* of this software and associated documentation files (the “Software”), to deal      *
* in the Software without restriction, including without limitation the rights       *
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies   *
* of the Software, and to permit persons to whom the Software is furnished to do so. *
*                                                                                    *
* THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS            *
* OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,        *
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE        *
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,  *
* WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN    *
* CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.         *
*************************************************************************************/

using System.Collections.Generic;
using System.Runtime.Versioning;

namespace System.Threading.Tasks
{
    /// <summary>
    /// A custom task scheduler that handles the low-level work of queuing tasks into one thread.
    /// </summary>
    public class SingleThreadTaskScheduler : TaskScheduler, IDisposable
    {
        private readonly Thread _schedulerThread;
        private readonly List<Task> _tasks = new List<Task>();
        private readonly AutoResetEvent _taskAddedEvent = new AutoResetEvent(false);
        private readonly object _modifyTasksLock = new object();

        /// <summary>The name of the thread used by the current scheduler.</summary>
        public string ThreadName
        {
            get { return _schedulerThread.Name; }
        }

        /// <summary>Indicating the ApartmentState of the current scheduler.</summary>
        public ApartmentState ApartmentState
        {
            get { return _schedulerThread.GetApartmentState(); }
        }

        /// <summary>Initializes a new instance of the <see cref="SingleThreadTaskScheduler"/> class.</summary>
        public SingleThreadTaskScheduler() : this(null, null) { }

        /// <summary>Initializes a new instance of the <see cref="SingleThreadTaskScheduler"/> class.</summary>
        /// <param name="threadName">The name of the thread used by the current scheduler.</param>
        public SingleThreadTaskScheduler(string threadName) : this(threadName, null) { }

        /// <summary>Initializes a new instance of the <see cref="SingleThreadTaskScheduler"/> class.</summary>
        /// <param name="apartmentState">Indicating the ApartmentState of the current scheduler.</param>
#if NET5_0_OR_GREATER
        [SupportedOSPlatform("windows")]
#endif
        public SingleThreadTaskScheduler(ApartmentState apartmentState) : this(null, new ApartmentState?(apartmentState)) { }

        // <summary>Initializes a new instance of the <see cref="SingleThreadTaskScheduler"/> class.</summary>
        /// <param name="threadName">The name of the thread used by the current scheduler.</param>
        /// <param name="apartmentState">Indicating the ApartmentState of the current scheduler.</param>
#if NET5_0_OR_GREATER
        [SupportedOSPlatform("windows")]
#endif
        public SingleThreadTaskScheduler(string threadName, ApartmentState apartmentState) : this(threadName, new ApartmentState?(apartmentState)) { }


        private SingleThreadTaskScheduler(string threadName, ApartmentState? apartmentState)
        {
            _schedulerThread = new Thread(RunSchedulerJob);
            if (apartmentState != null)
            {
#pragma warning disable IDE0079
#pragma warning disable CA1416
                _schedulerThread.SetApartmentState(apartmentState.Value);
#pragma warning restore CA1416
#pragma warning restore IDE0079
            }

            if (!string.IsNullOrWhiteSpace(threadName))
            {
                _schedulerThread.Name = threadName;
            }
            _schedulerThread.Start();
        }


        /// <inheritdoc />
        protected override void QueueTask(Task task)
        {
            ThrowExceptionIfDisposed();
            lock (_modifyTasksLock)
            {
                if (Thread.CurrentThread == _schedulerThread)
                {
                    TryExecuteTask(task);
                    return;
                }
                _tasks.Add(task);
            }
            _taskAddedEvent.Set();
        }

        /// <inheritdoc />
        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            return false;
        }

        /// <inheritdoc />
        protected override IEnumerable<Task> GetScheduledTasks()
        {
            lock (_modifyTasksLock)
            {
                return _tasks.ToArray();
            }
        }

        /// <inheritdoc />
        protected override bool TryDequeue(Task task)
        {
            lock (_modifyTasksLock)
            {
                return _tasks.Remove(task);
            }
        }

        private Task DequeueTask()
        {
            lock (_modifyTasksLock)
            {
                if (_tasks.Count > 0)
                {
                    Task task = _tasks[0];
                    _tasks.RemoveAt(0);
                    return task;
                }
            }
            return null;
        }

        private void RunSchedulerJob()
        {
            while (!_disposed)
            {
                try
                {
                    _taskAddedEvent.WaitOne();
                }
                catch (ObjectDisposedException)
                {
                    break;
                }

                while (!_disposed)
                {
                    Task nextTaskToRun = DequeueTask();
                    if (nextTaskToRun == null)
                    {
                        break;
                    }
                    else
                    {
                        TryExecuteTask(nextTaskToRun);
                    }
                }
            }
        }

        #region IDisposable Support
        private volatile bool _disposed; // To detect redundant calls

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;

                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _taskAddedEvent.Dispose();

                    try
                    {
                        _taskAddedEvent.Set();//required in case someone is already waiting for event it before calling dispose
                    }
                    catch (ObjectDisposedException) { }
                    _taskAddedEvent.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
            }
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        private void ThrowExceptionIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("Scheduler is disposed");
            }
        }
        #endregion
    }
}