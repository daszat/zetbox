using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace Kistl.Client.Presentables
{
    public class ViewModelCache
    {
        private Dictionary<object, ViewModel> _cache = new Dictionary<object, ViewModel>();

        public ViewModel LookupOrCreate(object key, Func<ViewModel> create)
        {
            if (key == null) throw new ArgumentNullException("key");
            if (create == null) throw new ArgumentNullException("create");

            var result = _cache.ContainsKey(key) ? _cache[key] : null;
            if (result != null) return result;
            result = create();
            _cache[key] = result;
            return result;
        }
    }

    public static class ViewModelCacheExtensions
    {
        private const String CacheKey = "__ViewModelCache__";

        public static ViewModelCache GetViewModelCache(this IKistlContext ctx)
        {
            if (ctx == null) throw new ArgumentNullException("ctx");

            ViewModelCache result;
            if (!ctx.TransientState.ContainsKey(CacheKey))
            {
                result = new ViewModelCache();
                ctx.TransientState[CacheKey] = result;
            }
            else
            {
                result = (ViewModelCache)ctx.TransientState[CacheKey];
            }

            return result;
        }
    }
}
