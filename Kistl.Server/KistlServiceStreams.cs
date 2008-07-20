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
                    using (IKistlContext ctx = KistlDataContext.InitSession())
                    {
                        IDataObject obj  = ServerObjectHandlerFactory.GetServerObjectHandler(m.Type.GetSerializedType()).GetObject(m.ID);
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
                    using (IKistlContext ctx = KistlDataContext.InitSession())
                    {
                        System.IO.BinaryReader sr = new System.IO.BinaryReader(msg);
                        List<IDataObject> objects = new List<IDataObject>();
                        bool @continue;
                        BinarySerializer.FromBinary(out @continue, sr);
                        while (@continue)
                        {
                            // Deserialize
                            long pos = msg.Position;
                            SerializableType objType;
                            BinarySerializer.FromBinary(out objType, sr);

                            msg.Seek(pos, System.IO.SeekOrigin.Begin);

                            IDataObject obj = (IDataObject)objType.NewObject();
                            obj.FromStream(sr);
                            objects.Add(obj);
                            BinarySerializer.FromBinary(out @continue, sr);
                        }

                        // Set Operation
                        ServerObjectHandlerFactory.GetServerObjectSetHandler().SetObjects(objects);

                        // Serialize back
                        MemoryStream result = new MemoryStream();
                        BinaryWriter sw = new BinaryWriter(result);

                        foreach (IDataObject obj in objects)
                        {
                            BinarySerializer.ToBinary(true, sw);
                            obj.ToStream(sw);
                        }
                        BinarySerializer.ToBinary(false, sw);

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
                    using (IKistlContext ctx = KistlDataContext.InitSession())
                    {
                        IEnumerable lst = ServerObjectHandlerFactory.GetServerObjectHandler(m.Type.GetSerializedType())
                            .GetList(m.MaxListCount,
                                m.Filter != null ? m.Filter.ToExpression() : null,
                                m.OrderBy != null ? m.OrderBy.ToExpression() : null);
                        MemoryStream result = new MemoryStream();
                        BinaryWriter sw = new BinaryWriter(result);
                        foreach (IDataObject obj in lst)
                        {
                            obj.ToStream(sw);
                        }

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
                    using (IKistlContext ctx = KistlDataContext.InitSession())
                    {
                        IEnumerable lst = ServerObjectHandlerFactory.GetServerObjectHandler(m.Type.GetSerializedType()).GetListOf(m.ID, m.Property);
                        MemoryStream result = new MemoryStream();
                        BinaryWriter sw = new BinaryWriter(result);
                        foreach (IDataObject obj in lst)
                        {
                            obj.ToStream(sw);
                        }

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
