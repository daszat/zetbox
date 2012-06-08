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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zetbox.API.Utils
{
    public sealed class FuncCache<T, TResult>
    {
        private readonly Func<T, TResult> _func;
        private readonly Dictionary<T, TResult> _cache = new Dictionary<T, TResult>();
        private readonly Func<T, TResult> _invoke;

        public FuncCache(Func<T, TResult> func)
        {
            _func = func;
            _invoke = t => this.Invoke(t);
        }

        public TResult Invoke(T arg)
        {
            TResult result;
            if (!_cache.TryGetValue(arg, out result))
            {
                result = _func(arg);
                _cache[arg] = result;
            }
            return result;
        }
        
        public Func<T, TResult> Func { get { return _invoke; } }
        
        public void Clear()
        {
            _cache.Clear();
        }
    }
}
