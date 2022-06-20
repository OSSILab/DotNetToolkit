/*********************************************************************************
* The MIT License(MIT)                                                           *
*                                                                                *
* Copyright(c) Open Source Software Initiative Contributors                      *
*                                                                                *
* Permission is hereby granted, free of charge, to any person obtaining a copy   *
* of this software and associated documentation files (the "Software"), to deal  *
* in the Software without restriction, including without limitation the rights   *
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell      *
* copies of the Software, and to permit persons to whom the Software is          *
* furnished to do so, subject to the following conditions:                       *
*                                                                                *
* The above copyright notice and this permission notice shall be included in all *
* copies or substantial portions of the Software.                                *
*                                                                                *
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR     *
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,       *
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE    *
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER         *
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,  *
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE  *
* SOFTWARE.                                                                      *
*********************************************************************************/

using System.Runtime.Versioning;

namespace System.Threading.Tasks
{
    /// <summary>
    /// Provides support for creating and scheduling <see cref="Task"/> objects on a <see cref="SingleThreadTaskScheduler"/> scheduler object.
    /// </summary>
    public class SingleThreadTaskFactory : TaskFactory, IDisposable
    {
        /// <summary>Initializes a new instance of the <see cref="SingleThreadTaskFactory"/> class.</summary>
        public SingleThreadTaskFactory() : base(new SingleThreadTaskScheduler()) { }

        /// <summary>Initializes a new instance of the <see cref="SingleThreadTaskFactory"/> class.</summary>
        /// <param name="threadName">The name of the thread used by the current scheduler.</param>
        public SingleThreadTaskFactory(string threadName) : base(new SingleThreadTaskScheduler(threadName)) { }

        /// <summary>Initializes a new instance of the <see cref="SingleThreadTaskFactory"/> class.</summary>
        /// <param name="apartmentState">Indicating the ApartmentState of the current scheduler.</param>
#if NET5_0_OR_GREATER
        [SupportedOSPlatform("windows")]
#endif
        public SingleThreadTaskFactory(ApartmentState apartmentState) : base(new SingleThreadTaskScheduler(apartmentState)) { }

        /// <summary>Initializes a new instance of the <see cref="SingleThreadTaskFactory"/> class.</summary>
        /// <param name="threadName">The name of the thread used by the current scheduler.</param>
        /// <param name="apartmentState">Indicating the ApartmentState of the current scheduler.</param>
#if NET5_0_OR_GREATER
        [SupportedOSPlatform("windows")]
#endif
        public SingleThreadTaskFactory(string threadName, ApartmentState apartmentState) : base(new SingleThreadTaskScheduler(threadName, apartmentState)) { }

        /// <summary>Initializes a new instance of the <see cref="SingleThreadTaskFactory"/> class.</summary>
        /// <param name="cancellationToken">The default <see cref="CancellationToken"/> that will be
        /// assigned to tasks created by this <see cref="SingleThreadTaskFactory"/> unless another
        /// CancellationToken is explicitly specified while calling the factory methods.</param>
        public SingleThreadTaskFactory(CancellationToken cancellationToken) : base(cancellationToken, TaskCreationOptions.None, TaskContinuationOptions.None, new SingleThreadTaskScheduler()) { }

        /// <summary>Initializes a new instance of the <see cref="SingleThreadTaskFactory"/> class.</summary>
        /// <param name="cancellationToken">The default <see cref="CancellationToken"/> that will be
        /// assigned to tasks created by this <see cref="SingleThreadTaskFactory"/> unless another
        /// CancellationToken is explicitly specified while calling the factory methods.</param>
        /// <param name="threadName">The name of the thread used by the current scheduler.</param>
        public SingleThreadTaskFactory(CancellationToken cancellationToken, string threadName) : base(cancellationToken, TaskCreationOptions.None, TaskContinuationOptions.None, new SingleThreadTaskScheduler(threadName)) { }

        /// <summary>Initializes a new instance of the <see cref="SingleThreadTaskFactory"/> class.</summary>
        /// <param name="cancellationToken">The default <see cref="CancellationToken"/> that will be
        /// assigned to tasks created by this <see cref="SingleThreadTaskFactory"/> unless another
        /// CancellationToken is explicitly specified while calling the factory methods.</param>
        /// <param name="apartmentState">Indicating the ApartmentState of the current scheduler.</param>
#if NET5_0_OR_GREATER
        [SupportedOSPlatform("windows")]
#endif
        public SingleThreadTaskFactory(CancellationToken cancellationToken, ApartmentState apartmentState) : base(cancellationToken, TaskCreationOptions.None, TaskContinuationOptions.None, new SingleThreadTaskScheduler(apartmentState)) { }

