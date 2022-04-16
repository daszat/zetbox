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
#define USE_STREAMS

namespace Zetbox.API.Client
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Security.Authentication;
    using System.Text;
    using System.Threading.Tasks;
    using Zetbox.API.Client.PerfCounter;
    using Zetbox.API.Client.ZetboxService;
    using Zetbox.API.Utils;

    public delegate object UnattachedObjectFactory(InterfaceType ifType);

    /// <summary>
    /// Proxy Interface for IZetboxService
    /// </summary>
    public interface IProxy
        : IDisposable
    {
        Task<Tuple<IEnumerable<IDataObject>, List<IStreamable>>> GetObjects(IReadOnlyZetboxContext requestingCtx, InterfaceType ifType, Expression query);
        Task<Tuple<IEnumerable<IDataObject>, List<IStreamable>>> GetListOf(InterfaceType ifType, int ID, string property);

        Task<IEnumerable<IPersistenceObject>> SetObjects(IEnumerable<IPersistenceObject> objects, IEnumerable<ObjectNotificationRequest> notificationRequests);

        Task<Tuple<object, IEnumerable<IPersistenceObject>, List<IStreamable>>> InvokeServerMethod(InterfaceType ifType, int ID, string method, Type retValType, IEnumerable<Type> parameterTypes, IEnumerable<object> parameter, IEnumerable<IPersistenceObject> objects, IEnumerable<ObjectNotificationRequest> notificationRequests);

        Task<Tuple<IEnumerable<T>, List<IStreamable>>> FetchRelation<T>(Guid relationId, RelationEndRole role, int parentId, InterfaceType parentIfType)
            where T : class, IRelationEntry;

        Task<Stream> GetBlobStream(int ID);
        Task<Zetbox.App.Base.Blob> SetBlobStream(Stream stream, string filename, string mimetype);
    }

    #region ZetboxServerIOException
    /// <summary>
    /// This exception is thrown when communication with the zetbox server has failed. This is usefull to distinguish it from the cases where the server throwed an internal error from IO errors.
    /// </summary>
    [Serializable]
    public class ZetboxServerIOException : IOException
    {
        public ZetboxServerIOException() : this("Error when accessing server.") { }
        public ZetboxServerIOException(string message) : base(message) { }
        public ZetboxServerIOException(string message, Exception inner) : base(message, inner) { }
        protected ZetboxServerIOException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

    }
    #endregion


    /// <summary>
    /// Proxy Implementation
    /// </summary>
    internal class ProxyImplementation
        : IProxy
    {
        private const int MAX_RETRY_COUNT = 2;

        private readonly InterfaceType.Factory _iftFactory;
        private readonly IImplementationTypeChecker _implTypeChecker;
        private readonly UnattachedObjectFactory _unattachedObjectFactory;
        private readonly ZetboxService.IZetboxService _service;
        private readonly IPerfCounter _perfCounter;
        private readonly ZetboxStreamReader.Factory _readerFactory;
        private readonly ZetboxStreamWriter.Factory _writerFactory;
        private readonly IEnumerable<SerializingTypeMap> _typeMaps;

        public ProxyImplementation(InterfaceType.Factory iftFactory, IImplementationTypeChecker implTypeChecker, UnattachedObjectFactory unattachedObjectFactory, ZetboxService.IZetboxService service, IPerfCounter perfCounter, ZetboxStreamReader.Factory readerFactory, ZetboxStreamWriter.Factory writerFactory, IEnumerable<SerializingTypeMap> typeMaps)
        {
            if (perfCounter == null) throw new ArgumentNullException("perfCounter");
            if (readerFactory == null) throw new ArgumentNullException("readerFactory");
            if (writerFactory == null) throw new ArgumentNullException("writerFactory");

            _iftFactory = iftFactory;
            _implTypeChecker = implTypeChecker;
            _unattachedObjectFactory = unattachedObjectFactory;
            _service = service;
            _perfCounter = perfCounter;
            _readerFactory = readerFactory;
            _writerFactory = writerFactory;
            _typeMaps = typeMaps;
        }

        public async Task<Tuple<IEnumerable<IDataObject>, List<IStreamable>>> GetObjects(IReadOnlyZetboxContext requestingCtx, InterfaceType ifType, Expression query)
        {
            int resultCount = 0;
            List<IStreamable> tmpAuxObjects = null;
            var ticks = _perfCounter.IncrementGetObjects(ifType);
            try
            {
                byte[] bytes = null;

                await MakeRequest(async () =>
                {
                    bytes = await _service.GetObjects(
                        ZetboxGeneratedVersionAttribute.Current,
                        SerializableExpression.FromExpression(query, requestingCtx, _iftFactory, _implTypeChecker, _typeMaps));
                });

                Logging.Facade.DebugFormat("GetObjects retrieved: {0:n0} bytes", bytes.Length);

                IEnumerable<IDataObject> result = null;
                using (var sr = _readerFactory.Invoke(new BinaryReader(new MemoryStream(bytes))))
                {
                    result = ReceiveObjects(sr, out tmpAuxObjects).Cast<IDataObject>();
                }
                resultCount = result.Count();
                return Tuple.Create(result, tmpAuxObjects);
            }
            finally
            {
                _perfCounter.DecrementGetObjects(ifType, resultCount, ticks);
            }
        }

        public async Task<Tuple<IEnumerable<IDataObject>, List<IStreamable>>> GetListOf(InterfaceType ifType, int ID, string property)
        {
            List<IStreamable> tmpAuxObjects = null;
            int resultCount = 0;
            var _ifType = ifType.ToSerializableType();
            var ticks = _perfCounter.IncrementGetListOf(ifType);
            try
            {
                byte[] bytes = null;

                await MakeRequest(async () =>
                {
                    bytes = await _service.GetListOf(ZetboxGeneratedVersionAttribute.Current, _ifType, ID, property);
                });

                IEnumerable<IDataObject> result = null;
                using (var sr = _readerFactory.Invoke(new BinaryReader(new MemoryStream(bytes))))
                {
                    result = ReceiveObjects(sr, out tmpAuxObjects).Cast<IDataObject>();
                }
                resultCount = result.Count();

                return Tuple.Create(result, tmpAuxObjects);
            }
            finally
            {
                _perfCounter.DecrementGetListOf(ifType, resultCount, ticks);
            }
        }

        public async Task<IEnumerable<IPersistenceObject>> SetObjects(IEnumerable<IPersistenceObject> objects, IEnumerable<ObjectNotificationRequest> notficationRequests)
        {
            var ticks = _perfCounter.IncrementSetObjects();
            try
            {
                IEnumerable<IPersistenceObject> result = null;
                // Serialize
                using (var ms = new MemoryStream())
                using (var sw = _writerFactory.Invoke(new BinaryWriter(ms)))
                {
                    SendObjects(objects, sw);
                    byte[] bytes = null;
                    var _ms = ms.ToArray();
                    var _nReq = notficationRequests.ToArray();

                    await MakeRequest(async () =>
                    {
                        bytes = await _service.SetObjects(ZetboxGeneratedVersionAttribute.Current, _ms, _nReq);
                    }, retry: false);

                    using (var sr = _readerFactory.Invoke(new BinaryReader(new MemoryStream(bytes))))
                    {
                        // merge auxiliary objects into primary set objects result
                        List<IStreamable> auxObjects;
                        var receivedObjects = ReceiveObjects(sr, out auxObjects);
                        result = receivedObjects.Concat(auxObjects).Cast<IPersistenceObject>();
                    }
                }

                return result;
            }
            finally
            {
                _perfCounter.DecrementSetObjects(objects.Count(), ticks);
            }
        }



        public async Task<Tuple<IEnumerable<T>, List<IStreamable>>> FetchRelation<T>(Guid relationId, RelationEndRole role, int parentId, InterfaceType parentIfType)
            where T : class, IRelationEntry
        {
            var ifType = parentIfType;
            var ticks = _perfCounter.IncrementFetchRelation(ifType);
            int resultCount = 0;
            try
            {
                // TODO: could be implemented in generated properties
                if (parentId <= Helper.INVALIDID)
                {
                    return Tuple.Create(Enumerable.Empty<T>(), new List<IStreamable>());
                }

                IEnumerable<T> result = null;
                List<IStreamable> tmpAuxObjects = null;
                byte[] bytes = null;

                await MakeRequest(async () =>
                {
                    bytes = await _service.FetchRelation(ZetboxGeneratedVersionAttribute.Current, relationId, (int)role, parentId);
                });
                using (MemoryStream s = new MemoryStream(bytes))
                using (var sr = _readerFactory.Invoke(new BinaryReader(s)))
                {
                    result = ReceiveObjects(sr, out tmpAuxObjects).Cast<T>();
                }
                resultCount = result.Count();

                return Tuple.Create(result, tmpAuxObjects);
            }
            finally
            {
                _perfCounter.DecrementFetchRelation(ifType, resultCount, ticks);
            }
        }

        public async Task<Stream> GetBlobStream(int ID)
        {
            Stream result = null;
            await MakeRequest(async () =>
            {
                result = await _service.GetBlobStream(ZetboxGeneratedVersionAttribute.Current, ID);
            });
            return result;
        }

        public async Task<Zetbox.App.Base.Blob> SetBlobStream(Stream stream, string filename, string mimetype)
        {
            Zetbox.App.Base.Blob result = null;
            BlobResponse response = null;
            BlobMessage msg = new BlobMessage() { Version = ZetboxGeneratedVersionAttribute.Current, FileName = filename, MimeType = mimetype, Stream = stream };

            await MakeRequest(async () =>
            {
                // Rewind stream to ensure complete files, e.g. after a fault
                if (msg.Stream.Position != 0)
                    msg.Stream.Seek(0, SeekOrigin.Begin);
                response = await _service.SetBlobStream(msg);
            }, retry: false);

            using (var sr = _readerFactory.Invoke(new BinaryReader(response.BlobInstance)))
            {
                // ignore auxObjects for blobs, which should not have them
                result = ReceiveObjectList(sr).Cast<Zetbox.App.Base.Blob>().Single();
            }
            return result;
        }

        public async Task<Tuple<object, IEnumerable<IPersistenceObject>, List<IStreamable>>> InvokeServerMethod(InterfaceType ifType, int ID, string method, Type retValType, IEnumerable<Type> parameterTypes, IEnumerable<object> parameter, IEnumerable<IPersistenceObject> objects, IEnumerable<ObjectNotificationRequest> notificationRequests)
        {
            _perfCounter.IncrementServerMethodInvocation();

            object result = null;
            List<IStreamable> auxObjects = null;

            byte[] retChangedObjectsArray = null;
            byte[] bytes = null;

            using (var parameterStream = new MemoryStream())
            using (var parameterWriter = _writerFactory.Invoke(new BinaryWriter(parameterStream)))
            {
                foreach (var paramVal in parameter)
                {
                    parameterWriter.Write(paramVal);
                }

                using (var changedObjectsStream = new MemoryStream())
                using (var sw = _writerFactory.Invoke(new BinaryWriter(changedObjectsStream)))
                {
                    SendObjects(objects, sw);

                    var _ifType = ifType.ToSerializableType();
                    var _parameterTypes = parameterTypes.Select(t => _iftFactory(t).ToSerializableType()).ToArray();
                    var _parameterStream = parameterStream.ToArray();
                    var _changedObjStream = changedObjectsStream.ToArray();
                    var _nReq = notificationRequests.ToArray();

                    await MakeRequest(async () =>
                    {
                        var result = await _service.InvokeServerMethod(
                             ZetboxGeneratedVersionAttribute.Current,
                             _ifType,
                             ID,
                             method,
                             _parameterTypes,
                             _parameterStream,
                             _changedObjStream,
                             _nReq);
                        bytes = result.Item1;
                        retChangedObjectsArray = result.Item2;
                    }, retry: false);
                }
            }

            IEnumerable<IPersistenceObject> tmpChangedObjects = null;

            using (var resultStream = new MemoryStream(bytes))
            {
                using (var retChangedObjects = new MemoryStream(retChangedObjectsArray))
                using (var br = _readerFactory.Invoke(new BinaryReader(retChangedObjects)))
                {
                    tmpChangedObjects = ReceiveObjectList(br).Cast<IPersistenceObject>();
                }

                resultStream.Seek(0, SeekOrigin.Begin);

                if (retValType.IsIStreamable())
                {
                    using (var br = _readerFactory.Invoke(new BinaryReader(resultStream)))
                    {
                        result = ReceiveObjects(br, out auxObjects).Cast<IPersistenceObject>().FirstOrDefault();
                    }
                }
                else if (retValType.IsIEnumerable() && retValType.FindElementTypes().Any(t => t.IsIPersistenceObject()))
                {
                    using (var br = _readerFactory.Invoke(new BinaryReader(resultStream)))
                    {
                        IList lst = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(retValType.FindElementTypes().Single(t => t.IsIPersistenceObject())));
                        foreach (object resultObj in ReceiveObjects(br, out auxObjects))
                        {
                            lst.Add(resultObj);
                        }
                        result = lst;
                    }
                }
                else if (resultStream.Length > 0)
                {
                    // result = new BinaryFormatter().Deserialize(resultStream);
                    throw new NotSupportedException("Returning a generic result form a sever side method call is not supported.");
                }
                else
                {
                    result = null;
                }

                return Tuple.Create(result, tmpChangedObjects, auxObjects);
            }
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
            sw.Flush();
        }

        private IEnumerable<IStreamable> ReceiveObjects(ZetboxStreamReader sr, out List<IStreamable> auxObjects)
        {
            var result = ReceiveObjectList(sr);
            auxObjects = ReceiveObjectList(sr);
            Logging.Facade.DebugFormat("retrieved: {0} objects; {1} auxObjects", result.Count, auxObjects.Count);
            return result;
        }

        private List<IStreamable> ReceiveObjectList(ZetboxStreamReader sr)
        {
            List<IStreamable> result = new List<IStreamable>();
            bool cont = sr.ReadBoolean();
            long dbgByteCounter = 0;
            long dbgObjTypeByteCounter = 0;
            while (cont)
            {
                long dbgCurrentPos = sr.BaseStream.Position;
                var objType = sr.ReadSerializableType();
                dbgObjTypeByteCounter += sr.BaseStream.Position - dbgCurrentPos;

                IStreamable obj = (IStreamable)_unattachedObjectFactory(_iftFactory(objType.GetSystemType()));
                obj.FromStream(sr);
                result.Add((IStreamable)obj);

                cont = sr.ReadBoolean();
                long dbgSize = sr.BaseStream.Position - dbgCurrentPos;
                dbgByteCounter += dbgSize;
            }
            Logging.Facade.DebugFormat("ReceiveObjectList: {0:n0} objects; {1:n0} bytes total size; {2:n0} bytes avg. size / object; Total ObjType size: {3:n0}; {4:n2}% of total", result.Count, dbgByteCounter, (double)dbgByteCounter / (double)result.Count, dbgObjTypeByteCounter, (double)dbgObjTypeByteCounter / (double)dbgByteCounter * 100.0);
            return result;
        }

        private async Task MakeRequest(Func<Task> request, bool retry = true)
        {
            Exception fault = null;
            int retryCount = retry ? MAX_RETRY_COUNT : 1;
            for (int i = 0; i < retryCount; i++)
            {
                fault = null;
                try
                {
                    await request();
                    break;
                }
                catch (ConcurrencyException)
                {
                    throw;
                }
                catch (ZetboxValidationException)
                {
                    throw;
                }
                catch (FKViolationException)
                {
                    throw;
                }
                catch (UniqueConstraintViolationException)
                {
                    throw;
                }
                catch (InvalidZetboxGeneratedVersionException)
                {
                    throw;
                }
                catch (AuthenticationException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    // Retry
                    fault = ex;
                }
            }

            if (fault != null) throw new ZetboxServerIOException("Error when accessing server: " + fault.Message, fault);
        }
    }
}
