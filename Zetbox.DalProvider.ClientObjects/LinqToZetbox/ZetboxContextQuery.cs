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

namespace Zetbox.DalProvider.Client
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using Zetbox.API;
    using Zetbox.API.Async;
    using Zetbox.API.Client;
    using Zetbox.API.Client.PerfCounter;

    // http://blogs.msdn.com/mattwar/archive/2007/07/30/linq-building-an-iqueryable-provider-part-i.aspx

    internal class ZetboxContextQuery<T> : IOrderedQueryable<T>, IAsyncQueryable<T>, IAsyncQueryable
    {
        private Expression _expression = null;
        private ZetboxContextProvider _provider = null;

        #region Constructor
        public ZetboxContextQuery(ZetboxContextImpl ctx, InterfaceType type, IProxy proxy, IPerfCounter perfCounter)
        {
            if (ctx == null) throw new ArgumentNullException("ctx");

            _expression = System.Linq.Expressions.Expression.Constant(this);
            _provider = new ZetboxContextProvider(ctx, type, proxy, perfCounter);
        }

        public ZetboxContextQuery(ZetboxContextProvider provider, Expression expression)
        {
            if (provider == null) throw new ArgumentNullException("provider");
            if (expression == null) throw new ArgumentNullException("expression");

            _expression = expression;
            _provider = provider;
        }
        #endregion

        #region IEnumerable Members

        public IEnumerator<T> GetEnumerator()
        {
            var task = Task.Run(async () => await _provider.GetListCallAsync<T>(this._expression));
            task.Wait();
            return ((IEnumerable<T>)task.Result).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            var task = Task.Run(async () => await _provider.GetListCallAsync<T>(this._expression));
            task.Wait();
            return ((IEnumerable)task.Result).GetEnumerator();
        }

        #endregion

        #region IQueryable Members

        public Type ElementType
        {
            get { return typeof(T); }
        }

        public Expression Expression
        {
            get { return _expression; }
        }

        public IQueryProvider Provider
        {
            get { return _provider; }
        }
        #endregion

        async Task<IEnumerator> IAsyncQueryable.GetEnumeratorAsync()
        {
            var getTask = await _provider.GetListCallAsync<T>(this._expression);
            if (getTask == null)
                return Enumerable.Empty<T>().GetEnumerator();
            else
                return ((IEnumerable)getTask).GetEnumerator();
        }

        async Task<IEnumerator<T>> IAsyncQueryable<T>.GetEnumeratorAsync()
        {
            var getTask = await _provider.GetListCallAsync<T>(this._expression);
            if (getTask == null)
                return Enumerable.Empty<T>().GetEnumerator();
            else
                return ((IEnumerable<T>)getTask).GetEnumerator();
        }
    }
}
