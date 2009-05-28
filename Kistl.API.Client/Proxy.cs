#define USE_STREAMS

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Kistl.API.Client.KistlService;
using Kistl.API.Client.KistlServiceStreams;

namespace Kistl.API.Client
{
    /// <summary>
    /// Proxy Interface for IKistlService
    /// </summary>
    public interface IProxy
        : IDisposable
    {
        IEnumerable<IDataObject> GetList(InterfaceType ifType, int maxListCount, Expression filter, IEnumerable<Expression> orderBy, out List<IStreamable> auxObjects);
        IEnumerable<IDataObject> GetListOf(InterfaceType ifType, int ID, string property, out List<IStreamable> auxObjects);

        IEnumerable<IPersistenceObject> SetObjects(IEnumerable<IPersistenceObject> objects);

        IEnumerable<T> FetchRelation<T>(int relationId, RelationEndRole role, IDataObject parent, out List<IStreamable> auxObjects)
            where T : class, IRelationCollectionEntry;

        /// <summary>
        /// Generates Objects &amp; Database. Throws an Exception if failed.
        /// </summary>
        void Generate();

        /// <summary>
        /// Hello World.
        /// </summary>
        /// <param name="name">A Name</param>
        /// <returns>"Hello " + name.</returns>
        // TODO: WTF?
        [Obsolete]
        string HelloWorld(string name);
    }


    /// <summary>
    /// Proxy Singleton
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
    internal class ProxyImplementation
        : IProxy
    {
        #region XmlSerializer
        private interface IXmlSerializer
        {
            string XmlFromList(IEnumerable lst);
            string XmlFromObject(IDataObject obj);
            IDataObject ObjectFromXml(IKistlContext ctx, string xml);
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

            public string XmlFromObject(IDataObject obj)
            {
                XMLOBJECT result = new XMLOBJECT();
                result.Object = obj;
                return result.ToXmlString();
            }

            public IDataObject ObjectFromXml(IKistlContext ctx, string xml)
            {
                IDataObject obj = xml.FromXmlString<XMLOBJECT>().Object as IDataObject;
                if (ctx != null)
                    obj = (IDataObject)ctx.Attach(obj);
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
                    Type xmlCollection = Type.GetType("XMLObjectCollection, Kistl.Objects.Client");
                    Type xmlObj = Type.GetType("XMLObject, Kistl.Objects.Client");

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
        private KistlServiceClient service = new KistlServiceClient();

        /// <summary>
        /// WCF Proxy für das KistlServiceStreams instanzieren.
        /// Konfiguration lt. app.config File
        /// </summary>
        private KistlServiceStreamsClient serviceStreams = new KistlServiceStreamsClient();

        public IEnumerable<IDataObject> GetList(InterfaceType ifType, int maxListCount, Expression filter, IEnumerable<Expression> orderBy, out List<IStreamable> auxObjects)
        {
            using (TraceClient.TraceHelper.TraceMethodCall(ifType.ToString()))
            {
#if USE_STREAMS
                KistlServiceStreamsMessage msg = new KistlServiceStreamsMessage();
                msg.Type = new SerializableType(ifType);
                msg.MaxListCount = maxListCount;
                msg.Filter = filter != null ? SerializableExpression.FromExpression(filter) : null;
                msg.OrderBy = orderBy != null ? orderBy.Select(o => SerializableExpression.FromExpression(o)).ToList() : new List<SerializableExpression>();

                MemoryStream s = serviceStreams.GetList(msg.ToStream());
                return ReceiveObjects(s, out auxObjects).Cast<IDataObject>();
#else
                string xml = service.GetList(type);
                return CurrentSerializer.ListFromXml(ctx, xml);
#endif
            }
        }

        public IEnumerable<IDataObject> GetListOf(InterfaceType ifType, int ID, string property, out List<IStreamable> auxObjects)
        {
            using (TraceClient.TraceHelper.TraceMethodCall("{0} [{1}].{2}", ifType, ID, property))
            {
#if USE_STREAMS
                KistlServiceStreamsMessage msg = new KistlServiceStreamsMessage();
                msg.Type = new SerializableType(ifType);
                msg.ID = ID;
                msg.Property = property;
                MemoryStream s = serviceStreams.GetListOf(msg.ToStream());
                return ReceiveObjects(s, out auxObjects).Cast<IDataObject>();

#else
                string xml = service.GetListOf(type, ID, property);
                return CurrentSerializer.ListFromXml(ctx, xml);
#endif
            }
        }

        public IEnumerable<IPersistenceObject> SetObjects(IEnumerable<IPersistenceObject> objects)
        {
            using (TraceClient.TraceHelper.TraceMethodCall())
            {
#if USE_STREAMS
                // Serialize
                MemoryStream ms = new MemoryStream();
                BinaryWriter sw = new BinaryWriter(ms);
                foreach (var obj in objects)
                {
                    BinarySerializer.ToStream(true, sw);
                    obj.ToStream(sw, null);
                }
                BinarySerializer.ToStream(false, sw);

                MemoryStream s = serviceStreams.SetObjects(ms);

                // merge auxiliary objects into primary set objects result
                List<IStreamable> auxObjects;
                var results = ReceiveObjects(s, out auxObjects);
                return results.Concat(auxObjects).Cast<IPersistenceObject>();
#else
                string xml = CurrentSerializer.XmlFromObject(obj);
                xml = service.SetObject(type, xml);
                return CurrentSerializer.ObjectFromXml(ctx, xml);
#endif
            }
        }

        private static IEnumerable<IStreamable> ReceiveObjects(MemoryStream s, out List<IStreamable> auxObjects)
        {
            var result = ReceiveObjectList(s);
            auxObjects = ReceiveObjectList(s);
            return result;
        }

        private static List<IStreamable> ReceiveObjectList(MemoryStream s)
        {
            BinaryReader sr = new BinaryReader(s);

            List<IStreamable> result = new List<IStreamable>();
            bool cont = true;
            BinarySerializer.FromStream(out cont, sr);
            while (cont)
            {
                long pos = s.Position;
                SerializableType objType;
                BinarySerializer.FromStream(out objType, sr);

                s.Seek(pos, SeekOrigin.Begin);

                IStreamable obj = (IStreamable)objType.NewObject();
                obj.FromStream(sr);

                result.Add((IStreamable)obj);
                BinarySerializer.FromStream(out cont, sr);
            }
            return result;
        }

        public IEnumerable<T> FetchRelation<T>(int relationId, RelationEndRole role, IDataObject parent, out List<IStreamable> auxObjects)
            where T : class, IRelationCollectionEntry
        {
            using (TraceClient.TraceHelper.TraceMethodCall("Fetching relation"))
            {
                //Trace.TraceWarning("FetchRelation(ID={0},role={1},parentId={2}): enter", relationId, role, parent.ID);
                // TODO: could be implemented in generated properties
                if (parent.ObjectState == DataObjectState.New)
                {
                    auxObjects = new List<IStreamable>();
                    return new List<T>();
                }

                MemoryStream ms = serviceStreams.FetchRelation(relationId, (int)role, parent.ID);

                return ReceiveObjects(ms, out auxObjects).Cast<T>();
            }
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
        [Obsolete]
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
