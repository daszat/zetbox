
namespace Kistl.Server
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Kistl.API;
    using Kistl.API.Server;
    using Kistl.App.Base;

    /// <summary>
    /// Implements the streaming WCF interface.
    /// </summary>
    public class KistlServiceStreams
        : IKistlServiceStreams
    {
        /// <summary>
        /// Gets a single object from the datastore.
        /// </summary>
        /// <param name="msg">the message should containt only a Type and an ID</param>
        /// <returns>a memory stream containing the serialized object, rewound to the beginning</returns>
        /// <exception cref="ArgumentOutOfRangeException">when the specified object was not found</exception>
        [Obsolete]
        public MemoryStream GetObject(MemoryStream msg)
        {
            try
            {
                msg.Seek(0, SeekOrigin.Begin);
                KistlServiceStreamsMessage m = new KistlServiceStreamsMessage(msg);
                using (TraceClient.TraceHelper.TraceMethodCall(m.Type.ToString()))
                {
                    using (IKistlContext ctx = KistlContext.GetContext())
                    {
                        IDataObject obj = ServerObjectHandlerFactory.GetServerObjectHandler(m.Type.GetSystemType()).GetObject(ctx, m.ID);
                        if (obj == null)
                            throw new ArgumentOutOfRangeException("ID", string.Format("Object with ID {0} not found", m.ID));

                        MemoryStream result = new MemoryStream();
                        BinaryWriter sw = new BinaryWriter(result);

                        return SendObjects(new[] { obj }.AsEnumerable<IStreamable>());
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex, true);
                // Never called, Handle errors throws an Exception
                return null;
            }
        }

        /// <summary>
        /// Puts a number of changed objects into the database. The resultant objects are sent back to the client.
        /// </summary>
        /// <param name="msg">a streamable list of <see cref="IPersistenceObject"/>s</param>
        /// <returns>a streamable list of <see cref="IPersistenceObject"/>s</returns>
        public MemoryStream SetObjects(MemoryStream msg)
        {
            try
            {
                msg.Seek(0, SeekOrigin.Begin);
                using (TraceClient.TraceHelper.TraceMethodCall())
                {

                    BinaryReader sr = new BinaryReader(msg);
                    var objects = new List<IPersistenceObject>();
                    bool @continue;
                    BinarySerializer.FromStream(out @continue, sr);
                    while (@continue)
                    {
                        // Deserialize
                        long pos = msg.Position;
                        SerializableType objType;
                        BinarySerializer.FromStream(out objType, sr);

                        msg.Seek(pos, SeekOrigin.Begin);

                        var obj = (IPersistenceObject)objType.NewObject();
                        obj.FromStream(sr);
                        objects.Add(obj);
                        BinarySerializer.FromStream(out @continue, sr);
                    }

                    using (IKistlContext ctx = KistlContext.GetContext())
                    {
                        // Set Operation
                        var changedObjects = ServerObjectHandlerFactory
                            .GetServerObjectSetHandler()
                            .SetObjects(ctx, objects)
                            .Cast<IStreamable>();
                        return SendObjects(changedObjects);
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex, true);
                // Never called, Handle errors throws an Exception
                return null;
            }
        }

        /// <summary>
        /// Returns a list of objects from the datastore, matching the specified filters.
        /// </summary>
        /// <param name="msg">a KistlServiceStreamsMessage specifying the type and filters to use for the query</param>
        /// <returns>the found objects</returns>
        public MemoryStream GetList(MemoryStream msg)
        {
            try
            {
                msg.Seek(0, SeekOrigin.Begin);
                KistlServiceStreamsMessage m = new KistlServiceStreamsMessage(msg);
                using (TraceClient.TraceHelper.TraceMethodCall(m.Type.ToString()))
                {
                    using (IKistlContext ctx = KistlContext.GetContext())
                    {
                        IEnumerable<IStreamable> lst = ServerObjectHandlerFactory.GetServerObjectHandler(m.Type.GetSystemType())
                            .GetList(ctx, m.MaxListCount,
                                m.Filter != null ? SerializableExpression.ToExpression(m.Filter) : null,
                                m.OrderBy != null ? m.OrderBy.Select(o => SerializableExpression.ToExpression(o)).ToList() : null);

                        return SendObjects(lst);
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex, true);
                // Never called, Handle errors throws an Exception
                return null;
            }
        }

        /// <summary>
        /// Sends a list of auxiliary objects to the specified BinaryWriter while avoiding to send objects twice.
        /// </summary>
        /// <param name="sw">the stream to write to</param>
        /// <param name="auxObjects">a set of objects to send; will not be modified by this call</param>
        /// <param name="sentObjects">a set objects already sent; receives all newly sent objects too</param>
        private static void SendAuxiliaryObjects(BinaryWriter sw, HashSet<IStreamable> auxObjects, HashSet<IStreamable> sentObjects)
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
                    BinarySerializer.ToStream(true, sw);
                    aux.ToStream(sw, secondTierAuxObjects);
                    sentObjects.Add(aux);
                }
                // check whether new objects where eagerly loaded
                secondTierAuxObjects.ExceptWith(sentObjects);
                auxObjects = secondTierAuxObjects;
            }
            // finish list
            BinarySerializer.ToStream(false, sw);
        }

        /// <summary>
        /// Serializes a list of objects onto a <see cref="MemoryStream"/>.
        /// </summary>
        /// <param name="lst">the list of objects to send</param>
        /// <returns>a memory stream containing all objects and all eagerly loaded auxiliary objects</returns>
        private static MemoryStream SendObjects(IEnumerable<IStreamable> lst)
        {
            HashSet<IStreamable> sentObjects = new HashSet<IStreamable>();
            HashSet<IStreamable> auxObjects = new HashSet<IStreamable>();

            MemoryStream result = new MemoryStream();
            BinaryWriter sw = new BinaryWriter(result);
            foreach (IStreamable obj in lst)
            {
                BinarySerializer.ToStream(true, sw);
                // don't check sentObjects here, because a list might contain items twice
                obj.ToStream(sw, auxObjects);
                sentObjects.Add(obj);
            }
            BinarySerializer.ToStream(false, sw);

            SendAuxiliaryObjects(sw, auxObjects, sentObjects);

            result.Seek(0, SeekOrigin.Begin);
            return result;
        }

        /// <summary>
        /// returns a list of objects referenced by a specified Property. Use an equivalent query in GetList() instead.
        /// </summary>
        /// <param name="msg">a KistlServiceStreamsMessage specifying the type, object and property to use for the query</param>
        /// <returns>the referenced objects</returns>
        [Obsolete]
        public MemoryStream GetListOf(MemoryStream msg)
        {
            try
            {
                msg.Seek(0, SeekOrigin.Begin);
                KistlServiceStreamsMessage m = new KistlServiceStreamsMessage(msg);
                using (TraceClient.TraceHelper.TraceMethodCall(m.Type.ToString()))
                {
                    using (IKistlContext ctx = KistlContext.GetContext())
                    {
                        IEnumerable<IStreamable> lst = ServerObjectHandlerFactory
                            .GetServerObjectHandler(m.Type.GetSystemType())
                            .GetListOf(ctx, m.ID, m.Property);
                        return SendObjects(lst);
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex, true);
                // Never called, Handle errors throws an Exception
                return null;
            }
        }

        /// <summary>
        /// Fetches a list of CollectionEntry objects of the Relation 
        /// <paramref name="relID"/> which are owned by the object with the 
        /// ID <paramref name="ID"/> in the role <paramref name="role"/>.
        /// </summary>
        /// <param name="relId">the requested Relation</param>
        /// <param name="serializableRole">the parent role (1 == A, 2 == B)</param>
        /// <param name="parentObjID">the ID of the parent object</param>
        /// <returns>the requested collection entries</returns>
        public MemoryStream FetchRelation(Guid relId, int serializableRole, int parentObjID)
        {
            try
            {
                using (TraceClient.TraceHelper.TraceMethodCall("relId = {0}, role = {1}, parentObjID = {2}", relId, serializableRole, parentObjID))
                {
                    using (IKistlContext ctx = KistlContext.GetContext())
                    {
                        var endRole = (RelationEndRole)serializableRole;
                        Relation rel = ctx.FindPersistenceObject<Relation>(relId);

                        var ifType = typeof(IRelationCollectionEntry<,>);
                        var ceType = ifType.MakeGenericType(rel.A.Type.GetDataType(), rel.B.Type.GetDataType());

                        var lst = ServerObjectHandlerFactory
                            .GetServerCollectionHandler(rel.A.Type.GetDataType(), rel.B.Type.GetDataType(), endRole)
                            .GetCollectionEntries(ctx, relId, endRole, parentObjID);

                        return SendObjects(lst.Cast<IStreamable>());
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex, true);
                // Never called, Handle errors throws an Exception
                return null;
            }
        }

    }
}
