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

namespace Zetbox.API.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;

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

        /// <summary>
        /// Retrieves the <see cref="CompoundObject"/> describing the specified <see cref="InterfaceType"/>.
        /// </summary>
        /// <param name="ifType">The interface type to look up.</param>
        /// <returns>The <see cref="ObjectClass"/> describing the specified <see cref="InterfaceType"/>.
        /// This is usually retrieved from a FrozenContext. While initializing the AppDomain,
        /// this may return null.</returns>
        CompoundObject GetCompoundObject(InterfaceType ifType);

        /// <summary>
        /// Retrieves the <see cref="DataType"/> describing the specified <see cref="InterfaceType"/>.
        /// </summary>
        /// <param name="ifType">The interface type to look up.</param>
        /// <returns>The <see cref="ObjectClass"/> describing the specified <see cref="InterfaceType"/>.
        /// This is usually retrieved from a FrozenContext. While initializing the AppDomain,
        /// this may return null.</returns>
        DataType GetDataType(InterfaceType ifType);
    }

    /// <summary>
    /// A simple, caching implementation of the <see cref="IMetaDataResolver"/> interface.
    /// </summary>
    public sealed class CachingMetaDataResolver
        : IMetaDataResolver
    {
        private readonly object _lock = new object();

        private readonly Lazy<IFrozenContext> _lazyFrozen;
        private ILookup<string, DataType> _cache;

        public CachingMetaDataResolver(Lazy<IFrozenContext> lazyFrozen)
        {
            _lazyFrozen = lazyFrozen;
        }

        private void Init()
        {
            lock (_lock)
            {
                if (_cache != null)
                    return;

                _cache = _lazyFrozen.Value.GetQuery<DataType>().ToLookup(cls => cls.Name);
                Logging.Log.InfoFormat("Initialised CachingMetaDataResolver with {0} classes", _cache.Count);
            }
        }

        private DataType Lookup(InterfaceType ifType)
        {
            if (_cache == null) { Init(); }

            return _cache[ifType.Type.Name].FirstOrDefault(o => o.Module.Namespace == ifType.Type.Namespace && o.Name == ifType.Type.Name);
        }

        /// <inheritdoc/>
        public DataType GetDataType(InterfaceType ifType)
        {
            return Lookup(ifType);
        }

        /// <inheritdoc/>
        public ObjectClass GetObjectClass(InterfaceType ifType)
        {
            return Lookup(ifType) as ObjectClass;
        }

        /// <inheritdoc/>
        public CompoundObject GetCompoundObject(InterfaceType ifType)
        {
            return Lookup(ifType) as CompoundObject;
        }
    }
}
