
namespace Zetbox.API.Async
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;

    public class PropertyTask<T>
    {
        private readonly Action _notifier;
        private readonly Func<System.Threading.Tasks.Task<T>> _createTask;
        private readonly Action<T> _set;

        private System.Threading.Tasks.Task<T> _task;
        private T _result;

        public System.Threading.Tasks.Task<T> Task { get { EnsureTask(); return _task; } }

        /// <summary>
        /// Initialize a new instance of the PropertyTask. This struct encapsulates a caching asynchronous access pattern for properties and similar things.
        /// </summary>
        /// <remarks>
        /// createTask
        /// </remarks>
        /// <param name="notifier">This is called when the cached value changes. If it is null, nothing is done.</param>
        /// <param name="createTask">This Func should create a fresh task to fetch the underlying value</param>
        /// <param name="set">This action should set the underlying value immediately. If it is null, Set() will throw a ReadOnlyObjectException</param>
        public PropertyTask(Action notifier, Func<System.Threading.Tasks.Task<T>> createTask, Action<T> set)
        {
            if (createTask == null) throw new ArgumentNullException("createTask");

            this._notifier = notifier;
            this._createTask = createTask;
            this._set = set;
        }

        /// <summary>
        /// Ensures that <code>_task</code> is initialized, running and correctly set up.
        /// </summary>
        private void EnsureTask()
        {
            if (_task != null) return;

            var self = this;
            _task = _createTask();

            _task.ContinueWith(t =>
                    {
                        self._result = t.Result;
                        if (self._notifier != null)
                            self._notifier();
                    });
        }

        /// <summary>
        /// Synchronously get the value ot this property.
        /// </summary>
        /// <returns>the current value of the underlying property</returns>
        public T Get()
        {
            EnsureTask();
            return _task.Result;
        }

        /// <summary>
        /// Try getting the value. This may return an invalid or out-dated value, but does not block.
        /// </summary>
        /// <remarks>
        /// <para>
        /// After triggering this, the notifier may be called one or more times when updates for this property arrive.
        /// </para>
        /// <para>Use Get() if you need guarantees.</para>
        /// </remarks>
        /// <returns>a (possibly stale) view of the cache</returns>
        public T GetAsync()
        {
            EnsureTask();
            return _result;
        }

        /// <summary>
        /// Set the value of this property. This invalidates the current cache.
        /// </summary>
        /// <param name="value">the new value</param>
        public void Set(T value)
        {
            if (_set == null) throw new ReadOnlyObjectException("No setter available");
            Invalidate();
            _set(value);
        }

        /// <summary>
        /// Reset
        /// </summary>
        public void Invalidate()
        {
            if (_task != null)
                _task.Wait();
            _task = null;
        }
    }
}
