#define USE_STREAMS

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Linq.Expressions;

namespace Kistl.API.Client
{
    public class Proxy
    {
        #region XmlSerializer
        private interface IXmlSerializer
        {
            string XmlFromList(IEnumerable lst);
            string XmlFromObject(Kistl.API.IDataObject obj);
            Kistl.API.IDataObject ObjectFromXml(IKistlContext ctx, string xml);
            IEnumerable ListFromXml(IKistlContext ctx, string xml);
        }

        private class XmlSerializer<XMLCOLLECTION, XMLOBJECT> : IXmlSerializer
            where XMLCOLLECTION : IXmlObjectCollection, new()
            where XMLOBJECT : IXmlObject, new()
        {
            public string XmlFromList(IEnumerable lst)
            {
                XMLCOLLECTION list = new XMLCOLLECTION();
                list.Objects.AddRange(lst.OfType<object>());
                return list.ToXmlString();
            }

            public string XmlFromObject(Kistl.API.IDataObject obj)
            {
                XMLOBJECT result = new XMLOBJECT();
                result.Object = obj;
                return result.ToXmlString();
            }

            public Kistl.API.IDataObject ObjectFromXml(IKistlContext ctx, string xml)
            {
                Kistl.API.IDataObject obj = xml.FromXmlString<XMLOBJECT>().Object as Kistl.API.IDataObject;
                if(ctx != null) ctx.Attach(obj);
                return obj;
            }

            public IEnumerable ListFromXml(IKistlContext ctx, string xml)
            {
                IEnumerable result = xml.FromXmlString<XMLCOLLECTION>().Objects;
                if (ctx != null) result.ForEach<IDataObject>(o => ctx.Attach(o));
                return result;
            }
        }

        private static IXmlSerializer _CurrentSerializer = null;
        private static IXmlSerializer CurrentSerializer
        {
            get
            {
                if (_CurrentSerializer == null)
                {
                    Type t = typeof(XmlSerializer<,>);
                    Type xmlCollection = Type.GetType("Kistl.API.XMLObjectCollection, Kistl.Objects.Client");
                    Type xmlObj = Type.GetType("Kistl.API.XMLObject, Kistl.Objects.Client");

                    Type result = t.MakeGenericType(xmlCollection, xmlObj);

                    _CurrentSerializer = Activator.CreateInstance(result) as IXmlSerializer;
                    if (_CurrentSerializer == null) throw new ApplicationException("Cannot create instance of KistlService.XmlSerializer");
                }

                return _CurrentSerializer;
            }
        }
        #endregion

        private Proxy()
        {
        }

        /// <summary>
        /// WCF Proxy für das KistlService instanzieren.
        /// Konfiguration lt. app.config File
        /// </summary>
        private KistlService.KistlServiceClient service = new KistlService.KistlServiceClient();

        /// <summary>
        /// WCF Proxy für das KistlServiceStreams instanzieren.
        /// Konfiguration lt. app.config File
        /// </summary>
        private KistlServiceStreams.KistlServiceStreamsClient serviceStreams = new KistlServiceStreams.KistlServiceStreamsClient();
        
        /// <summary>
        /// Singelton
        /// </summary>
        private static Proxy current = new Proxy();

        /// <summary>
        /// WCF Proxy für das KistlService
        /// </summary>
        public static Proxy Current
        {
            get
            {
                return current;
            }
        }

        public IEnumerable GetList(IKistlContext ctx, ObjectType type, int maxListCount, Expression filter, Expression orderBy)
        {
            using (TraceClient.TraceHelper.TraceMethodCall(type.ToString()))
            {
#if USE_STREAMS
                KistlServiceStreamsMessage msg = new KistlServiceStreamsMessage();
                msg.Type = type;
                msg.MaxListCount = maxListCount;
                msg.Filter = filter != null ? SerializableExpression.FromExpression(filter, SerializableType.SerializeDirection.ClientToServer) : null;
                msg.OrderBy = orderBy != null ? SerializableExpression.FromExpression(orderBy, SerializableType.SerializeDirection.ClientToServer) : null;

                System.IO.MemoryStream s = serviceStreams.GetList(msg.ToStream());
                System.IO.BinaryReader sr = new System.IO.BinaryReader(s);

                List<Kistl.API.IDataObject> result = new List<Kistl.API.IDataObject>();

                while (s.Position < s.Length)
                {
                    long pos = s.Position;
                    ObjectType objType;
                    BinarySerializer.FromBinary(out objType, sr);

                    s.Seek(pos, System.IO.SeekOrigin.Begin);

                    IDataObject obj = objType.NewDataObject();
                    obj.FromStream(ctx, sr);

                    result.Add((Kistl.API.IDataObject)obj);
                }

                return result;
#else
                string xml = service.GetList(type);
                return CurrentSerializer.ListFromXml(ctx, xml);
#endif
            }
        }

