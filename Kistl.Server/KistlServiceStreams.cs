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
                    using (KistlDataContext ctx = KistlDataContext.InitSession())
                    {
                        BaseServerDataObject obj  = ServerObjectHandlerFactory.GetServerObjectHandler(m.Type).GetObject(m.ID);
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

        public System.IO.MemoryStream SetObject(System.IO.MemoryStream msg)
        {
            try
            {
                msg.Seek(0, SeekOrigin.Begin);
                KistlServiceStreamsMessage m = new KistlServiceStreamsMessage(msg);
                using (TraceClient.TraceHelper.TraceMethodCall(m.Type.ToString()))
                {
                    using (KistlDataContext ctx = KistlDataContext.InitSession())
                    {
                        // Deserialize
                        long pos = msg.Position;
                        System.IO.BinaryReader sr = new System.IO.BinaryReader(msg);
                        ObjectType objType;
                        BinarySerializer.FromBinary(out objType, sr);

                        msg.Seek(pos, System.IO.SeekOrigin.Begin);

                        IDataObject obj = objType.NewDataObject();
                        obj.FromStream(ctx, sr);

                        // Set Operation
                        obj = ServerObjectHandlerFactory.GetServerObjectHandler(objType).SetObject((BaseServerDataObject)obj);

                        // Serialize back
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

        public System.IO.MemoryStream GetList(System.IO.MemoryStream msg)
        {
            try
            {
                msg.Seek(0, SeekOrigin.Begin);
                KistlServiceStreamsMessage m = new KistlServiceStreamsMessage(msg);
                using (TraceClient.TraceHelper.TraceMethodCall(m.Type.ToString()))
                {
                    using (KistlDataContext ctx = KistlDataContext.InitSession())
                    {
                        IEnumerable lst = ServerObjectHandlerFactory.GetServerObjectHandler(m.Type).GetList();
                        MemoryStream result = new MemoryStream();
                        BinaryWriter sw = new BinaryWriter(result);
                        foreach (BaseServerDataObject obj in lst)
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
                    using (KistlDataContext ctx = KistlDataContext.InitSession())
                    {
                        IEnumerable lst = ServerObjectHandlerFactory.GetServerObjectHandler(m.Type).GetListOf(m.ID, m.Property);
                        MemoryStream result = new MemoryStream();
                        BinaryWriter sw = new BinaryWriter(result);
                        foreach (BaseServerDataObject obj in lst)
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
