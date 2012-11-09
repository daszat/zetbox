// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.API.Async
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;

    public enum ZbTaskState
    {
        /// <summary>
        /// In this state, the task is currently waiting for something (usually another task, or its own initialisation) before starting to run.
        /// </summary>
        Waiting = 0,
        /// <summary>
        /// In this state, the task is currently executing somewhere.
        /// </summary>
        Running,
        /// <summary>
        /// The core task is finished, but some async continuations are still running.
        /// </summary>
        AsyncContinuationsRunning,
        /// <summary>
        /// The core task and continuations are finished, and the result processing was triggered, but has not started yet.
        /// </summary>
        ResultEventsPosted,
        /// <summary>
        /// The core task and continuations are finished, but the result processing still running.
        /// </summary>
        ResultEventRunning,
        /// <summary>
        /// The task has completely finished running and processing results.
        /// </summary>
        Finished
    }

    public class ZbTask
    {
        public static readonly SynchronizationContext Synchron = null;
        public static readonly ZbTask NoInnerTask = null;

        private readonly object _lockObject = new object();
        protected object SyncRoot { get { return _lockObject; } }

        protected readonly List<Action> asyncContinuationActions = new List<Action>();
        protected readonly List<Action> resultActions = new List<Action>();
        protected readonly ZbTask innerZbTask;

        public ZbTaskState State
        {
            get;
            protected set;
        }

        private readonly SynchronizationContext _syncContext;
        public SynchronizationContext SyncContext
        {
            get { return _syncContext; }
        }

        public object GetAwaiter()
        {
            throw new NotImplementedException("Will be implemented when switching to .net 4.5; or replaced by using await directly");
        }

        /// <summary>
        /// This constructor only initializes the internal state of the ZbTask, without starting any execution.
        /// </summary>
        protected ZbTask(SynchronizationContext syncContext, ZbTask innerTask)
        {
            if (syncContext != null && innerTask != null && syncContext != innerTask._syncContext) throw new ArgumentOutOfRangeException("syncContext", "SynchronizationContext differs between this and inner Task");

            this._syncContext = syncContext ?? (innerTask != null ? innerTask._syncContext : null);
            this.innerZbTask = innerTask;
        }

        protected void ExecuteOrChainTask(Action task)
        {
            if (innerZbTask != null)
            {
                if (task == null)
                {
                    // attach to innerZbTask directly
                    innerZbTask
                        .ContinueWith(t =>
                        {
                            // Set State to Running for completeness' sake
                            lock (_lockObject) State = ZbTaskState.Running;
                            // but since this task has nothing to do, execute continuations immediately
                            CallAsyncContinuations();
                            lock (_lockObject) State = ZbTaskState.ResultEventsPosted;
                        })
                        .OnResult(t => CallResultActions());
                }
                else
                {
                    // has to be deferred until the innerZbTask has run
                    innerZbTask
                        .OnResult(t => ExecuteTask(task));
                }
            }
            else
            {
                ExecuteTask(task);
            }
        }

        private void ExecuteTask(Action task)
        {
            lock (_lockObject) State = ZbTaskState.Running;
            Void work = () =>
            {
                if (task != null) task();
                CallAsyncContinuations();
            };
            if (_syncContext != null)
            {
                ThreadPool.QueueUserWorkItem(tpState =>
                {
                    work();
                    lock (_lockObject)
                    {
                        State = ZbTaskState.ResultEventsPosted;
                        if (IsWaiting > 0)
                        {
                            Monitor.PulseAll(_lockObject);
                        }
                        else
                        {
                            _syncContext.Post(scState => CallResultActions(), null);
                        }
                    }
                });
            }
            else
            {
                work();
                lock (_lockObject)
                {
                    State = ZbTaskState.ResultEventsPosted;
                    if (IsWaiting > 0)
                    {
                        Monitor.PulseAll(_lockObject);
                    }
                    else
                    {
                        CallResultActions();
                    }
                }
            }
        }

        public ZbTask(Action task)
            : this(SynchronizationContext.Current, NoInnerTask)
        {
            ExecuteOrChainTask(task);
        }

        public ZbTask(ZbTask innerTask)
            : this(innerTask != null ? innerTask._syncContext : Synchron, innerTask)
        {
            ExecuteOrChainTask(null);
        }

        public ZbTask(ZbTask innerTask, Action task)
            : this(innerTask != null ? innerTask._syncContext : Synchron, innerTask)
        {
            ExecuteOrChainTask(task);
        }

        public ZbTask(SynchronizationContext syncContext, Action task)
            : this(syncContext, NoInnerTask)
        {
            ExecuteOrChainTask(task);
        }

        delegate void Void();

        /// <summary>
        /// Number of threads waiting for the results
        /// </summary>
        public int IsWaiting
        {
            get;
            private set;
        }

        public ZbTask ContinueWith(Action<ZbTask> continuationAction)
        {
            if (continuationAction == null) throw new ArgumentNullException("continuationAction");

            lock (_lockObject)
            {
                switch (State)
                {
                    case ZbTaskState.Waiting:
                    case ZbTaskState.Running:
                        asyncContinuationActions.Add(() => continuationAction(this));
                        break;
                    case ZbTaskState.AsyncContinuationsRunning:
                    case ZbTaskState.ResultEventsPosted:
                    case ZbTaskState.ResultEventRunning:
                    case ZbTaskState.Finished:
                        continuationAction(this);
                        break;
                }
            }
            return this;
        }

        public ZbTask OnResult(Action<ZbTask> continuationAction)
        {
            if (continuationAction == null) throw new ArgumentNullException("continuationAction");

            lock (_lockObject)
            {
                switch (State)
                {
                    case ZbTaskState.Waiting:
                    case ZbTaskState.Running:
                    case ZbTaskState.AsyncContinuationsRunning:
                    case ZbTaskState.ResultEventsPosted:
                        resultActions.Add(() => continuationAction(this));
                        break;
                    case ZbTaskState.ResultEventRunning:
                    case ZbTaskState.Finished:
                        continuationAction(this);
                        break;
                }
            }
            return this;
        }

        public void Wait()
        {
            if (innerZbTask != null) innerZbTask.Wait();
            CallResultActions();
        }

        protected void CallAsyncContinuations()
        {
            lock (_lockObject) State = ZbTaskState.AsyncContinuationsRunning;
            foreach (var action in asyncContinuationActions)
            {
                action();
            }
        }

        protected void CallResultActions()
        {
            lock (_lockObject)
            {
            RECHECK:
                switch (State)
                {
                    case ZbTaskState.Waiting:
                    case ZbTaskState.Running:
                    case ZbTaskState.AsyncContinuationsRunning:
                        IsWaiting += 1;
                        Monitor.Wait(_lockObject);
                        IsWaiting -= 1;
                        // something happened: we have to decide whether we are the
                        // "lucky" thread to continue execution or whether someone else
                        // already did the work for us
                        goto RECHECK;
                    case ZbTaskState.ResultEventsPosted:
                        State = ZbTaskState.ResultEventRunning;
                        break;
                    case ZbTaskState.ResultEventRunning:
                    case ZbTaskState.Finished:
                        return;
                }
            }

            foreach (var action in resultActions)
            {
                action();
            }

            lock (_lockObject) State = ZbTaskState.Finished;
        }
    }

    public class ZbTask<TResult> : ZbTask
    {
        protected void ExecuteOrChainTask(Func<TResult> task)
        {
            ExecuteOrChainTask(() =>
            {
                if (task != null)
                    this._result = task();
            });
        }

        /// <summary>
        /// This constructor only initializes the internal state of the ZbTask, without starting any execution.
        /// </summary>
        protected ZbTask(SynchronizationContext syncContext, ZbTask innerTask)
            : base(syncContext, innerTask)
        {
        }

        public ZbTask(Func<TResult> task)
            : base(SynchronizationContext.Current, NoInnerTask)
        {
            ExecuteOrChainTask(task);
        }

        public ZbTask(ZbTask innerTask)
            : base(innerTask)
        {
            // ExecuteOrChainTask is called by base constructor
        }

        public ZbTask(ZbTask innerTask, Func<TResult> task)
            : base(innerTask != null ? innerTask.SyncContext : Synchron, innerTask)
        {
            ExecuteOrChainTask(task);
        }

        public ZbTask(SynchronizationContext syncContext, Func<TResult> task)
            : base(syncContext, NoInnerTask)
        {
            ExecuteOrChainTask(task);
        }

        public ZbTask(TResult result)
            : base(ZbTask.Synchron, NoInnerTask)
        {
            _result = result;
            State = ZbTaskState.Finished;
        }

        public ZbTask<TResult> ContinueWith(Action<ZbTask<TResult>> continuationAction)
        {
            if (continuationAction == null) throw new ArgumentNullException("continuationAction");

            ContinueWith((ZbTask _) => continuationAction(this));
            return this;
        }

        public ZbTask<TResult> OnResult(Action<ZbTask<TResult>> continuationAction)
        {
            if (continuationAction == null) throw new ArgumentNullException("continuationAction");

            OnResult((ZbTask _) => continuationAction(this));
            return this;
        }

        private TResult _result;
        public TResult Result
        {
            get
            {
                Wait();
                return _result;
            }
            set
            {
                _result = value;
            }
        }
    }

    /// <summary>
    /// This class executes after a Task that can be only created in the future. Then it proceeds as normal.
    /// </summary>
    /// <typeparam name="TIntermediate">The result type of the future task</typeparam>
    /// <typeparam name="TResult">The final result type</typeparam>
    public sealed class ZbFutureTask<TIntermediate, TResult> : ZbTask<TResult>
    {
        /// <summary>
        /// Initialize the future task.
        /// </summary>
        /// <param name="innerTaskFactory">The task which will create the task we'll be acting on.</param>
        /// <param name="task">the transformation we have to do.</param>
        public ZbFutureTask(ZbTask<ZbTask<TIntermediate>> innerTaskFactory, Func<TIntermediate, TResult> task)
            : base(innerTaskFactory != null ? innerTaskFactory.SyncContext : Synchron, innerTaskFactory)
        {
            if (innerTaskFactory == null) throw new ArgumentNullException("innerTaskFactory");

            innerTaskFactory
                .OnResult(
                    (ZbTask<ZbTask<TIntermediate>> factory) =>
                        factory.Result.OnResult(
                            t =>
                            {
                                ExecuteOrChainTask(() =>
                                    {
                                        if (task != null)
                                            task(t.Result);
                                    });
                            }));
        }
    }
}
