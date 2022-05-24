// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

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
    using System.Threading.Tasks;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.API.Server;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Packaging;
    using Zetbox.App.Test;
    using Zetbox.DalProvider.Memory;

    public class ProxyMock
        : IProxy
    {
        private readonly InterfaceType.Factory _iftFactory;
        private readonly UnattachedObjectFactory _unattachedObjectFactory;
        private readonly BaseMemoryContext _backingStore;
        private readonly MemoryObjectHandlerFactory _memoryFactory;
        private readonly TypeMap _map;
        private readonly IFrozenContext _frozenCtx;

        public ProxyMock(InterfaceType.Factory iftFactory, UnattachedObjectFactory unattachedObjectFactory, BaseMemoryContext backingStore, IFrozenContext frozen, TypeMap map)
        {
            _iftFactory = iftFactory;
            _unattachedObjectFactory = unattachedObjectFactory;
            _backingStore = backingStore;
            _frozenCtx = frozen;
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

        public async Task<Tuple<IEnumerable<IDataObject>, List<IStreamable>>> GetObjects(IReadOnlyZetboxContext requestingCtx, InterfaceType ifType, Expression query)
        {
            List<IStreamable> tmpAuxObjects = null;
            IEnumerable<IDataObject> result = null;

            var handler = _memoryFactory.GetServerObjectHandler(ifType);
            var objects = await handler.GetObjects(
                ZetboxGeneratedVersionAttribute.Current,
                requestingCtx,
                query);
            var bytes = SendObjects(objects, true).ToArray();

            using (var sr = new ZetboxStreamReader(_map, new BinaryReader(new MemoryStream(bytes))))
            {
                result = ReceiveObjects(sr, out tmpAuxObjects).Cast<IDataObject>();
            }
            return Tuple.Create(result, tmpAuxObjects);
        }

        public async Task<Tuple<IEnumerable<IDataObject>, List<IStreamable>>> GetListOf(InterfaceType ifType, int ID, string property)
        {
            List<IStreamable> tmpAuxObjects = null;
            IEnumerable<IDataObject> result = null;

            var handler = _memoryFactory.GetServerObjectHandler(ifType);
            var objects = await handler.GetListOf(ZetboxGeneratedVersionAttribute.Current, _backingStore, ID, property);
            var bytes = SendObjects(objects, true).ToArray();

            using (var sr = new ZetboxStreamReader(_map, new BinaryReader(new MemoryStream(bytes))))
            {
                result = ReceiveObjects(sr, out tmpAuxObjects).Cast<IDataObject>();
            }

            return Tuple.Create(result, tmpAuxObjects);
        }

        public async Task<IEnumerable<IPersistenceObject>> SetObjects(IEnumerable<IPersistenceObject> objects, IEnumerable<ObjectNotificationRequest> notficationRequests)
        {
            IEnumerable<IPersistenceObject> result = null;

            // Serialize
            using (var ms = new MemoryStream())
            using (var sw = new ZetboxStreamWriter(_map, new BinaryWriter(ms)))
            {
                SendObjects(objects, sw);

                var handler = _memoryFactory.GetServerObjectSetHandler();
                var changedObjects = (await handler
                    .SetObjects(ZetboxGeneratedVersionAttribute.Current, _backingStore, objects, notficationRequests ?? new ObjectNotificationRequest[0]))
                    .Cast<IStreamable>();
                var bytes = SendObjects(changedObjects, true).ToArray();

                using (var sr = new ZetboxStreamReader(_map, new BinaryReader(new MemoryStream(bytes))))
                {
                    // merge auxiliary objects into primary set objects result
                    List<IStreamable> auxObjects;
                    var receivedObjects = ReceiveObjects(sr, out auxObjects);
                    result = receivedObjects.Concat(auxObjects).Cast<IPersistenceObject>();
                }
            }

            return result;
        }

        public async Task<Tuple<IEnumerable<T>, List<IStreamable>>> FetchRelation<T>(Guid relationId, RelationEndRole role, int parentId, InterfaceType parentIfType)
            where T : class, IRelationEntry
        {
            // TODO: could be implemented in generated properties
            if (parentId <= Helper.INVALIDID)
            {
                return Tuple.Create(Enumerable.Empty<T>(), new List<IStreamable>());
            }

            Relation rel = _frozenCtx.FindPersistenceObject<Relation>(relationId);

            IEnumerable<T> result = null;
            List<IStreamable> tmpAuxObjects = null;

            var handler = _memoryFactory
                .GetServerCollectionHandler(
                    _backingStore,
                    _iftFactory(await rel.A.Type.GetDataType()),
                    _iftFactory(await rel.B.Type.GetDataType()),
                    role);
            var objects = (await handler
                .GetCollectionEntries(ZetboxGeneratedVersionAttribute.Current, _backingStore, relationId, role, parentId))
                .Cast<IStreamable>();
            var bytes = SendObjects(objects, true).ToArray();

            using (MemoryStream s = new MemoryStream(bytes))
            using (var sr = new ZetboxStreamReader(_map, new BinaryReader(s)))
            {
                result = ReceiveObjects(sr, out tmpAuxObjects).Cast<T>();
            }

            return Tuple.Create(result, tmpAuxObjects);
        }

        public Task<Stream> GetBlobStream(int ID)
        {
            var handler = _memoryFactory.GetServerDocumentHandler();
            return handler.GetBlobStream(ZetboxGeneratedVersionAttribute.Current, _backingStore, ID);
        }

        public async Task<Zetbox.App.Base.Blob> SetBlobStream(Stream stream, string filename, string mimetype)
        {
            Zetbox.App.Base.Blob result = null;
            var handler = _memoryFactory.GetServerDocumentHandler();
            var serverBlob = await handler.SetBlobStream(ZetboxGeneratedVersionAttribute.Current, _backingStore, stream, filename, mimetype);

            BlobResponse resp = new BlobResponse();
            resp.ID = serverBlob.ID;
            resp.BlobInstance = SendObjects(new IDataObject[] { serverBlob }, true);

            using (var sr = new ZetboxStreamReader(_map, new BinaryReader(resp.BlobInstance)))
            {
                result = ReceiveObjectList(sr).Cast<Zetbox.App.Base.Blob>().Single();
            }
            return result;
        }

        public Task<Tuple<object, IEnumerable<IPersistenceObject>, List<IStreamable>>> InvokeServerMethod(InterfaceType ifType, int ID, string method, Type retValType, IEnumerable<Type> parameterTypes, IEnumerable<object> parameter, IEnumerable<IPersistenceObject> objects, IEnumerable<ObjectNotificationRequest> notificationRequests)
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

        private IEnumerable<IStreamable> ReceiveObjects(ZetboxStreamReader sr, out List<IStreamable> auxObjects)
        {
            var result = ReceiveObjectList(sr);
            auxObjects = ReceiveObjectList(sr);
            return result;
        }

        private List<IStreamable> ReceiveObjectList(ZetboxStreamReader sr)
        {
            List<IStreamable> result = new List<IStreamable>();
            var cont = sr.ReadBoolean();
            while (cont)
            {
                var objType = sr.ReadSerializableType();

                IStreamable obj = (IStreamable)_unattachedObjectFactory(_iftFactory(objType.GetSystemType()));
                obj.FromStream(sr);

                result.Add((IStreamable)obj);
                cont = sr.ReadBoolean();
            }
            return result;
        }
    }
}