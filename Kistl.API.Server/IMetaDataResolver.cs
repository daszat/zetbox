
namespace Kistl.API.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.App.Base;
    using Kistl.API.Utils;

    public interface IMetaDataResolver
    {
        /// <summary>
        /// Retrieves the <see cref="ObjectClass"/> describing the specified <see cref="InterfaceType"/>.
        /// </summary>
        /// <param name="ifType">The interface type to look up.</param>
        /// <returns>The <see cref="ObjectClass"/> describing the specified <see cref="InterfaceType"/>.
        /// This is usually retrieved from a FrozenContext. While initializing the AppDomain, 
        /// this may return null.</returns>
        ObjectClass GetObjectClass(InterfaceType ifType);
    }

    /// <summary>
    /// A simple, caching implementation of the <see cref="IMetaDataResolver"/> interface.
    /// </summary>
    public sealed class CachingMetaDataResolver
        : IMetaDataResolver
    {
        private readonly Func<IReadOnlyKistlContext> _lazyFrozen;
        private IReadOnlyKistlContext _ctx;
        private ILookup<string, ObjectClass> _cache;

        public CachingMetaDataResolver(Func<IReadOnlyKistlContext> lazyFrozen)
        {
            _lazyFrozen = lazyFrozen;
        }

        private void Init(IReadOnlyKistlContext ctx)
        {
            _ctx = ctx;
            _cache = ctx.GetQuery<ObjectClass>().ToLookup(cls => cls.Name);
            Logging.Log.InfoFormat("Initialised CachingMetaDataResolver with {0} classes", _cache.Count);
        }

        /// <inheritdoc/>
        public ObjectClass GetObjectClass(InterfaceType ifType)
        {
            if (_cache == null) { Init(_lazyFrozen.Invoke()); }

            return _cache[ifType.Type.Name].FirstOrDefault(o => o.Module.Namespace == ifType.Type.Namespace && o.Name == ifType.Type.Name);
        }
    }
}
