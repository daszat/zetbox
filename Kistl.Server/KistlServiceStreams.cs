using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;

namespace Kistl.Server
{
    public class KistlServiceStreams : IKistlServiceStreams
    {
        public System.IO.MemoryStream GetObject(System.IO.MemoryStream msg)
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
                        if (obj == null) throw new ArgumentOutOfRangeException("ID", string.Format("Object with ID {0} not found", m.ID));
                        MemoryStream result = new MemoryStream();
                        BinaryWriter sw = new BinaryWriter(result);

                        obj.ToStream(sw);

                        result.Seek(0, SeekOrigin.Begin);
                        return result;
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

        public System.IO.MemoryStream SetObjects(System.IO.MemoryStream msg)
        {
            try
            {
                msg.Seek(0, SeekOrigin.Begin);
                using (TraceClient.TraceHelper.TraceMethodCall())
                {

                    System.IO.BinaryReader sr = new System.IO.BinaryReader(msg);
                    var objects = new List<IPersistenceObject>();
                    bool @continue;
                    BinarySerializer.FromStream(out @continue, sr);
                    while (@continue)
                    {
                        // Deserialize
                        long pos = msg.Position;
                        SerializableType objType;
                        BinarySerializer.FromStream(out objType, sr);

                        msg.Seek(pos, System.IO.SeekOrigin.Begin);

                        var obj = (IPersistenceObject)objType.NewObject();
                        obj.FromStream(sr);
                        objects.Add(obj);
                        BinarySerializer.FromStream(out @continue, sr);
                    }

                    using (IKistlContext ctx = KistlContext.GetContext())
                    {
                        // Set Operation
                        var changedObjects = ServerObjectHandlerFactory.GetServerObjectSetHandler().SetObjects(ctx, objects);

                        // Serialize back
                        MemoryStream result = new MemoryStream();
                        BinaryWriter sw = new BinaryWriter(result);

                        foreach (var obj in changedObjects)
                        {
                            BinarySerializer.ToStream(true, sw);
                            obj.ToStream(sw);
                        }
                        BinarySerializer.ToStream(false, sw);

                        result.Seek(0, SeekOrigin.Begin);
                        return result;
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

        public System.IO.MemoryStream GetList(System.IO.MemoryStream msg)
        {
            try
            {
                msg.Seek(0, SeekOrigin.Begin);
                KistlServiceStreamsMessage m = new KistlServiceStreamsMessage(msg);
                using (TraceClient.TraceHelper.TraceMethodCall(m.Type.ToString()))
                {
                    using (IKistlContext ctx = KistlContext.GetContext())
                    {
                        IEnumerable lst = ServerObjectHandlerFactory.GetServerObjectHandler(m.Type.GetSystemType())
                            .GetList(ctx, m.MaxListCount,
                                m.Filter != null ? SerializableExpression.ToExpression(m.Filter) : null,
                                m.OrderBy != null ? m.OrderBy.Select(o => SerializableExpression.ToExpression(o)).ToList() : null);
                        MemoryStream result = new MemoryStream();
                        BinaryWriter sw = new BinaryWriter(result);
                        foreach (IDataObject obj in lst)
                        {
                            BinarySerializer.ToStream(true, sw);
                            obj.ToStream(sw);
                        }
                        BinarySerializer.ToStream(false, sw);

                        result.Seek(0, SeekOrigin.Begin);
                        return result;
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

        public System.IO.MemoryStream GetListOf(System.IO.MemoryStream msg)
        {
            try
            {
                msg.Seek(0, SeekOrigin.Begin);
                KistlServiceStreamsMessage m = new KistlServiceStreamsMessage(msg);
                using (TraceClient.TraceHelper.TraceMethodCall(m.Type.ToString()))
                {
                    using (IKistlContext ctx = KistlContext.GetContext())
                    {
                        IEnumerable lst = ServerObjectHandlerFactory.GetServerObjectHandler(m.Type.GetSystemType()).GetListOf(ctx, m.ID, m.Property);
                        MemoryStream result = new MemoryStream();
                        BinaryWriter sw = new BinaryWriter(result);
                        foreach (IDataObject obj in lst)
                        {
                            BinarySerializer.ToStream(true, sw);
                            obj.ToStream(sw);
                        }
                        BinarySerializer.ToStream(false, sw);

                        result.Seek(0, SeekOrigin.Begin);
                        return result;
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


        // TODO: korrekte Signatur ist FetchRelation(int relationId, int serializableRole, int parentObjID)
        public System.IO.MemoryStream FetchRelation(int relId, int serializableRole, int ID)
        {
            try
            {
                using (TraceClient.TraceHelper.TraceMethodCall("relId = {0}, role = {1}, ID = {2}", relId, serializableRole, ID))
                {
                    using (IKistlContext ctx = KistlContext.GetContext())
                    {
                        var endRole = (RelationEndRole)serializableRole;
                        Relation rel = ctx.Find<Relation>(relId);

                        var ifType = typeof(INewCollectionEntry<,>);
                        var ceType = ifType.MakeGenericType(rel.A.Type.GetDataType(), rel.B.Type.GetDataType());

                        var lst = ServerObjectHandlerFactory
                            .GetServerCollectionHandler(rel.A.Type.GetDataType(), rel.B.Type.GetDataType(), endRole)
                            .GetCollectionEntries(ctx, relId, endRole, ID);

                        MemoryStream result = new MemoryStream();
                        BinaryWriter sw = new BinaryWriter(result);
                        foreach (var obj in lst)
                        {
                            BinarySerializer.ToStream(true, sw);
                            obj.ToStream(sw);
                        }
                        BinarySerializer.ToStream(false, sw);

                        result.Seek(0, SeekOrigin.Begin);
                        return result;
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
