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

namespace Zetbox.API.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API.Utils;

    /// <summary>
    /// Default-Factory for loading ServerObjectHandlers from types containing
    /// helper methods to help with creating the generic handlers.
    /// </summary>
    public abstract class ServerObjectHandlerFactory
        : IServerObjectHandlerFactory
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Server");

        protected ServerObjectHandlerFactory()
        {
        }

        protected IServerObjectHandler GetServerObjectHandlerHelper(
            Type objectHandlerType,
            InterfaceType intfType)
        {
            try
            {
                Type result = objectHandlerType.MakeGenericType(intfType.Type);
                return (IServerObjectHandler)Activator.CreateInstance(result);
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Failed to create IServerObjectHandler for [{0}]", intfType), ex);
                throw;
            }
        }

        protected IServerCollectionHandler GetServerCollectionHandlerHelper(
            Type collectionHandlerType,
            ImplementationType aType,
            ImplementationType bType,
            RelationEndRole endRole)
        {
            if (Object.ReferenceEquals(aType, null)) { throw new ArgumentNullException("aType"); }
            if (Object.ReferenceEquals(bType, null)) { throw new ArgumentNullException("bType"); }
            try
            {
                // dynamically translate generic types into provider-known types
                Type[] genericArgs;
                if (endRole == RelationEndRole.A)
                {
                    genericArgs = new Type[] { aType.Type, bType.Type, aType.Type, bType.Type };
                }
                else
                {
                    genericArgs = new Type[] { aType.Type, bType.Type, bType.Type, aType.Type };
                }

                Type resultType = collectionHandlerType.MakeGenericType(genericArgs);
                return (IServerCollectionHandler)Activator.CreateInstance(resultType);
            }
            catch (Exception ex)
            {
                var msg = String.Format(
                    "Failed to create IServerCollectionHandler for A=[{0}], B=[{1}], role=[{2}]",
                    aType,
                    bType,
                    endRole);
                Log.Error(msg, ex);
                throw;
            }
        }

        #region IServerObjectHandlerFactory Members

        /// <inheritdoc/>
        public abstract IServerCollectionHandler GetServerCollectionHandler(IZetboxContext ctx, InterfaceType aType, InterfaceType bType, RelationEndRole endRole);

        /// <inheritdoc/>
        public abstract IServerObjectHandler GetServerObjectHandler(InterfaceType type);

        /// <inheritdoc/>
        public abstract IServerObjectSetHandler GetServerObjectSetHandler();

        /// <inheritdoc/>
        public abstract IServerDocumentHandler GetServerDocumentHandler();

        #endregion
    }
}
