using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetbox.API
{
    public static class TempTaskExtensions
    {
        public static Task OnResult(this Task task, Action<Task> action)
        { 
            return task.ContinueWith(action);
        }

        public static Task OnResult<T>(this Task<T> task, Action<Task<T>> action)
        {
            return task.ContinueWith(action);
        }
    }
}