        /// <summary>Initializes a new instance of the <see cref="SingleThreadTaskFactory"/> class.</summary>
        /// <param name="cancellationToken">The default <see cref="CancellationToken"/> that will be
        /// assigned to tasks created by this <see cref="SingleThreadTaskFactory"/> unless another
        /// CancellationToken is explicitly specified while calling the factory methods.</param>
        /// <param name="threadName">The name of the thread used by the current scheduler.</param>
        /// <param name="apartmentState">Indicating the ApartmentState of the current scheduler.</param>
#if NET5_0_OR_GREATER
        [SupportedOSPlatform("windows")]
#endif
        public SingleThreadTaskFactory(CancellationToken cancellationToken, string threadName, ApartmentState apartmentState) : base(cancellationToken, TaskCreationOptions.None, TaskContinuationOptions.None, new SingleThreadTaskScheduler(threadName, apartmentState)) { }

        /// <summary>Initializes a new instance of the <see cref="SingleThreadTaskFactory"/> class.</summary>
        /// <param name="taskCreationOptions">The default <see cref="TaskCreationOptions"/> to use when creating tasks with this TaskFactory.</param>
        /// <param name="taskContinuationOptions">The default <see cref="TaskContinuationOptions"/> to use when creating continuation tasks with this TaskFactory.</param>
        public SingleThreadTaskFactory(TaskCreationOptions taskCreationOptions, TaskContinuationOptions taskContinuationOptions) : base(default, taskCreationOptions, taskContinuationOptions, new SingleThreadTaskScheduler()) { }

        /// <summary>Initializes a new instance of the <see cref="SingleThreadTaskFactory"/> class.</summary>
        /// <param name="taskCreationOptions">The default <see cref="TaskCreationOptions"/> to use when creating tasks with this TaskFactory.</param>
        /// <param name="taskContinuationOptions">The default <see cref="TaskContinuationOptions"/> to use when creating continuation tasks with this TaskFactory.</param>
        /// <param name="threadName">The name of the thread used by the current scheduler.</param>
        public SingleThreadTaskFactory(TaskCreationOptions taskCreationOptions, TaskContinuationOptions taskContinuationOptions, string threadName) : base(default, taskCreationOptions, taskContinuationOptions, new SingleThreadTaskScheduler(threadName)) { }

        /// <summary>Initializes a new instance of the <see cref="SingleThreadTaskFactory"/> class.</summary>
        /// <param name="taskCreationOptions">The default <see cref="TaskCreationOptions"/> to use when creating tasks with this TaskFactory.</param>
        /// <param name="taskContinuationOptions">The default <see cref="TaskContinuationOptions"/> to use when creating continuation tasks with this TaskFactory.</param>
        /// <param name="apartmentState">Indicating the ApartmentState of the current scheduler.</param>
#if NET5_0_OR_GREATER
        [SupportedOSPlatform("windows")]
#endif
        public SingleThreadTaskFactory(TaskCreationOptions taskCreationOptions, TaskContinuationOptions taskContinuationOptions, ApartmentState apartmentState) : base(default, taskCreationOptions, taskContinuationOptions, new SingleThreadTaskScheduler(apartmentState)) { }

        /// <summary>Initializes a new instance of the <see cref="SingleThreadTaskFactory"/> class.</summary>
        /// <param name="taskCreationOptions">The default <see cref="TaskCreationOptions"/> to use when creating tasks with this TaskFactory.</param>
        /// <param name="taskContinuationOptions">The default <see cref="TaskContinuationOptions"/> to use when creating continuation tasks with this TaskFactory.</param>
        /// <param name="threadName">The name of the thread used by the current scheduler.</param>
        /// <param name="apartmentState">Indicating the ApartmentState of the current scheduler.</param>
#if NET5_0_OR_GREATER
        [SupportedOSPlatform("windows")]
#endif
        public SingleThreadTaskFactory(TaskCreationOptions taskCreationOptions, TaskContinuationOptions taskContinuationOptions, string threadName, ApartmentState apartmentState) : base(default, taskCreationOptions, taskContinuationOptions, new SingleThreadTaskScheduler(threadName, apartmentState)) { }

