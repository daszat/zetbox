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
using Kistl.API.Utils;

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

        IEnumerable<T> FetchRelation<T>(Guid relationId, RelationEndRole role, IDataObject parent, out List<IStreamable> auxObjects)
            where T : class, IRelationCollectionEntry;
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
        /// <summary>
        /// WCF Proxy für das KistlService instanzieren.
        /// Konfiguration lt. app.config File
        /// </summary>
        private KistlServiceClient service = new KistlServiceClient();

        public IEnumerable<IDataObject> GetList(InterfaceType ifType, int maxListCount, Expression filter, IEnumerable<Expression> orderBy, out List<IStreamable> auxObjects)
        {
            using (Logging.Facade.TraceMethodCall(ifType.ToString()))
            {
                MemoryStream s = service.GetList(
                    new SerializableType(ifType), 
                    maxListCount, 
                    filter != null ? SerializableExpression.FromExpression(filter) : null,
                    orderBy != null ? orderBy.Select(o => SerializableExpression.FromExpression(o)).ToArray() : null);
                return ReceiveObjects(s, out auxObjects).Cast<IDataObject>();
            }
        }

        public IEnumerable<IDataObject> GetListOf(InterfaceType ifType, int ID, string property, out List<IStreamable> auxObjects)
        {
            using (Logging.Facade.TraceMethodCall("{0} [{1}].{2}", ifType, ID, property))
            {
                MemoryStream s = service.GetListOf(new SerializableType(ifType), ID, property);
                return ReceiveObjects(s, out auxObjects).Cast<IDataObject>();
            }
        }

        public IEnumerable<IPersistenceObject> SetObjects(IEnumerable<IPersistenceObject> objects)
        {
            using (Logging.Facade.TraceMethodCall())
            {
                // Serialize
                MemoryStream ms = new MemoryStream();
                BinaryWriter sw = new BinaryWriter(ms);
                foreach (var obj in objects)
                {
                    BinarySerializer.ToStream(true, sw);
                    obj.ToStream(sw, new HashSet<IStreamable>());
                }
                BinarySerializer.ToStream(false, sw);

                MemoryStream s = service.SetObjects(ms);

                // merge auxiliary objects into primary set objects result
                List<IStreamable> auxObjects;
                var results = ReceiveObjects(s, out auxObjects);
                return results.Concat(auxObjects).Cast<IPersistenceObject>();
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

        public IEnumerable<T> FetchRelation<T>(Guid relationId, RelationEndRole role, IDataObject parent, out List<IStreamable> auxObjects)
            where T : class, IRelationCollectionEntry
        {
            using (Logging.Facade.TraceMethodCall("Fetching relation"))
            {
                //Trace.TraceWarning("FetchRelation(ID={0},role={1},parentId={2}): enter", relationId, role, parent.ID);
                // TODO: could be implemented in generated properties
                if (parent.ObjectState == DataObjectState.New)
                {
                    auxObjects = new List<IStreamable>();
                    return new List<T>();
                }

                MemoryStream ms = service.FetchRelation(relationId, (int)role, parent.ID);

                return ReceiveObjects(ms, out auxObjects).Cast<T>();
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
