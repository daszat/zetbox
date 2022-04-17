using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetbox.API
{
    public static class TaskExtensions
    {
        public static Task OnResult(this Task task, Action<Task> action)
        { 
            task.ConfigureAwait(true);
            return task.ContinueWith(action, TaskContinuationOptions.ExecuteSynchronously);
        }

        public static Task OnResult<T>(this Task<T> task, Action<Task<T>> action)
        {
            task.ConfigureAwait(true);
            return task.ContinueWith(action, TaskContinuationOptions.ExecuteSynchronously);
        }

        public static void TryRunSynchronously(this Task task)
        {
            if(!task.IsCompleted)
            {
                task.RunSynchronously();
            }
        }
    }
}
