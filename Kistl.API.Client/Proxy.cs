#define USE_STREAMS

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Kistl.API.Client
{
    public class Proxy
    {
        #region XmlSerializer
        private interface IXmlSerializer
        {
            string XmlFromList(IEnumerable lst);
            string XmlFromObject(BaseClientDataObject obj);
            BaseClientDataObject ObjectFromXml(string xml);
            IEnumerable ListFromXml(string xml);
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

            public string XmlFromObject(BaseClientDataObject obj)
            {
                XMLOBJECT result = new XMLOBJECT();
                result.Object = obj;
                return result.ToXmlString();
            }

            public BaseClientDataObject ObjectFromXml(string xml)
            {
                return xml.FromXmlString<XMLOBJECT>().Object as BaseClientDataObject;
            }

            public IEnumerable ListFromXml(string xml)
            {
                return xml.FromXmlString<XMLCOLLECTION>().Objects;
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

        public IEnumerable GetList(ObjectType type)
        {
            using (TraceClient.TraceHelper.TraceMethodCall(type.ToString()))
            {
#if USE_STREAMS
                KistlServiceStreamsMessage msg = new KistlServiceStreamsMessage();
                msg.Type = type;
                System.IO.MemoryStream s = serviceStreams.GetList(msg.ToStream());
                System.IO.BinaryReader sr = new System.IO.BinaryReader(s);

                List<BaseClientDataObject> result = new List<BaseClientDataObject>();

                while (s.Position < s.Length)
                {
                    long pos = s.Position;
                    ObjectType objType = new ObjectType();
                    objType = objType.FromBinary(sr);

                    s.Seek(pos, System.IO.SeekOrigin.Begin);

                    BaseClientDataObject obj = ClientHelper.NewBaseClientDataObject(objType);
                    obj.FromStream(sr);

                    result.Add(obj);
                }

                return result;
#else
                string xml = service.GetList(type);
                return CurrentSerializer.ListFromXml(xml);
#endif
            }
        }

        public IEnumerable GetListOf(ObjectType type, int ID, string property)
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

                List<BaseClientDataObject> result = new List<BaseClientDataObject>();

                while (s.Position < s.Length)
                {
                    long pos = s.Position;
                    ObjectType objType = new ObjectType();
                    objType = objType.FromBinary(sr);

                    s.Seek(pos, System.IO.SeekOrigin.Begin);

                    BaseClientDataObject obj = ClientHelper.NewBaseClientDataObject(objType);
                    obj.FromStream(sr);

                    result.Add(obj);
                }

                return result;
#else
                string xml = service.GetListOf(type, ID, property);
                return CurrentSerializer.ListFromXml(xml);
#endif
            }
        }

        public BaseClientDataObject GetObject(ObjectType type, int ID)
        {
            using (TraceClient.TraceHelper.TraceMethodCall("{0} [{1}]", type, ID))
            {
#if USE_STREAMS
                KistlServiceStreamsMessage msg = new KistlServiceStreamsMessage();
                msg.Type = type;
                msg.ID = ID;
                System.IO.MemoryStream s = serviceStreams.GetObject(msg.ToStream());
                System.IO.BinaryReader sr = new System.IO.BinaryReader(s);

                ObjectType objType = new ObjectType();
                objType = objType.FromBinary(sr);

                s.Seek(0, System.IO.SeekOrigin.Begin);

                BaseClientDataObject obj = ClientHelper.NewBaseClientDataObject(objType);
                obj.FromStream(sr);

                return obj;
#else
                string xml = service.GetObject(type, ID);
                return CurrentSerializer.ObjectFromXml(xml);
#endif
            }
        }

        public BaseClientDataObject SetObject(ObjectType type, BaseClientDataObject obj)
        {
            using (TraceClient.TraceHelper.TraceMethodCall("{0}", type))
            {
                string xml = CurrentSerializer.XmlFromObject(obj);
                xml = service.SetObject(type, xml);
                return CurrentSerializer.ObjectFromXml(xml);
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
