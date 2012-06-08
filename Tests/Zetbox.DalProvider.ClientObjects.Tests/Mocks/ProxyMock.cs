
namespace Zetbox.DalProvider.Client.Mocks
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.API.Server;
    using Zetbox.App.Packaging;
    using Zetbox.App.Test;
    using Zetbox.DalProvider.Memory;
    using Zetbox.App.Base;
    using Zetbox.API.Utils;

    public class ProxyMock
        : IProxy
    {
        private readonly InterfaceType.Factory _iftFactory;
        private readonly BaseMemoryContext _backingStore;
        private readonly MemoryObjectHandlerFactory _memoryFactory;
        private readonly TypeMap _map;

        public ProxyMock(InterfaceType.Factory iftFactory, BaseMemoryContext backingStore, IFrozenContext frozen, TypeMap map)
        {
            _iftFactory = iftFactory;
            _backingStore = backingStore;
            _memoryFactory = new MemoryObjectHandlerFactory();
            _map = map;

            var generatedAssembly = System.Reflection.Assembly.Load(MemoryProvider.GeneratedAssemblyName);
            Importer.LoadFromXml(_backingStore, generatedAssembly.GetManifestResourceStream("Zetbox.Objects.MemoryImpl.FrozenObjects.xml"), "FrozenObjects.xml from assembly");

            // create default test data

            var list = new List<TestObjClass>();
            while (list.Count < 2)
            {
                var newObj = _backingStore.Create<TestObjClass>();
                newObj.ObjectProp = null; // kunde;
                newObj.StringProp = "blah" + list.Count;
                list.Add(newObj);
            }

            list[0].StringProp = "First";
            list[0].TestEnumProp = TestEnum.First;

            list[1].StringProp = "Second";
            list[1].TestEnumProp = TestEnum.Second;

            _backingStore.SubmitChanges();
        }

        public IEnumerable<IDataObject> GetList(IZetboxContext ctx, InterfaceType ifType, int maxListCount, bool eagerLoadLists, IEnumerable<Expression> filter, IEnumerable<OrderBy> orderBy, out List<IStreamable> auxObjects)
        {
            List<IStreamable> tmpAuxObjects = null;
            IEnumerable<IDataObject> result = null;

            var handler = _memoryFactory.GetServerObjectHandler(ifType);
            var objects = handler.GetList(
                ZetboxGeneratedVersionAttribute.Current,
                _backingStore,
                maxListCount,
                filter != null ? filter.ToList() : null,
                orderBy != null ? orderBy.ToList() : null);
            var bytes = SendObjects(objects, eagerLoadLists).ToArray();

            using (var sr = new ZetboxStreamReader(_map, new BinaryReader(new MemoryStream(bytes))))
            {
                result = ReceiveObjects(ctx, sr, out tmpAuxObjects).Cast<IDataObject>();
            }
            auxObjects = tmpAuxObjects;
            return result;
        }

        public IEnumerable<IDataObject> GetListOf(IZetboxContext ctx, InterfaceType ifType, int ID, string property, out List<IStreamable> auxObjects)
        {
            List<IStreamable> tmpAuxObjects = null;
            IEnumerable<IDataObject> result = null;

            var handler = _memoryFactory.GetServerObjectHandler(ifType);
            var objects = handler.GetListOf(ZetboxGeneratedVersionAttribute.Current, _backingStore, ID, property);
            var bytes = SendObjects(objects, true).ToArray();

            using (var sr = new ZetboxStreamReader(_map, new BinaryReader(new MemoryStream(bytes))))
            {
                result = ReceiveObjects(ctx, sr, out tmpAuxObjects).Cast<IDataObject>();
            }

            auxObjects = tmpAuxObjects;
            return result;
        }

        public IEnumerable<IPersistenceObject> SetObjects(IZetboxContext ctx, IEnumerable<IPersistenceObject> objects, IEnumerable<ObjectNotificationRequest> notficationRequests)
        {
            IEnumerable<IPersistenceObject> result = null;

            // Serialize
            using (var ms = new MemoryStream())
            using (var sw = new ZetboxStreamWriter(_map, new BinaryWriter(ms)))
            {
                SendObjects(objects, sw);

                var handler = _memoryFactory.GetServerObjectSetHandler();
                var changedObjects = handler
                    .SetObjects(ZetboxGeneratedVersionAttribute.Current, _backingStore, objects, notficationRequests ?? new ObjectNotificationRequest[0])
                    .Cast<IStreamable>();
                var bytes = SendObjects(changedObjects, true).ToArray();

                using (var sr = new ZetboxStreamReader(_map, new BinaryReader(new MemoryStream(bytes))))
                {
                    // merge auxiliary objects into primary set objects result
                    List<IStreamable> auxObjects;
                    var receivedObjects = ReceiveObjects(ctx, sr, out auxObjects);
                    result = receivedObjects.Concat(auxObjects).Cast<IPersistenceObject>();
                }
            }

            return result;
        }

        public IEnumerable<T> FetchRelation<T>(IZetboxContext ctx, Guid relationId, RelationEndRole role, IDataObject parent, out List<IStreamable> auxObjects)
            where T : class, IRelationEntry
        {
            // TODO: could be implemented in generated properties
            if (parent.ObjectState == DataObjectState.New)
            {
                auxObjects = new List<IStreamable>();
                return new List<T>();
            }

            Relation rel = ctx.FindPersistenceObject<Relation>(relationId);

            IEnumerable<T> result = null;
            List<IStreamable> tmpAuxObjects = null;

            var handler = _memoryFactory
                .GetServerCollectionHandler(
                    _backingStore,
                    _iftFactory(rel.A.Type.GetDataType()),
                    _iftFactory(rel.B.Type.GetDataType()),
                    role);
            var objects = handler
                .GetCollectionEntries(ZetboxGeneratedVersionAttribute.Current, _backingStore, relationId, role, parent.ID)
                .Cast<IStreamable>();
            var bytes = SendObjects(objects, true).ToArray();

            using (MemoryStream s = new MemoryStream(bytes))
            using (var sr = new ZetboxStreamReader(_map, new BinaryReader(s)))
            {
                result = ReceiveObjects(ctx, sr, out tmpAuxObjects).Cast<T>();
            }

            auxObjects = tmpAuxObjects;
            return result;
        }

        public Stream GetBlobStream(int ID)
        {
            Stream result = null;
            var handler = _memoryFactory.GetServerDocumentHandler();
            result = handler.GetBlobStream(ZetboxGeneratedVersionAttribute.Current, _backingStore, ID);
            return result;
        }

        public Zetbox.App.Base.Blob SetBlobStream(IZetboxContext ctx, Stream stream, string filename, string mimetype)
        {
            Zetbox.App.Base.Blob result = null;
            var handler = _memoryFactory.GetServerDocumentHandler();
            var serverBlob = handler.SetBlobStream(ZetboxGeneratedVersionAttribute.Current, _backingStore, stream, filename, mimetype);

            BlobResponse resp = new BlobResponse();
            resp.ID = serverBlob.ID;
            resp.BlobInstance = SendObjects(new IDataObject[] { serverBlob }, true);

            using (var sr = new ZetboxStreamReader(_map, new BinaryReader(resp.BlobInstance)))
            {
                result = ReceiveObjectList(ctx, sr).Cast<Zetbox.App.Base.Blob>().Single();
            }
            return result;
        }

        public object InvokeServerMethod(IZetboxContext ctx, InterfaceType ifType, int ID, string method, Type retValType, IEnumerable<Type> parameterTypes, IEnumerable<object> parameter, IEnumerable<IPersistenceObject> objects, IEnumerable<ObjectNotificationRequest> notificationRequests, out IEnumerable<IPersistenceObject> changedObjects, out List<IStreamable> auxObjects)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        private static void SendObjects(IEnumerable<IPersistenceObject> objects, ZetboxStreamWriter sw)
        {
            foreach (var obj in objects)
            {
                sw.Write(true);
                obj.ToStream(sw, new HashSet<IStreamable>(), false);
            }
            sw.Write(false);
        }

        /// <summary>
        /// Serializes a list of objects onto a <see cref="MemoryStream"/>.
        /// </summary>
        /// <param name="lst">the list of objects to send</param>
        /// <param name="eagerLoadLists">True if Lists should be eager loaded</param>
        /// <returns>a memory stream containing all objects and all eagerly loaded auxiliary objects</returns>
        private MemoryStream SendObjects(IEnumerable<IStreamable> lst, bool eagerLoadLists)
        {
            HashSet<IStreamable> sentObjects = new HashSet<IStreamable>();
            HashSet<IStreamable> auxObjects = new HashSet<IStreamable>();

            MemoryStream result = new MemoryStream();
            ZetboxStreamWriter sw = new ZetboxStreamWriter(_map, new BinaryWriter(result));
            foreach (IStreamable obj in lst)
            {
                sw.Write(true);
                // don't check sentObjects here, because a list might contain items twice
                obj.ToStream(sw, auxObjects, eagerLoadLists);
                sentObjects.Add(obj);
            }
            sw.Write(false);

            SendAuxiliaryObjects(sw, auxObjects, sentObjects, eagerLoadLists);

            // https://connect.microsoft.com/VisualStudio/feedback/details/541494/wcf-streaming-issue
            sw.Write(false);
            sw.Write(false);
            sw.Write(false);

            result.Seek(0, SeekOrigin.Begin);
            return result;
        }

        /// <summary>
        /// Sends a list of auxiliary objects to the specified BinaryWriter while avoiding to send objects twice.
        /// </summary>
        /// <param name="sw">the stream to write to</param>
        /// <param name="auxObjects">a set of objects to send; will not be modified by this call</param>
        /// <param name="sentObjects">a set objects already sent; receives all newly sent objects too</param>
        /// <param name="eagerLoadLists">True if Lists should be eager loaded</param>
        private static void SendAuxiliaryObjects(ZetboxStreamWriter sw, HashSet<IStreamable> auxObjects, HashSet<IStreamable> sentObjects, bool eagerLoadLists)
        {
            // clone auxObjects to avoid modification
            auxObjects = new HashSet<IStreamable>(auxObjects);
            auxObjects.ExceptWith(sentObjects);
            // send all eagerly loaded objects
            while (auxObjects.Count > 0)
            {
                HashSet<IStreamable> secondTierAuxObjects = new HashSet<IStreamable>();
                foreach (var aux in auxObjects.Where(o => o != null))
                {
                    sw.Write(true);
                    aux.ToStream(sw, secondTierAuxObjects, eagerLoadLists);
                    sentObjects.Add(aux);
                }
                // check whether new objects where eagerly loaded
                secondTierAuxObjects.ExceptWith(sentObjects);
                auxObjects = secondTierAuxObjects;
            }
            // finish list
            sw.Write(false);
        }

        private IEnumerable<IStreamable> ReceiveObjects(IZetboxContext ctx, ZetboxStreamReader sr, out List<IStreamable> auxObjects)
        {
            var result = ReceiveObjectList(ctx, sr);
            auxObjects = ReceiveObjectList(ctx, sr);
            return result;
        }

        private List<IStreamable> ReceiveObjectList(IZetboxContext ctx, ZetboxStreamReader sr)
        {
            List<IStreamable> result = new List<IStreamable>();
            var cont = sr.ReadBoolean();
            while (cont)
            {
                var objType = sr.ReadSerializableType();

                IStreamable obj = (IStreamable)ctx.Internals().CreateUnattached(_iftFactory(objType.GetSystemType()));
                obj.FromStream(sr);

                result.Add((IStreamable)obj);
                cont = sr.ReadBoolean();
            }
            return result;
        }
    }
}