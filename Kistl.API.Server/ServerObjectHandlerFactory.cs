
namespace Kistl.API.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API.Utils;

    /// <summary>
    /// Default-Factory for loading ServerObjectHandlers from types.
    /// </summary>
    public class ServerObjectHandlerFactory
        : IServerObjectHandlerFactory
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server");

        private readonly Type _collectionHandlerType = null;
        private readonly Type _objectHandlerType = null;
        private readonly Type _objectSetHandlerType = null;
        private readonly Type _documentHandlerType = null;

        /// <summary>
        /// Initialises a new instance of the ServerObjectHandlerFactory 
        /// class using the specified types as source for the server object 
        /// handlers.
        /// </summary>
        /// <param name="collectionHandler"></param>
        /// <param name="objectHandler"></param>
        /// <param name="objectSetHandler"></param>
        /// <param name="documentHandlerType"></param>
        public ServerObjectHandlerFactory(Type collectionHandler, Type objectHandler, Type objectSetHandler, Type documentHandlerType)
        {
            _collectionHandlerType = collectionHandler;
            _objectHandlerType = objectHandler;
            _objectSetHandlerType = objectSetHandler;
            _documentHandlerType = documentHandlerType;
        }

        /// <inheritdoc/>
        public IServerObjectHandler GetServerObjectHandler(Type type)
        {
            if (type == null) { throw new ArgumentNullException("type"); }

            try
            {
                Type result = _objectHandlerType.MakeGenericType(type);
                return (IServerObjectHandler)Activator.CreateInstance(result);
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Failed to create IServerObjectHandler for [{0}]", type), ex);
                throw;
            }
        }

        /// <inheritdoc/>
        public IServerObjectSetHandler GetServerObjectSetHandler()
        {
            try
            {
                return (IServerObjectSetHandler)Activator.CreateInstance(_objectSetHandlerType);
            }
            catch (Exception ex)
            {
                Log.Error("Failed to create IServerObjectSetHandler", ex);
                throw;
            }
        }

        /// <inheritdoc/>
        public IServerCollectionHandler GetServerCollectionHandler(Type aType, Type bType, RelationEndRole endRole)
        {
            if (aType == null) { throw new ArgumentNullException("aType"); }
            if (bType == null) { throw new ArgumentNullException("bType"); }
            try
            {
                aType = aType.ToImplementationType();
                bType = bType.ToImplementationType();

                // dynamically translate generic types into provider-known types
                Type[] genericArgs;
                if (endRole == RelationEndRole.A)
                {
                    genericArgs = new Type[] { aType, bType, aType, bType };
                }
                else
                {
                    genericArgs = new Type[] { aType, bType, bType, aType };
                }

                Type resultType = _collectionHandlerType.MakeGenericType(genericArgs);
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

        public IServerDocumentHandler GetServerDocumentHandler()
        {
            try
            {
                return (IServerDocumentHandler)Activator.CreateInstance(_documentHandlerType);
            }
            catch (Exception ex)
            {
                Log.Error("Failed to create IServerDocumentHandler", ex);
                throw;
            }
        }
    }
}
