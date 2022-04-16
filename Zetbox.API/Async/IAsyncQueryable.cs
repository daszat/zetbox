using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Zetbox.API.Async
{
    public interface IAsyncQueryable
    {
        Task<IEnumerator> GetEnumeratorAsync();
    }

    public interface IAsyncQueryable<T>
    {
        Task<IEnumerator<T>> GetEnumeratorAsync();
    }

    public interface IAsyncQueryProvider
    {
        Task<object> ExecuteAsync(Expression expression);
        Task<T> ExecuteAsync<T>(Expression expression);
    }
}
