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

namespace Zetbox.Server
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using Zetbox.API;
    using Zetbox.API.Server;
    using Zetbox.API.Server.PerfCounter;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;

    /// <summary>
    /// Implements the main service interface.
    /// </summary>
    public class ZetboxService
        : IZetboxService, IDisposable
    {
        private readonly IServerObjectHandlerFactory _sohFactory;
        private readonly Func<IZetboxContext> _ctxFactory;
        private readonly InterfaceType.Factory _iftFactory;
        private readonly IPerfCounter _perfCounter;
        private readonly ZetboxStreamReader.Factory _readerFactory;
        private readonly ZetboxStreamWriter.Factory _writerFactory;

        public ZetboxService(IServerObjectHandlerFactory sohFactory, Func<IZetboxContext> ctxFactory, InterfaceType.Factory iftFactory, IPerfCounter perfCounter, ZetboxStreamReader.Factory readerFactory, ZetboxStreamWriter.Factory writerFactory)
        {
            if (readerFactory == null) throw new ArgumentNullException("readerFactory");
            if (writerFactory == null) throw new ArgumentNullException("writerFactory");

            Logging.Facade.Debug("Creating new ZetboxService instance");

            _sohFactory = sohFactory;
            _ctxFactory = ctxFactory;
            _iftFactory = iftFactory;
            _perfCounter = perfCounter;
            _readerFactory = readerFactory;
            _writerFactory = writerFactory;
        }

        private static void DebugLogIdentity()
        {
            Logging.Facade.DebugFormat("Called IsAuthenticated = {0}, Identity = {1}", System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated, System.Threading.Thread.CurrentPrincipal.Identity.Name);
        }

        /// <summary>
        /// Puts a number of changed objects into the database. The resultant objects are sent back to the client.
        /// </summary>
        /// <param name="version">Current version of generated Zetbox.Objects assembly</param>
        /// <param name="msgArray">a streamable list of <see cref="IPersistenceObject"/>s</param>
        /// <param name="notificationRequests">A list of objects the client wants to be notified about, if they change.</param>
        /// <returns>a streamable list of <see cref="IPersistenceObject"/>s</returns>
        public byte[] SetObjects(Guid version, byte[] msgArray, ObjectNotificationRequest[] notificationRequests)
        {
            using (Logging.Facade.DebugTraceMethodCall("SetObjects"))
            {
                DebugLogIdentity();
                int resultCount = 0;
                var ticks = _perfCounter.IncrementSetObjects();
                try
                {
                    if (msgArray == null) { throw new ArgumentNullException("msgArray"); }
                    MemoryStream msg = new MemoryStream(msgArray);

                    msg.Seek(0, SeekOrigin.Begin);

                    using (IZetboxContext ctx = _ctxFactory())
                    {
                        var objects = ReadObjects(msg, ctx);

                        // Set Operation
                        var changedObjects = _sohFactory
                            .GetServerObjectSetHandler()
                            .SetObjects(version, ctx, objects, notificationRequests ?? new ObjectNotificationRequest[0])
                            .Cast<IStreamable>();
                        resultCount = objects.Count;
                        return SendObjects(changedObjects, true).ToArray();
                    }

                }
                finally
                {
                    _perfCounter.DecrementSetObjects(resultCount, ticks);
                }
            }
        }

        /// <summary>
        /// Returns a list of objects from the datastore, as requested by the query.
        /// </summary>
        /// <param name="version">Current version of generated Zetbox.Objects assembly</param>
        /// <param name="query">A full LINQ query returning zero, one or more objects (FirstOrDefault, Single, Where, Skip, Take, etc.)</param>
        /// <returns>the found objects</returns>
        public byte[] GetObjects(Guid version, SerializableExpression query)
        {
            if (query == null) { throw new ArgumentNullException("query"); }
            using (Logging.Facade.DebugTraceMethodCallFormat("GetObjects", "query={0}", query))
            {
                DebugLogIdentity();
                ZetboxGeneratedVersionAttribute.Check(version);

                var type = query.SerializableType.GetSystemType();
                var ifType = _iftFactory(type.IsGenericType && typeof(IQueryable).IsAssignableFrom(type)
                    ? type.GetGenericArguments()[0]
                    : type);
                int resultCount = 0;
                var ticks = _perfCounter.IncrementGetObjects(ifType);
                try
                {
                    using (IZetboxContext ctx = _ctxFactory())
                    {
                        IEnumerable<IStreamable> lst = _sohFactory
                            .GetServerObjectHandler(ifType)
                            .GetObjects(version, ctx, SerializableExpression.ToExpression(ctx, query, _iftFactory));
                        resultCount = lst.Count();
                        return SendObjects(lst, resultCount <= 1).ToArray();
                    }
                }
                finally
                {
                    _perfCounter.DecrementGetObjects(ifType, resultCount, ticks);
                }
            }
        }

        /// <summary>
        /// Sends a list of auxiliary objects to the specified ZetboxStreamWriter while avoiding to send objects twice.
        /// </summary>
        /// <param name="sw">the stream to write to</param>
        /// <param name="auxObjects">a set of objects to send; will not be modified by this call</param>
        /// <param name="sentObjects">a set objects already sent; receives all newly sent objects too</param>
        /// <param name="eagerLoadLists">True if Lists should be eager loaded</param>
        private static void SendAuxiliaryObjects(ZetboxStreamWriter sw, HashSet<IStreamable> auxObjects, HashSet<IStreamable> sentObjects, bool eagerLoadLists)
        {
            // clone auxObjects to avoid modification
            auxObjects = new HashSet<IStreamable>(auxObjects);
            auxObjects.ExceptWith(sentObjects);
            // send all eagerly loaded objects
            while (auxObjects.Count > 0)
            {
                HashSet<IStreamable> secondTierAuxObjects = new HashSet<IStreamable>();
                foreach (var aux in auxObjects.Where(o => o != null))
                {
                    sw.Write(true);
                    aux.ToStream(sw, secondTierAuxObjects, eagerLoadLists);
                    sentObjects.Add(aux);
                }
                // check whether new objects where eagerly loaded
                secondTierAuxObjects.ExceptWith(sentObjects);
                auxObjects = secondTierAuxObjects;
            }
            // finish list
            sw.Write(false);
        }

        /// <summary>
        /// Serializes a list of objects onto a <see cref="MemoryStream"/>.
        /// </summary>
        /// <param name="lst">the list of objects to send</param>
        /// <param name="eagerLoadLists">True if Lists should be eager loaded</param>
        /// <returns>a memory stream containing all objects and all eagerly loaded auxiliary objects</returns>
        private MemoryStream SendObjects(IEnumerable<IStreamable> lst, bool eagerLoadLists)
        {
            HashSet<IStreamable> sentObjects = new HashSet<IStreamable>();
            HashSet<IStreamable> auxObjects = new HashSet<IStreamable>();

            var result = new MemoryStream();
            // Don't Dispose sw, to keep result open.
            var sw = _writerFactory.Invoke(new BinaryWriter(result));
            foreach (IStreamable obj in lst)
            {
                sw.Write(true);
                // don't check sentObjects here, because a list might contain items twice
                obj.ToStream(sw, auxObjects, eagerLoadLists);
                sentObjects.Add(obj);
            }
            sw.Write(false);

            SendAuxiliaryObjects(sw, auxObjects, sentObjects, eagerLoadLists);

            // https://connect.microsoft.com/VisualStudio/feedback/details/541494/wcf-streaming-issue
            sw.Write(false);
            sw.Write(false);
            sw.Write(false);
            sw.Flush();

            Logging.Facade.DebugFormat("Sending {0} Objects with {1} with AuxObjects and EagerLoadLists = {2}", sentObjects.Count, auxObjects.Count, eagerLoadLists);

            result.Seek(0, SeekOrigin.Begin);
            return result;
        }

        private List<IPersistenceObject> ReadObjects(Stream msg, IZetboxContext ctx)
        {
            var objects = new List<IPersistenceObject>();
            var sr = _readerFactory.Invoke(new BinaryReader(msg));
            while (sr.ReadBoolean())
            {
                // Deserialize
                var objType = sr.ReadSerializableType();

                var obj = ctx.Internals().CreateUnattached(_iftFactory(objType.GetSystemType()));
                obj.FromStream(sr);
                objects.Add(obj);
            }
            return objects;
        }

        /// <summary>
        /// returns a list of objects referenced by a specified Property. Use an equivalent query in GetObjects() instead.
        /// </summary>
        /// <param name="version">Current version of generated Zetbox.Objects assembly</param>
        /// <param name="type">Type of Object</param>
        /// <param name="ID">Object id</param>
        /// <param name="property">Property</param>
        /// <returns>the referenced objects</returns>
        [Obsolete]
        public byte[] GetListOf(Guid version, SerializableType type, int ID, string property)
        {
            using (Logging.Facade.DebugTraceMethodCallFormat("GetListOf", "type={0}", type))
            {
                DebugLogIdentity();
                if (type == null) { throw new ArgumentNullException("type"); }

                using (var ctx = _ctxFactory())
                {
                    var ifType = _iftFactory(type.GetSystemType());
                    int resultCount = 0;
                    var ticks = _perfCounter.IncrementGetListOf(ifType);
                    try
                    {
                        IEnumerable<IStreamable> lst = _sohFactory
                            .GetServerObjectHandler(ifType)
                            .GetListOf(version, ctx, ID, property);
                        resultCount = lst.Count();
                        return SendObjects(lst, false /*true*/).ToArray();
                    }
                    finally
                    {
                        _perfCounter.DecrementGetListOf(ifType, resultCount, ticks);
                    }
                }
            }
        }

        /// <summary>
        /// Fetches a list of CollectionEntry objects of the Relation 
        /// <paramref name="relId"/> which are owned by the object with the 
        /// ID <paramref name="parentObjID"/> in the role <paramref name="serializableRole"/>.
        /// </summary>
        /// <param name="version">Current version of generated Zetbox.Objects assembly</param>
        /// <param name="relId">the requested Relation</param>
        /// <param name="serializableRole">the parent role (1 == A, 2 == B)</param>
        /// <param name="parentObjID">the ID of the parent object</param>
        /// <returns>the requested collection entries</returns>
        public byte[] FetchRelation(Guid version, Guid relId, int serializableRole, int parentObjID)
        {
            using (Logging.Facade.DebugTraceMethodCallFormat("FetchRelation", "relId = [{0}], role = [{1}], parentObjID = [{2}]", relId, serializableRole, parentObjID))
            {
                DebugLogIdentity();

                using (IZetboxContext ctx = _ctxFactory())
                {
                    var endRole = (RelationEndRole)serializableRole;
                    // TODO: Use FrozenContext
                    Relation rel = ctx.FindPersistenceObject<Relation>(relId);
                    var ifTypeA = _iftFactory(rel.A.Type.GetDataType());
                    var ifTypeB = _iftFactory(rel.B.Type.GetDataType());
                    var ifType = endRole == RelationEndRole.A ? ifTypeA : ifTypeB;
                    int resultCount = 0;
                    var ticks = _perfCounter.IncrementFetchRelation(ifType);
                    try
                    {
                        var lst = _sohFactory
                            .GetServerCollectionHandler(
                                ctx,
                                ifTypeA,
                                ifTypeB,
                                endRole)
                            .GetCollectionEntries(version, ctx, relId, endRole, parentObjID);
                        resultCount = lst.Count();
                        return SendObjects(lst.Cast<IStreamable>(), true).ToArray();
                    }
                    finally
                    {
                        _perfCounter.DecrementFetchRelation(ifType, resultCount, ticks);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the content stream of the given Document instance ID
        /// </summary>
        /// <param name="version">Current version of generated Zetbox.Objects assembly</param>
        /// <param name="ID">ID of an valid Document instance</param>
        /// <returns>Stream containing the Document content</returns>
        public Stream GetBlobStream(Guid version, int ID)
        {
            using (Logging.Facade.DebugTraceMethodCallFormat("GetBlobStream", "ID={0}", ID))
            {
                DebugLogIdentity();

                using (IZetboxContext ctx = _ctxFactory())
                {
                    var result = _sohFactory
                        .GetServerDocumentHandler()
                        .GetBlobStream(version, ctx, ID);

                    if (result == null)
                    {
                        Logging.Facade.Debug("GetBlobStream returns null");
                    }
                    else
                    {
                        Logging.Facade.DebugFormat("GetBlobStream returns {0} of {1} bytes.", result.GetType().FullName, result.Length);
                    }
                    return result;
                }
            }
        }

        /// <summary>
        /// Sets the content stream of the given Document instance ID
        /// </summary>
        /// <param name="blob">Information about the given blob</param>
        /// <returns>the newly created Blob instance</returns>
        public BlobResponse SetBlobStream(BlobMessage blob)
        {
            using (Logging.Facade.DebugTraceMethodCall("SetBlobStream"))
            {
                if (blob == null)
                    throw new ArgumentNullException("blob");
                DebugLogIdentity();
                if (Logging.Facade.IsDebugEnabled)
                {
                    long length = -1;
                    try { length = blob.Stream.Length; }
                    catch (NotSupportedException)
                    {
                        // ignore missing support for accessing the stream's length
                        length = -2;
                    }
                    catch (Exception ex)
                    {
                        Logging.Facade.Debug(string.Format("SetBlobStream failed to access {0}.Length", blob.Stream.GetType()), ex);
                        length = -3;
                    }

                    Logging.Facade.DebugFormat("SetBlobStream started with BlobMessage ( FileName='{0}', MimeType='{1}', Stream='{2}' of length={3} )",
                        blob.FileName,
                        blob.MimeType,
                        blob.Stream.GetType().FullName,
                        length);
                }

                using (IZetboxContext ctx = _ctxFactory())
                {
                    var result = _sohFactory
                        .GetServerDocumentHandler()
                        .SetBlobStream(blob.Version, ctx, blob.Stream, blob.FileName, blob.MimeType);

                    if (Logging.Facade.IsDebugEnabled)
                    {
                        using (var stream = result.GetStream())
                            Logging.Facade.DebugFormat("SetBlobStream created Blob with ID=#{0}, length={1}.", result.ID, stream.Length);
                    }
                    return new BlobResponse()
                    {
                        ID = result.ID,
                        BlobInstance = SendObjects(new IDataObject[] { result }, true)
                    };
                }
            }
        }

        public byte[] InvokeServerMethod(Guid version, SerializableType type, int ID, string method, SerializableType[] parameterTypes, byte[] parameterArray, byte[] changedObjectsArray, ObjectNotificationRequest[] notificationRequests, out byte[] retChangedObjects)
        {
            using (Logging.Facade.DebugTraceMethodCallFormat("InvokeServerMethod:" + method, "method={0}, ID={1}", method, ID))
            {
                if (type == null)
                    throw new ArgumentNullException("type");
                if (string.IsNullOrEmpty(method))
                    throw new ArgumentNullException("method");
                if (parameterTypes == null)
                    throw new ArgumentNullException("parameterTypes");
                if (parameterArray == null)
                    throw new ArgumentNullException("parameterArray");
                if (changedObjectsArray == null)
                    throw new ArgumentNullException("changedObjectsArray");

                _perfCounter.IncrementServerMethodInvocation();
                retChangedObjects = null;

                DebugLogIdentity();

                using (IZetboxContext ctx = _ctxFactory())
                {
                    var parameter = new MemoryStream(parameterArray);
                    parameter.Seek(0, SeekOrigin.Begin);
                    List<object> parameterList = new List<object>();
                    using (var parameterReader = _readerFactory.Invoke(new BinaryReader(parameter)))
                    {
                        foreach (var t in parameterTypes)
                        {
                            object val;
                            var systemType = t.GetSystemType();
                            parameterReader.Read(out val,
                                systemType.IsICompoundObject() // IDataObjects are serialized as proxy, only ICompoundObject are serialized directoy
                                    ? ctx.ToImplementationType(ctx.GetInterfaceType(systemType)).Type
                                    : systemType);
                            parameterList.Add(val);
                        }
                    }

                    var changedObjects = new MemoryStream(changedObjectsArray);
                    changedObjects.Seek(0, SeekOrigin.Begin);

                    var readObjects = ReadObjects(changedObjects, ctx);

                    // Context is ready, translate IDataObjectParameter
                    for (int i = 0; i < parameterList.Count; i++)
                    {
                        var p = parameterList[i] as ZetboxStreamReader.IDataObjectProxy;
                        if (p != null)
                        {
                            parameterList[i] = ctx.Find(_iftFactory(p.Type.GetSystemType()), p.ID);
                        }
                    }

                    IEnumerable<IPersistenceObject> changedObjectsList;
                    var result = _sohFactory
                        .GetServerObjectHandler(_iftFactory(type.GetSystemType()))
                        .InvokeServerMethod(version, ctx, ID, method,
                            parameterTypes.Select(t => t.GetSystemType()),
                            parameterList,
                            readObjects,
                            notificationRequests ?? new ObjectNotificationRequest[0],
                            out changedObjectsList);

                    retChangedObjects = SendObjects(changedObjectsList.Cast<IStreamable>(), true).ToArray();

                    if (Logging.Facade.IsDebugEnabled && result != null)
                    {
                        Logging.Facade.DebugFormat("Serializing method result type is '{0}'", result.GetType().FullName);
                    }

                    if (result != null && result.GetType() == typeof(string))
                    {
                        Logging.Facade.Debug("Serializing method result as string");
                        // string is also a IEnumerable, but FindElementTypes returns nothing
                        MemoryStream resultStream = new MemoryStream();
                        new BinaryFormatter().Serialize(resultStream, result);
                        return resultStream.ToArray();
                    }
                    else if (result != null && result.GetType().IsIStreamable())
                    {
                        Logging.Facade.Debug("Serializing method result as IStreamable");
                        IStreamable resultObj = (IStreamable)result;
                        return SendObjects(new IStreamable[] { resultObj }, true).ToArray();
                    }
                    else if (result != null && result.GetType().IsIEnumerable() && result.GetType().FindElementTypes().Any(t => t.IsIStreamable()))
                    {
                        Logging.Facade.Debug("Serializing method result as IEnumerable<IStreamable>");
                        var lst = ((IEnumerable)result).AsQueryable().Cast<IStreamable>().Take(Zetbox.API.Helper.MAXLISTCOUNT);
                        return SendObjects(lst, true).ToArray();
                    }
                    else if (result != null)
                    {
                        Logging.Facade.Debug("Serializing method result as object with BinaryFormatter");
                        MemoryStream resultStream = new MemoryStream();
                        new BinaryFormatter().Serialize(resultStream, result);
                        return resultStream.ToArray();
                    }
                    else
                    {
                        Logging.Facade.Debug("Serializing empty method");
                        return new byte[] { };
                    }
                }
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Logging.Facade.Debug("Disposing ZetboxService instance");
        }

        #endregion
    }
}