        /// <summary>Initializes a new instance of the <see cref="SingleThreadTaskFactory"/> class.</summary>
        /// <param name="cancellationToken">The default <see cref="CancellationToken"/> that will be
        /// assigned to tasks created by this <see cref="SingleThreadTaskFactory"/> unless another
        /// CancellationToken is explicitly specified while calling the factory methods.</param>
        /// <param name="taskCreationOptions">The default <see cref="TaskCreationOptions"/> to use when creating tasks with this TaskFactory.</param>
        /// <param name="taskContinuationOptions">The default <see cref="TaskContinuationOptions"/> to use when creating continuation tasks with this TaskFactory.</param>
        public SingleThreadTaskFactory(CancellationToken cancellationToken, TaskCreationOptions taskCreationOptions, TaskContinuationOptions taskContinuationOptions) : base(cancellationToken, taskCreationOptions, taskContinuationOptions, new SingleThreadTaskScheduler()) { }

        /// <summary>Initializes a new instance of the <see cref="SingleThreadTaskFactory"/> class.</summary>
        /// <param name="cancellationToken">The default <see cref="CancellationToken"/> that will be
        /// assigned to tasks created by this <see cref="SingleThreadTaskFactory"/> unless another
        /// CancellationToken is explicitly specified while calling the factory methods.</param>
        /// <param name="taskCreationOptions">The default <see cref="TaskCreationOptions"/> to use when creating tasks with this TaskFactory.</param>
        /// <param name="taskContinuationOptions">The default <see cref="TaskContinuationOptions"/> to use when creating continuation tasks with this TaskFactory.</param>
        /// <param name="threadName">The name of the thread used by the current scheduler.</param>
        public SingleThreadTaskFactory(CancellationToken cancellationToken, TaskCreationOptions taskCreationOptions, TaskContinuationOptions taskContinuationOptions, string threadName) : base(cancellationToken, taskCreationOptions, taskContinuationOptions, new SingleThreadTaskScheduler(threadName)) { }

        /// <summary>Initializes a new instance of the <see cref="SingleThreadTaskFactory"/> class.</summary>
        /// <param name="cancellationToken">The default <see cref="CancellationToken"/> that will be
        /// assigned to tasks created by this <see cref="SingleThreadTaskFactory"/> unless another
        /// CancellationToken is explicitly specified while calling the factory methods.</param>
        /// <param name="taskCreationOptions">The default <see cref="TaskCreationOptions"/> to use when creating tasks with this TaskFactory.</param>
        /// <param name="taskContinuationOptions">The default <see cref="TaskContinuationOptions"/> to use when creating continuation tasks with this TaskFactory.</param>
        /// <param name="apartmentState">Indicating the ApartmentState of the current scheduler.</param>
#if NET5_0_OR_GREATER
        [SupportedOSPlatform("windows")]
#endif
        public SingleThreadTaskFactory(CancellationToken cancellationToken, TaskCreationOptions taskCreationOptions, TaskContinuationOptions taskContinuationOptions, ApartmentState apartmentState) : base(cancellationToken, taskCreationOptions, taskContinuationOptions, new SingleThreadTaskScheduler(apartmentState)) { }

        /// <summary>Initializes a new instance of the <see cref="SingleThreadTaskFactory"/> class.</summary>
        /// <param name="cancellationToken">The default <see cref="CancellationToken"/> that will be
        /// assigned to tasks created by this <see cref="SingleThreadTaskFactory"/> unless another
        /// CancellationToken is explicitly specified while calling the factory methods.</param>
        /// <param name="taskCreationOptions">The default <see cref="TaskCreationOptions"/> to use when creating tasks with this TaskFactory.</param>
        /// <param name="taskContinuationOptions">The default <see cref="TaskContinuationOptions"/> to use when creating continuation tasks with this TaskFactory.</param>
        /// <param name="threadName">The name of the thread used by the current scheduler.</param>
        /// <param name="apartmentState">Indicating the ApartmentState of the current scheduler.</param>
#if NET5_0_OR_GREATER
        [SupportedOSPlatform("windows")]
#endif
        public SingleThreadTaskFactory(CancellationToken cancellationToken, TaskCreationOptions taskCreationOptions, TaskContinuationOptions taskContinuationOptions, string threadName, ApartmentState apartmentState) : base(cancellationToken, taskCreationOptions, taskContinuationOptions, new SingleThreadTaskScheduler(threadName, apartmentState)) { }


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
                    ((SingleThreadTaskScheduler)Scheduler).Dispose();
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
        #endregion
    }
}