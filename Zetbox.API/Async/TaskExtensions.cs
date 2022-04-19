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
            if (task.Status == TaskStatus.Created)
                task.Start();
            return task.ContinueWith(action, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.ExecuteSynchronously);
        }

        public static Task OnResult<T>(this Task<T> task, Action<Task<T>> action)
        {
            if (task.Status == TaskStatus.Created)
                task.Start();
            return task.ContinueWith(action, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.ExecuteSynchronously);
        }

        public static void TryRunSynchronously(this Task task)
        {
            if (task.Status == TaskStatus.Created)
            {
                try
                {
                    task.RunSynchronously();
                }
                catch (InvalidOperationException)
                {
                    task.Wait();
                }
            }
            
            task.Wait();
        }
    }
}
