
namespace Kistl.API.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.App.Base;

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
        private IReadOnlyKistlContext _ctx;
        private ILookup<string, ObjectClass> _cache;

        public CachingMetaDataResolver()
        {
        }

        /// <summary>
        /// Used to break the dependency cycle
        /// </summary>
        public IReadOnlyKistlContext Context { set { Init(value); } }

        private void Init(IReadOnlyKistlContext ctx)
        {
            _ctx = ctx;
            _cache = ctx.GetQuery<ObjectClass>().ToLookup(cls => cls.Name);
        }

        /// <inheritdoc/>
        public ObjectClass GetObjectClass(InterfaceType ifType)
        {
            if (_cache == null) { return null; }

            return _cache[ifType.Type.Name].First(o => o.Module.Namespace == ifType.Type.Namespace && o.Name == ifType.Type.Name);
        }
    }
}
