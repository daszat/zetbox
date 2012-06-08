
namespace Kistl.API.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API.Utils;

    /// <summary>
    /// Default-Factory for loading ServerObjectHandlers from types containing
    /// helper methods to help with creating the generic handlers.
    /// </summary>
    public abstract class ServerObjectHandlerFactory
        : IServerObjectHandlerFactory
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server");

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
        public abstract IServerCollectionHandler GetServerCollectionHandler(IKistlContext ctx, InterfaceType aType, InterfaceType bType, RelationEndRole endRole);

        /// <inheritdoc/>
        public abstract IServerObjectHandler GetServerObjectHandler(InterfaceType type);

        /// <inheritdoc/>
        public abstract IServerObjectSetHandler GetServerObjectSetHandler();

        /// <inheritdoc/>
        public abstract IServerDocumentHandler GetServerDocumentHandler();

        #endregion
    }
}
