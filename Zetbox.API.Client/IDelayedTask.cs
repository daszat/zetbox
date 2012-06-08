using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Client
{
    public interface IDelayedTask
    {
        /// <summary>
        /// Trigger one execution of this Task. The execution may be delayed by the implementation by queueing it onto the UI's message pump or similar.
        /// </summary>
        void Trigger();
    }

    public class ImmediateTask : IDelayedTask
    {
        private readonly Action _task;

        public ImmediateTask(Action task)
        {
            if (task == null) throw new ArgumentNullException("task");

            _task = task;
        }

        public void Trigger()
        {
            _task();
        }
    }
}
