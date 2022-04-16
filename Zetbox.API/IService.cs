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

namespace Zetbox.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Zetbox.API.Utils;

    /// <summary>
    /// Interface fo an Zetbox Custom Service
    /// </summary>
    public interface IService
    {
        void Start();
        void Stop();

        string DisplayName { get; }
        string Description { get; }
    }

    public interface IServiceControlManager
    {
        void Start();
        void Stop();
    }

    /// <summary>
    /// A basic service implementation running on a background thread, processing items.
    /// </summary>
    /// <typeparam name="T">the type of item that is processed</typeparam>
    public abstract class ThreadedQueueService<T> : IService
    {
        protected readonly log4net.ILog Log;

        /// <summary>Time to wait before beginning queue processing.</summary>
        /// <remarks>This is used to flatten out the initial spike when all services start up at the same time.</remarks>
        private readonly TimeSpan? _startupDelay;

        /// <summary>Time to wait before retrying a deferred item.</summary>
        /// <remarks>When processing of an item fails temporarily, at least this time will pass before the item is retried.</remarks>
        private readonly TimeSpan? _retryInterval;

        /// <summary>Time to wait before forcing a shutdown.</summary>
        /// <remarks>When shutting down the service, the processing thread gets this grace period before it is forcibly terminated. Pass null to avoid termination at all costs.</remarks>
        private readonly TimeSpan? _shutdownTimeout;

        /// <summary>A queue of currently processing items. Access to this Queue is protected by the _queueLock.</summary>
        private Queue<T> _queue = new Queue<T>();

        /// <summary>A queue of deferred processing items. Access to this Queue is protected by the _queueLock.</summary>
        /// <remarks>Items that create an error on processing are put here. After at least _retryInterval seconds, they're put back onto the _queue, where they'll be processed as usual.</remarks>
        private Queue<T> _deferredQueue = new Queue<T>();

        /// <summary>The lock for accessing the Queues.</summary>
        /// <remarks>Using a single lock for both Queues makes for simpler programming (no deadlock) and it is not expected that there is much contention from the Timer on this lock.</remarks>
        private readonly object _queueLock = new object();

        /// <summary>This is set to signal the _processingThread to shut down.</summary>
        private ManualResetEvent _shutdownEvent;

        /// <summary>This is set to signal the _processingThread that one or more new items are available.</summary>
        private AutoResetEvent _itemsEvent;

        /// <summary>The work happens here.</summary>
        private Thread _processingThread;

        /// <summary>This timer controls retrying deferred items.</summary>
        private Timer _retryTimer;

        /// <summary>
        /// Initialise the service with the specified timeouts.
        /// </summary>
        /// <param name="startupDelay">Time to wait before beginning queue processing.</param>
        /// <param name="retryInterval">Every time this interval elapses, deferred items are retried.</param>
        /// <param name="shutdownTimeout">Time to wait before forcing a shutdown. Pass null if the processing has to finish at all costs.</param>
        protected ThreadedQueueService(TimeSpan? startupDelay = null, TimeSpan? retryInterval = null, TimeSpan? shutdownTimeout = null)
        {
            _startupDelay = startupDelay;
            _retryInterval = retryInterval;
            _shutdownTimeout = shutdownTimeout;

            // use a dynamically scoped logger.
            Log = log4net.LogManager.GetLogger(typeof(ThreadedQueueService<>));
        }

        /// <summary>This method is called when the service is started, before the background thread is started.</summary>
        protected virtual void OnStart() { }

        /// <summary>This method is called when an item is enqueued.</summary>
        /// <remarks>This method runs on the enqueueing thread and is not protected by any locks. It is intended as a hook to allow an implementation a fast-path rejection of items. If a non-silent rejection is required, this method should throw the appropriate exception.</remarks>
        /// <returns>True, if the item should be queued and processed. False, if the item should be silently ignored.</returns>
        protected virtual bool OnEnqueue(T item) { return true; }

        /// <summary>This method is called when the service is stopped, before the background thread is being shut down.</summary>
        protected virtual void OnStop() { }

        /// <summary>This method is called on a separate thread to process a single item.</summary>
        /// <remarks>Exceptions thrown from this method are logged and ignored. If the item is not yet ready or can temporarily not be processed, the method should use Defer(item) to put the item back into the queue.</remarks>
        /// <param name="item">the item to process</param>
        protected abstract Task ProcessItem(T item);

        #region IService Members

        public void Start()
        {
            if (_shutdownEvent != null)
            {
                Log.Warn("Tried to Start() an already running FileImportService. Ignored.");
                return;
            }
            _shutdownEvent = new ManualResetEvent(false);
            _itemsEvent = new AutoResetEvent(false);

            OnStart();

            // start background thread
            _processingThread = new Thread(async () => await ProcessItems());
            _processingThread.Priority = ThreadPriority.BelowNormal;
            _processingThread.IsBackground = _shutdownTimeout.HasValue;
            _processingThread.Start();

            if (_retryInterval.HasValue)
                _retryTimer = new Timer(state => RetryDeferredItems(), null, _startupDelay ?? TimeSpan.Zero, _retryInterval.Value);
        }

        public void Stop()
        {
            OnStop();

            if (_retryTimer != null)
            {
                _retryTimer.Dispose();
                _retryTimer = null;
            }

            // One last time
            RetryDeferredItems();

            // signal the _workerThread to shutdown.
            _shutdownEvent.Set();

            // shortcut waiting for new files. If there are any in the queue, they will still be processed.
            _itemsEvent.Set();

            var processFinished = false;
            while (!processFinished)
            {
                try
                {
                    if (_shutdownTimeout.HasValue && !_processingThread.Join(_shutdownTimeout.Value))
                    {
                        Log.Warn("Cannot abort thread");
                    }
                    else if (!_shutdownTimeout.HasValue)
                    {
                        _processingThread.Join();
                    }
                    processFinished = true;
                }
                catch (ThreadInterruptedException)
                {
                    // try again
                }
            }
            _processingThread = null;
            _itemsEvent.Close();
            _itemsEvent = null;
            _shutdownEvent.Close();
            _shutdownEvent = null;
        }

        public abstract string DisplayName { get; }
        public abstract string Description { get; }

        #endregion

        private async Task ProcessItems()
        {
            // run until signalled, no waiting, fall through to _itemsEvent
            while (!_shutdownEvent.WaitOne(0))
            {
                // wait until items appear or Stop() is called
                if (_itemsEvent.WaitOne(-1))
                {
                    T item;
                    // process all queued files
                    while (TryDequeue(out item))
                    {
                        await ProcessItem(item);
                    }
                }
            }
        }

        private void RetryDeferredItems()
        {
            lock (_queueLock)
            {
                if (_deferredQueue.Count > 0)
                {
                    Log.InfoFormat("Flushing {0} items from the deferred queue to the processing queue", _deferredQueue.Count);
                    while (_deferredQueue.Count > 0)
                        _queue.Enqueue(_deferredQueue.Dequeue());

                    _itemsEvent.Set();
                }
            }
        }

        public void Enqueue(T item)
        {
            if (_itemsEvent == null) return; // service was not started
            if (OnEnqueue(item))
            {
                Log.InfoFormat("Adding '{0}' to queue", item);
                lock (_queueLock)
                {
                    _queue.Enqueue(item);
                }
                _itemsEvent.Set();
            }
        }

        protected void Defer(T item)
        {
            if (!_retryInterval.HasValue)
                Log.WarnOnce("Item deferred until shutdown: no retryInterval specified");

            Log.InfoFormat("Adding '{0}' to deferred queue", item);
            lock (_queueLock)
            {
                _deferredQueue.Enqueue(item);
            }
        }

        private bool TryDequeue(out T result)
        {
            lock (_queueLock)
            {
                if (_queue.Count > 0)
                {
                    result = _queue.Dequeue();
                    return true;
                }
                else
                {
                    result = default(T);
                    return false;
                }
            }
        }
    }
}
