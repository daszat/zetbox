#define USE_STREAMS

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Kistl.API.Client
{
    /// <summary>
    /// Proxy Interface for IKistlService
    /// </summary>
    public interface IProxy : IDisposable
    {
        IEnumerable<IDataObject> GetList(Type type, int maxListCount, Expression filter, IEnumerable<Expression> orderBy);
        IEnumerable<IDataObject> GetListOf(Type type, int ID, string property);
        IDataObject GetObject(Type type, int ID);

        IEnumerable<IDataObject> SetObjects(IEnumerable<IDataObject> objects);

        IEnumerable<INewCollectionEntry<A, B>> FetchRelation<A, B>(int relationId, RelationEndRole role, IDataObject parent);

        /// <summary>
        /// Generates Objects &amp; Database. Throws a Exception if failed.
        /// </summary>
        void Generate();

        /// <summary>
        /// Hello World.
        /// </summary>
        /// <param name="name">A Name</param>
        /// <returns>"Hello " + name.</returns>
        // TODO: WTF?
        string HelloWorld(string name);
    }


    /// <summary>
    /// Proxy Singelton
    /// </summary>
    public static class Proxy
    {
        /// <summary>
        /// Singleton
        /// </summary>
        private static IProxy current;

        /// <summary>
        /// Sets the current Proxy, used in Unit Tests
        /// </summary>
        /// <param name="p"></param>
        public static void SetProxy(IProxy p)
        {
            current = p;
        }

        /// <summary>
        /// WCF Proxy für das KistlService
        /// </summary>
        public static IProxy Current
        {
            get
            {
                if (current == null)
                {
                    SetProxy(new ProxyImplementation());
                }
                return current;
            }
        }
    }

    /// <summary>
    /// Proxy Implementation
    /// </summary>
    internal class ProxyImplementation : IProxy
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
                if (ctx != null)
                    obj = (Kistl.API.IDataObject)ctx.Attach(obj);
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
                    if (_CurrentSerializer == null) throw new InvalidOperationException("Cannot create instance of KistlService.XmlSerializer");
                }

                return _CurrentSerializer;
            }
        }
        #endregion

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

        public IEnumerable<Kistl.API.IDataObject> GetList(Type type, int maxListCount, Expression filter, IEnumerable<Expression> orderBy)
        {
            using (TraceClient.TraceHelper.TraceMethodCall(type.ToString()))
            {
#if USE_STREAMS
                KistlServiceStreamsMessage msg = new KistlServiceStreamsMessage();
                msg.Type = new SerializableType(type);
                msg.MaxListCount = maxListCount;
                msg.Filter = filter != null ? SerializableExpression.FromExpression(filter) : null;
                msg.OrderBy = orderBy != null ? orderBy.Select(o => SerializableExpression.FromExpression(o)).ToList() : new List<SerializableExpression>();

                System.IO.MemoryStream s = serviceStreams.GetList(msg.ToStream());
                System.IO.BinaryReader sr = new System.IO.BinaryReader(s);

                List<Kistl.API.IDataObject> result = new List<Kistl.API.IDataObject>();
                bool cont = true;
                BinarySerializer.FromStream(out cont, sr);
                while (cont)
                {
                    long pos = s.Position;
                    SerializableType objType;
                    BinarySerializer.FromStream(out objType, sr);

                    s.Seek(pos, System.IO.SeekOrigin.Begin);

                    IDataObject obj = (IDataObject)objType.NewObject();
                    obj.FromStream(sr);

                    result.Add((Kistl.API.IDataObject)obj);
                    BinarySerializer.FromStream(out cont, sr);
                }

                return result;
#else
                string xml = service.GetList(type);
                return CurrentSerializer.ListFromXml(ctx, xml);
#endif
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="ID"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public IEnumerable<Kistl.API.IDataObject> GetListOf(Type type, int ID, string property)
        {
            using (TraceClient.TraceHelper.TraceMethodCall("{0} [{1}].{2}", type, ID, property))
            {
#if USE_STREAMS
                KistlServiceStreamsMessage msg = new KistlServiceStreamsMessage();
                msg.Type = new SerializableType(type);
                msg.ID = ID;
                msg.Property = property;
                System.IO.MemoryStream s = serviceStreams.GetListOf(msg.ToStream());
                System.IO.BinaryReader sr = new System.IO.BinaryReader(s);

                List<Kistl.API.IDataObject> result = new List<Kistl.API.IDataObject>();

                bool cont;
                BinarySerializer.FromStream(out cont, sr);
                while (cont)
                {
                    long pos = s.Position;
                    SerializableType objType;
                    BinarySerializer.FromStream(out objType, sr);

                    s.Seek(pos, System.IO.SeekOrigin.Begin);

                    IDataObject obj = (IDataObject)objType.NewObject();
                    obj.FromStream(sr);

                    result.Add((Kistl.API.IDataObject)obj);
                    BinarySerializer.FromStream(out cont, sr);
                }

                return result;
#else
                string xml = service.GetListOf(type, ID, property);
                return CurrentSerializer.ListFromXml(ctx, xml);
#endif
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Kistl.API.IDataObject GetObject(Type type, int ID)
        {
            using (TraceClient.TraceHelper.TraceMethodCall("{0} [{1}]", type, ID))
            {
#if USE_STREAMS
                KistlServiceStreamsMessage msg = new KistlServiceStreamsMessage();
                msg.Type = new SerializableType(type);
                msg.ID = ID;
                System.IO.MemoryStream s = serviceStreams.GetObject(msg.ToStream());
                System.IO.BinaryReader sr = new System.IO.BinaryReader(s);

                SerializableType objType;
                BinarySerializer.FromStream(out objType, sr);

                s.Seek(0, System.IO.SeekOrigin.Begin);

                IDataObject obj = (IDataObject)objType.NewObject();
                obj.FromStream(sr);

                return (Kistl.API.IDataObject)obj;
#else
                string xml = service.GetObject(type, ID);
                return CurrentSerializer.ObjectFromXml(ctx, xml);
#endif
            }
        }

        public IEnumerable<Kistl.API.IDataObject> SetObjects(IEnumerable<Kistl.API.IDataObject> objects)
        {
            using (TraceClient.TraceHelper.TraceMethodCall())
            {
#if USE_STREAMS
                // Serialize
                MemoryStream ms = new MemoryStream();
                BinaryWriter sw = new BinaryWriter(ms);
                foreach (IDataObject obj in objects)
                {
                    BinarySerializer.ToStream(true, sw);
                    obj.ToStream(sw);
                }
                BinarySerializer.ToStream(false, sw);

                // Set Operation
                System.IO.MemoryStream s = serviceStreams.SetObjects(ms);

                // Deserialize
                System.IO.BinaryReader sr = new System.IO.BinaryReader(s);

                List<IDataObject> result = new List<IDataObject>();
                bool @continue;
                BinarySerializer.FromStream(out @continue, sr);
                while (@continue)
                {
                    long pos = s.Position;
                    SerializableType objType;
                    BinarySerializer.FromStream(out objType, sr);

                    s.Seek(pos, System.IO.SeekOrigin.Begin);

                    IDataObject obj = (IDataObject)objType.NewObject();
                    obj.FromStream(sr);
                    result.Add(obj);
                    BinarySerializer.FromStream(out @continue, sr);
                }

                return result;

#else
                string xml = CurrentSerializer.XmlFromObject(obj);
                xml = service.SetObject(type, xml);
                return CurrentSerializer.ObjectFromXml(ctx, xml);
#endif
            }
        }

        public IEnumerable<INewCollectionEntry<A, B>> FetchRelation<A, B>(int relationId, RelationEndRole role, IDataObject parent)
        {
            // TODO: could be implemented in generated properties
            if(parent.ObjectState == DataObjectState.New)
                return new List<INewCollectionEntry<A, B>>();

            MemoryStream ms = serviceStreams.FetchRelation(relationId, (int)role, parent.ID);
            System.IO.BinaryReader sr = new System.IO.BinaryReader(ms);

            List<INewCollectionEntry<A, B>> result = new List<INewCollectionEntry<A, B>>();
            bool cont = true;
            BinarySerializer.FromStream(out cont, sr);
            while (cont)
            {
                long pos = ms.Position;
                SerializableType objType;
                BinarySerializer.FromStream(out objType, sr);

                ms.Seek(pos, System.IO.SeekOrigin.Begin);

                var obj = (INewCollectionEntry<A, B>)objType.NewObject();
                obj.FromStream(sr);

                result.Add(obj);
                BinarySerializer.FromStream(out cont, sr);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Generate()
        {
            using (TraceClient.TraceHelper.TraceMethodCall())
            {
                service.Generate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string HelloWorld(string name)
        {
            using (TraceClient.TraceHelper.TraceMethodCall(name))
            {
                return service.HelloWorld(name);
            }
        }

        #region IDisposable Members

        // as required on http://msdn2.microsoft.com/en-gb/ms182172.aspx
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose managed resources
                service.Close();
            }
            // free native resources
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion


    }
}
