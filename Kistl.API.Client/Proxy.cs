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
        IEnumerable<IDataObject> GetList(IKistlContext ctx, InterfaceType ifType, int maxListCount, bool eagerLoadLists, IEnumerable<Expression> filter, IEnumerable<Expression> orderBy, out List<IStreamable> auxObjects);
        IEnumerable<IDataObject> GetListOf(IKistlContext ctx, InterfaceType ifType, int ID, string property, out List<IStreamable> auxObjects);

        IEnumerable<IPersistenceObject> SetObjects(IKistlContext ctx, IEnumerable<IPersistenceObject> objects, IEnumerable<ObjectNotificationRequest> notificationRequests);

        IEnumerable<T> FetchRelation<T>(IKistlContext ctx, Guid relationId, RelationEndRole role, IDataObject parent, out List<IStreamable> auxObjects)
            where T : class, IRelationCollectionEntry;

        Stream GetBlobStream(int ID);
        Kistl.App.Base.Blob SetBlobStream(IKistlContext ctx, Stream stream, string filename, string mimetype);
    }

    /// <summary>
    /// Proxy Implementation
    /// </summary>
    internal class ProxyImplementation
        : IProxy
    {
        private InterfaceType.Factory _iftFactory;

        public ProxyImplementation(InterfaceType.Factory iftFactory)
        {
            _iftFactory = iftFactory;
        }

        private readonly static object _lock = new object();

        private KistlServiceClient _service;

        /// <summary>
        /// Instantiates a WCF Proxy for KistlService, configured according 
        /// to the app.config file.
        /// </summary>        
        private KistlServiceClient Service
        {
            get
            {
                lock (_lock)
                {
                    if (_service == null || _service.State != System.ServiceModel.CommunicationState.Opened)
                    {
                        Logging.Facade.Info("Initializing Service");
                        _service = new KistlServiceClient();
                    }
                    return _service;
                }
            }
        }

        public IEnumerable<IDataObject> GetList(IKistlContext ctx, InterfaceType ifType, int maxListCount, bool eagerLoadLists, IEnumerable<Expression> filter, IEnumerable<Expression> orderBy, out List<IStreamable> auxObjects)
        {
            using (Logging.Facade.InfoTraceMethodCallFormat("GetList[{0}]", ifType.ToString()))
            {
                using (MemoryStream s = Service.GetList(
                    ifType.ToSerializableType(),
                    maxListCount,
                    eagerLoadLists,
                    filter != null ? filter.Select(f => SerializableExpression.FromExpression(f, _iftFactory)).ToArray() : null,
                    orderBy != null ? orderBy.Select(o => SerializableExpression.FromExpression(o, _iftFactory)).ToArray() : null))
                {
                    using (var sr = new BinaryReader(s))
                    {
                        return ReceiveObjects(ctx, sr, out auxObjects).Cast<IDataObject>();
                    }
                }
            }
        }

        public IEnumerable<IDataObject> GetListOf(IKistlContext ctx, InterfaceType ifType, int ID, string property, out List<IStreamable> auxObjects)
        {
            using (Logging.Facade.InfoTraceMethodCallFormat("{0} [{1}].{2}", ifType, ID, property))
            {
                using (MemoryStream s = Service.GetListOf(ifType.ToSerializableType(), ID, property))
                {
                    using (var sr = new BinaryReader(s))
                    {
                        var result = ReceiveObjects(ctx, sr, out auxObjects).Cast<IDataObject>();
                        return result;
                    }
                }
            }
        }

        public IEnumerable<IPersistenceObject> SetObjects(IKistlContext ctx, IEnumerable<IPersistenceObject> objects, IEnumerable<ObjectNotificationRequest> notficationRequests)
        {
            using (Logging.Facade.InfoTraceMethodCall("SetObjects"))
            {
                // Serialize
                using (var ms = new MemoryStream())
                {
                    using (var sw = new BinaryWriter(ms))
                    {
                        foreach (var obj in objects)
                        {
                            BinarySerializer.ToStream(true, sw);
                            obj.ToStream(sw, new HashSet<IStreamable>(), false);
                        }
                        BinarySerializer.ToStream(false, sw);

                        using (MemoryStream s = Service.SetObjects(ms, notficationRequests.ToArray()))
                        {
                            using (var sr = new BinaryReader(s))
                            {
                                // merge auxiliary objects into primary set objects result
                                List<IStreamable> auxObjects;
                                var receivedObjects = ReceiveObjects(ctx, sr, out auxObjects);
                                var result = receivedObjects.Concat(auxObjects).Cast<IPersistenceObject>();
                                return result;
                            }
                        }
                    }
                }
            }
        }

        private IEnumerable<IStreamable> ReceiveObjects(IKistlContext ctx, BinaryReader sr, out List<IStreamable> auxObjects)
        {
            var result = ReceiveObjectList(ctx, sr);
            auxObjects = ReceiveObjectList(ctx, sr);
            Logging.Facade.DebugFormat("retrieved: {0} objects; {1} auxObjects", result.Count(), auxObjects.Count());
            return result;
        }

        private List<IStreamable> ReceiveObjectList(IKistlContext ctx, BinaryReader sr)
        {
            List<IStreamable> result = new List<IStreamable>();
            bool cont = true;
            BinarySerializer.FromStream(out cont, sr);
            while (cont)
            {
                SerializableType objType;
                BinarySerializer.FromStream(out objType, sr);

                IStreamable obj = (IStreamable)ctx.CreateUnattached(_iftFactory(objType.GetSystemType()));
                obj.FromStream(sr);

                result.Add((IStreamable)obj);
                BinarySerializer.FromStream(out cont, sr);
            }
            return result;
        }

        public IEnumerable<T> FetchRelation<T>(IKistlContext ctx, Guid relationId, RelationEndRole role, IDataObject parent, out List<IStreamable> auxObjects)
            where T : class, IRelationCollectionEntry
        {
            using (Logging.Facade.InfoTraceMethodCallFormat("Fetching relation: ID=[{0}],role=[{1}],parentId=[{2}]", relationId, role, parent.ID))
            {
                // TODO: could be implemented in generated properties
                if (parent.ObjectState == DataObjectState.New)
                {
                    auxObjects = new List<IStreamable>();
                    return new List<T>();
                }

                using (MemoryStream s = Service.FetchRelation(relationId, (int)role, parent.ID))
                {
                    using (var sr = new BinaryReader(s))
                    {
                        return ReceiveObjects(ctx, sr, out auxObjects).Cast<T>();
                    }
                }
            }
        }

        #region IDisposable Members

        // as recommended on http://msdn2.microsoft.com/en-gb/ms182172.aspx
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose managed resources
                if (_service != null)
                {
                    Logging.Facade.Debug("Closing Service");
                    _service.Close();
                    ((IDisposable)_service).Dispose();
                    _service = null;
                }
            }
            // free native resources
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        public Stream GetBlobStream(int ID)
        {
            using (Logging.Facade.InfoTraceMethodCallFormat("GetBlobStream: ID=[{0}]", ID))
            {
                return Service.GetBlobStream(ID);
            }
        }

        public Kistl.App.Base.Blob SetBlobStream(IKistlContext ctx, Stream stream, string filename, string mimetype)
        {
            using (Logging.Facade.InfoTraceMethodCallFormat("SetBlobStream: filename=[{0}]", filename))
            {
                Stream result;
                int id = Service.SetBlobStream(filename, mimetype, stream, out result);
                try
                {
                    using (var sr = new BinaryReader(result))
                    {
                        return ReceiveObjectList(ctx, sr).Cast<Kistl.App.Base.Blob>().Single();
                    }
                }
                finally
                {
                    result.Dispose();
                }
            }
        }
    }
}
