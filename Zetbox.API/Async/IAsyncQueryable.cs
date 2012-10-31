using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Linq.Expressions;

namespace Zetbox.API.Async
{
    public interface IAsyncQueryable
    {
        ZbTask<IEnumerator> GetEnumeratorAsync();
    }

    public interface IAsyncQueryable<T>
    {
        ZbTask<IEnumerator<T>> GetEnumeratorAsync();
    }

    public interface IAsyncQueryProvider
    {
        ZbTask<object> ExecuteAsync(Expression expression);
        ZbTask<T> ExecuteAsync<T>(Expression expression);
    }
}
