
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

        /// <inheritdoc/>
        protected IServerObjectHandler GetServerObjectHandlerHelper(
            Type objectHandlerType,
            ImplementationType implType)
        {
            if (implType == null) { throw new ArgumentNullException("implType"); }

            try
            {
                Type result = objectHandlerType.MakeGenericType(implType.Type);
                return (IServerObjectHandler)Activator.CreateInstance(result);
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Failed to create IServerObjectHandler for [{0}]", implType), ex);
                throw;
            }
        }

        /// <inheritdoc/>
        protected IServerCollectionHandler GetServerCollectionHandlerHelper(
            Type collectionHandlerType,
            ImplementationType aType,
            ImplementationType bType,
            RelationEndRole endRole)
        {
            if (aType == null) { throw new ArgumentNullException("aType"); }
            if (bType == null) { throw new ArgumentNullException("bType"); }
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

        public abstract IServerCollectionHandler GetServerCollectionHandler(InterfaceType aType, InterfaceType bType, RelationEndRole endRole);
        public abstract IServerObjectHandler GetServerObjectHandler(InterfaceType type);
        public abstract IServerObjectSetHandler GetServerObjectSetHandler();
        public abstract IServerDocumentHandler GetServerDocumentHandler();

        #endregion
    }
}
