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
using Zetbox.API;
using Zetbox.API.Client.PerfCounter;

namespace Zetbox.Client.Presentables
{
    public class ViewModelCache
    {
        private readonly IPerfCounter _perfCounter;
        public ViewModelCache(IPerfCounter perfCounter)
        {
            if (perfCounter == null) throw new ArgumentNullException("perfCounter");
            _perfCounter = perfCounter;
        }

        private Dictionary<object, ViewModel> _cache = new Dictionary<object, ViewModel>();

        public ViewModel LookupOrCreate(object key, Func<ViewModel> create)
        {
            if (key == null) throw new ArgumentNullException("key");
            if (create == null) throw new ArgumentNullException("create");

            _perfCounter.IncrementViewModelFetch();

            var result = _cache.ContainsKey(key) ? _cache[key] : null;
            if (result != null) return result;

            _perfCounter.IncrementViewModelCreate();
            result = create();
            _cache[key] = result;
            return result;
        }
    }

    public static class ViewModelCacheExtensions
    {
        private const String CacheKey = "__ViewModelCache__";

        public static ViewModelCache GetViewModelCache(this IZetboxContext ctx, IPerfCounter perfCounter)
        {
            if (ctx == null) throw new ArgumentNullException("ctx");

            object result;
            if (!ctx.TransientState.TryGetValue(CacheKey, out result))
            {
                result = new ViewModelCache(perfCounter);
                ctx.TransientState[CacheKey] = result;
            }

            return (ViewModelCache)result;
        }
    }
}
