using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Xml;
using Kistl.API.Server;
using Kistl.API;
using System.Diagnostics;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;
using System.IO;

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
                    using (IKistlContext ctx = KistlContext.InitSession())
                    {
                        IDataObject obj = ServerObjectHandlerFactory.GetServerObjectHandler(m.Type.GetSystemType()).GetObject(m.ID);
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
                    List<IDataObject> objects = new List<IDataObject>();
                    bool @continue;
                    BinarySerializer.FromStream(out @continue, sr);
                    while (@continue)
                    {
                        // Deserialize
                        long pos = msg.Position;
                        SerializableType objType;
                        BinarySerializer.FromStream(out objType, sr);

                        msg.Seek(pos, System.IO.SeekOrigin.Begin);

                        IDataObject obj = (IDataObject)objType.NewObject();
                        obj.FromStream(sr);
                        objects.Add(obj);
                        BinarySerializer.FromStream(out @continue, sr);
                    }

                    using (IKistlContext ctx = KistlContext.InitSession())
                    {
                        // Set Operation
                        var changedObjects = ServerObjectHandlerFactory.GetServerObjectSetHandler().SetObjects(objects);

                        // Serialize back
                        MemoryStream result = new MemoryStream();
                        BinaryWriter sw = new BinaryWriter(result);

                        foreach (IDataObject obj in changedObjects)
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
                    using (IKistlContext ctx = KistlContext.InitSession())
                    {
                        IEnumerable lst = ServerObjectHandlerFactory.GetServerObjectHandler(m.Type.GetSystemType())
                            .GetList(m.MaxListCount,
                                m.Filter != null ? SerializableExpression.ToExpression(m.Filter) : null,
                                m.OrderBy != null ? SerializableExpression.ToExpression(m.OrderBy) : null);
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
                    using (IKistlContext ctx = KistlContext.InitSession())
                    {
                        IEnumerable lst = ServerObjectHandlerFactory.GetServerObjectHandler(m.Type.GetSystemType()).GetListOf(m.ID, m.Property);
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
    }
}
