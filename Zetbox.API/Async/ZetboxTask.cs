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
        protected readonly object _lock = new object();
        protected readonly List<Action> _asyncContinuationActions = new List<Action>();
        protected readonly List<Action> _resultActions = new List<Action>();
        protected readonly SynchronizationContext _syncContext;
        protected Action _task;

        public ZbTaskState State
        {
            get;
            private set;
        }

        public object GetAwaiter()
        {
            throw new NotImplementedException("Will be implemented when switching to .net 4.5; or replaced by using await directly");
        }

        protected ZbTask(SynchronizationContext syncContext)
        {
            _syncContext = syncContext;
        }

        public ZbTask(Action task)
            : this(SynchronizationContext.Current, task)
        {
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
            if (task != null)
            {
                lock (_lock) State = ZbTaskState.Running;
                Void work = () =>
                {
                    task();
                    CallAsyncContinuations();
                };
                if (_syncContext != null)
                {
                    ThreadPool.QueueUserWorkItem(tpState =>
                    {
                        work();
                        lock (_lock)
                        {
                            State = ZbTaskState.ResultEventsPosted;
                            if (IsWaiting > 0)
                            {
                                Monitor.PulseAll(_lock);
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
                    lock (_lock)
                    {
                        State = ZbTaskState.ResultEventsPosted;
                        if (IsWaiting > 0)
                        {
                            Monitor.PulseAll(_lock);
                        }
                        else
                        {
                            CallResultActions();
                        }
                    }
                }
            }
        }

        public ZbTask ContinueWith(Action<ZbTask> continuationAction)
        {
            lock (_lock)
            {
                switch (State)
                {
                    case ZbTaskState.Running:
                        _asyncContinuationActions.Add(() => continuationAction(this));
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
            lock (_lock)
            {
                switch (State)
                {
                    case ZbTaskState.Running:
                    case ZbTaskState.AsyncContinuationsRunning:
                    case ZbTaskState.ResultEventsPosted:
                        _resultActions.Add(() => continuationAction(this));
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
            CallResultActions();
        }

        protected void CallAsyncContinuations()
        {
            lock (_lock) State = ZbTaskState.AsyncContinuationsRunning;
            foreach (var action in _asyncContinuationActions)
            {
                action();
            }
        }

        protected void CallResultActions()
        {
            lock (_lock)
            {
            RECHECK:
                switch (State)
                {
                    case ZbTaskState.Running:
                    case ZbTaskState.AsyncContinuationsRunning:
                        IsWaiting += 1;
                        Monitor.Wait(_lock);
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

            foreach (var action in _resultActions)
            {
                action();
            }

            lock (_lock) State = ZbTaskState.Finished;
        }
    }

    public class ZbTask<TResult> : ZbTask
    {
        public ZbTask(Func<TResult> task)
            : this(SynchronizationContext.Current, task)
        {
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

        private TResult _result;
        public TResult Result
        {
            get
            {
                Wait();
                return _result;
            }
        }
    }
}
