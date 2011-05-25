using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Utils
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