        public IEnumerable GetListOf(IKistlContext ctx, ObjectType type, int ID, string property)
        {
            using (TraceClient.TraceHelper.TraceMethodCall("{0} [{1}].{2}", type, ID, property))
            {
#if USE_STREAMS
                KistlServiceStreamsMessage msg = new KistlServiceStreamsMessage();
                msg.Type = type;
                msg.ID = ID;
                msg.Property = property;
                System.IO.MemoryStream s = serviceStreams.GetListOf(msg.ToStream());
                System.IO.BinaryReader sr = new System.IO.BinaryReader(s);

                List<Kistl.API.IDataObject> result = new List<Kistl.API.IDataObject>();

                while (s.Position < s.Length)
                {
                    long pos = s.Position;
                    ObjectType objType;
                    BinarySerializer.FromBinary(out objType, sr);

                    s.Seek(pos, System.IO.SeekOrigin.Begin);

                    IDataObject obj = objType.NewDataObject();
                    obj.FromStream(ctx, sr);

                    result.Add((Kistl.API.IDataObject)obj);
                }

                return result;
#else
                string xml = service.GetListOf(type, ID, property);
                return CurrentSerializer.ListFromXml(ctx, xml);
#endif
            }
        }

        public Kistl.API.IDataObject GetObject(IKistlContext ctx, ObjectType type, int ID)
        {
            using (TraceClient.TraceHelper.TraceMethodCall("{0} [{1}]", type, ID))
            {
#if USE_STREAMS
                KistlServiceStreamsMessage msg = new KistlServiceStreamsMessage();
                msg.Type = type;
                msg.ID = ID;
                System.IO.MemoryStream s = serviceStreams.GetObject(msg.ToStream());
                System.IO.BinaryReader sr = new System.IO.BinaryReader(s);

                ObjectType objType;
                BinarySerializer.FromBinary(out objType, sr);

                s.Seek(0, System.IO.SeekOrigin.Begin);

                IDataObject obj = objType.NewDataObject();
                obj.FromStream(ctx, sr);

                return (Kistl.API.IDataObject)obj;
#else
                string xml = service.GetObject(type, ID);
                return CurrentSerializer.ObjectFromXml(ctx, xml);
#endif
            }
        }

        public Kistl.API.IDataObject SetObject(IKistlContext ctx, ObjectType type, Kistl.API.IDataObject obj)
        {
            using (TraceClient.TraceHelper.TraceMethodCall("{0}", type))
            {
#if USE_STREAMS
                // Serialize
                KistlServiceStreamsMessage msg = new KistlServiceStreamsMessage();
                msg.Type = type;

                MemoryStream ms = new MemoryStream();
                msg.ToStream(ms);

                BinaryWriter sw = new BinaryWriter(ms);
                obj.ToStream(sw);

                // Set Operation
                System.IO.MemoryStream s = serviceStreams.SetObject(ms);

                // Deserialize
                System.IO.BinaryReader sr = new System.IO.BinaryReader(s);

                ObjectType objType;
                BinarySerializer.FromBinary(out objType, sr);

                s.Seek(0, System.IO.SeekOrigin.Begin);

                obj = objType.NewDataObject();
                obj.FromStream(ctx, sr);

                return obj;

#else
                string xml = CurrentSerializer.XmlFromObject(obj);
                xml = service.SetObject(type, xml);
                return CurrentSerializer.ObjectFromXml(ctx, xml);
#endif
            }
        }

        public void Generate()
        {
            using (TraceClient.TraceHelper.TraceMethodCall())
            {
                service.Generate();
            }
        }

        public string HelloWorld(string name)
        {
            using (TraceClient.TraceHelper.TraceMethodCall(name))
            {
                return service.HelloWorld(name);
            }
        }
    }
}
