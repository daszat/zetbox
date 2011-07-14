using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Client.PerfCounter;

namespace Kistl.Client.Presentables
{
    public class ViewModelCache
    {
        private readonly IPerfCounter _perfCounter;
        public ViewModelCache(IPerfCounter perfCounter)
        {
            _perfCounter = perfCounter;
        }

        private Dictionary<object, ViewModel> _cache = new Dictionary<object, ViewModel>();

        public ViewModel LookupOrCreate(object key, Func<ViewModel> create)
        {
            if (key == null) throw new ArgumentNullException("key");
            if (create == null) throw new ArgumentNullException("create");

            if (_perfCounter != null) _perfCounter.IncrementViewModelFetch();

            var result = _cache.ContainsKey(key) ? _cache[key] : null;
            if (result != null) return result;

            if (_perfCounter != null) _perfCounter.IncrementViewModelCreate();
            result = create();
            _cache[key] = result;
            return result;
        }
    }

    public static class ViewModelCacheExtensions
    {
        private const String CacheKey = "__ViewModelCache__";

        public static ViewModelCache GetViewModelCache(this IKistlContext ctx, IPerfCounter perfCounter)
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
