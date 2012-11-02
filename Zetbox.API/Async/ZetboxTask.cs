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
        protected readonly object lockObj = new object();
        protected readonly List<Action> asyncContinuationActions = new List<Action>();
        protected readonly List<Action> resultActions = new List<Action>();
        protected readonly SynchronizationContext syncContext;
        protected Action task;
        protected readonly ZbTask innerZbTask;

        public static readonly SynchronizationContext Synchron = null;

        public ZbTaskState State
        {
            get;
            protected set;
        }

        public object GetAwaiter()
        {
            throw new NotImplementedException("Will be implemented when switching to .net 4.5; or replaced by using await directly");
        }

        protected ZbTask(SynchronizationContext syncContext)
        {
            this.syncContext = syncContext;
            innerZbTask = null;
        }

        public ZbTask(Action task)
            : this(SynchronizationContext.Current, task)
        {
        }

        public ZbTask(ZbTask task)
            : this(task != null ? task.syncContext : null)
        {
            if (task != null)
            {
                innerZbTask = task;
                innerZbTask
                    .ContinueWith(t =>
                    {
                        CallAsyncContinuations();
                        lock (lockObj) State = ZbTaskState.ResultEventsPosted;
                    })
                    .OnResult(t => CallResultActions());
            }
            else
            {
                ExecuteTask(() => { });
            }
        }

        public ZbTask(SynchronizationContext syncContext, Action task)
            : this(syncContext)
        {
            ExecuteTask(task);
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

        protected void ExecuteTask(Action task)
        {
            lock (lockObj) State = ZbTaskState.Running;
            Void work = () =>
            {
                if (task != null) task();
                CallAsyncContinuations();
            };
            if (syncContext != null)
            {
                ThreadPool.QueueUserWorkItem(tpState =>
                {
                    work();
                    lock (lockObj)
                    {
                        State = ZbTaskState.ResultEventsPosted;
                        if (IsWaiting > 0)
                        {
                            Monitor.PulseAll(lockObj);
                        }
                        else
                        {
                            syncContext.Post(scState => CallResultActions(), null);
                        }
                    }
                });
            }
            else
            {
                work();
                lock (lockObj)
                {
                    State = ZbTaskState.ResultEventsPosted;
                    if (IsWaiting > 0)
                    {
                        Monitor.PulseAll(lockObj);
                    }
                    else
                    {
                        CallResultActions();
                    }
                }
            }
        }

        public ZbTask ContinueWith(Action<ZbTask> continuationAction)
        {
            lock (lockObj)
            {
                switch (State)
                {
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
            lock (lockObj)
            {
                switch (State)
                {
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

        public ZbTask Wait()
        {
            if (innerZbTask != null) innerZbTask.Wait();
            CallResultActions();
            return this;
        }

        protected void CallAsyncContinuations()
        {
            lock (lockObj) State = ZbTaskState.AsyncContinuationsRunning;
            foreach (var action in asyncContinuationActions)
            {
                action();
            }
        }

        protected void CallResultActions()
        {
            lock (lockObj)
            {
            RECHECK:
                switch (State)
                {
                    case ZbTaskState.Running:
                    case ZbTaskState.AsyncContinuationsRunning:
                        IsWaiting += 1;
                        Monitor.Wait(lockObj);
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

            lock (lockObj) State = ZbTaskState.Finished;
        }
    }

    public class ZbTask<TResult> : ZbTask
    {
        public ZbTask(ZbTask task)
            : base(task)
        {
        }

        public ZbTask(Func<TResult> task)
            : this(SynchronizationContext.Current, task)
        {
        }

        public ZbTask(TResult result)
            : base(ZbTask.Synchron)
        {
            _result = result;
            State = ZbTaskState.Finished;
        }

        public ZbTask(SynchronizationContext syncContext, Func<TResult> task)
            : base(syncContext)
        {
            ExecuteTask(() => this._result = task());
        }

        public ZbTask<TResult> ContinueWith(Action<ZbTask<TResult>> continuationAction)
        {
            ContinueWith((ZbTask _) => continuationAction(this));
            return this;
        }

        public ZbTask<TResult> OnResult(Action<ZbTask<TResult>> continuationAction)
        {
            OnResult((ZbTask _) => continuationAction(this));
            return this;
        }

        public new ZbTask<TResult> Wait()
        {
            base.Wait();
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
}
