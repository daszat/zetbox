using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zetbox.API;
using Zetbox.API.Common;

namespace Zetbox.Server.HttpService.Controllers
{
    public class ZetboxServiceController : Controller
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ZetboxServiceController));
        private readonly BinaryFormatter _formatter = new BinaryFormatter();
        private readonly ILifetimeScope scope;
        private readonly ZetboxStreamReader.Factory readerFactory;
        private readonly ZetboxStreamWriter.Factory writerFactory;
        private readonly IZetboxService service;

        public ZetboxServiceController(ILifetimeScope scope)
        {
            this.scope = scope;
            this.readerFactory = scope.Resolve<ZetboxStreamReader.Factory>();
            this.writerFactory = scope.Resolve<ZetboxStreamWriter.Factory>();

            this.service = scope.Resolve<IZetboxService>();
        }

        [HttpPost]
        public async Task SetObjects()
        {
            await CheckUser();
            using (var reader = readerFactory.Invoke(new BinaryReader(Request.Body)))
            {
                var version = reader.ReadGuid();

                byte[] msg = reader.ReadByteArray();
                ObjectNotificationRequest[] notificationRequests;
                reader.Read(out notificationRequests);
                Log.DebugFormat("SetObjects(byte[{0}], ObjectNotificationRequest[{1}])", msg.Length, notificationRequests.Length);
                var result = await service.SetObjects(version, msg, notificationRequests);

                SendByteArray(Response, result, writerFactory);
            }
        }
        [HttpPost]
        public async Task GetObjects()
        {
            await CheckUser();
            using (var reader = readerFactory.Invoke(new BinaryReader(Request.Body)))
            {
                var version = reader.ReadGuid();

                var iftFactory = scope.Resolve<InterfaceType.Factory>();

                SerializableExpression query;
                reader.Read(out query, iftFactory);

                Log.DebugFormat("GetObjects(query=[{0}])", query);
                var result = await service.GetObjects(version, query);

                SendByteArray(Response, result, writerFactory);
            }
        }
        [HttpPost]
        public async Task GetListOf()
        {
            await CheckUser();
            using (var reader = readerFactory.Invoke(new BinaryReader(Request.Body)))
            {
                var version = reader.ReadGuid();

                var type = reader.ReadSerializableType();
                int ID = reader.ReadInt32();
                string property = reader.ReadString();

                Log.DebugFormat("GetListOf(type=[{0}], ID={1}, property=[{2}])", type, ID, property);
                var result = await service.GetListOf(version, type, ID, property);

                SendByteArray(Response, result, writerFactory);
            }
        }
        [HttpPost]
        public async Task FetchRelation()
        {
            await CheckUser();
            using (var reader = readerFactory.Invoke(new BinaryReader(Request.Body)))
            {
                var version = reader.ReadGuid();

                Guid relId = reader.ReadGuid();
                int role = reader.ReadInt32();
                int ID = reader.ReadInt32();

                Log.DebugFormat("FetchRelation(relId=[{0}], role={1}, ID=[{2}])", relId, role, ID);
                var result = await service.FetchRelation(version, relId, role, ID);
                SendByteArray(Response, result, writerFactory);
            }
        }
        [HttpPost]
        public async Task GetBlobStream(int id)
        {
            await CheckUser();
            using (var reader = readerFactory.Invoke(new BinaryReader(Request.Body)))
            {
                var version = reader.ReadGuid();

                Log.DebugFormat("GetBlobStream(ID={0})", id);
                var result = await service.GetBlobStream(version, id);
                Response.StatusCode = 200;
                Response.ContentType = "application/octet-stream";
                result.CopyAllTo(Response.Body);
            }
        }
        [HttpPost]
        public async Task SetBlobStream()
        {
            await CheckUser();
            using (var reader = readerFactory.Invoke(new BinaryReader(Request.Body)))
            {
                var version = reader.ReadGuid();

                string fileName = reader.ReadString();
                string mimeType = reader.ReadString();
                byte[] data = reader.ReadByteArray();

                Log.DebugFormat("SetBlobStream(fileName=[{0}], mimeType=[{1}], Stream of {2} bytes)", fileName, mimeType, data.Length);
                var result = await service.SetBlobStream(new BlobMessage()
                {
                    Version = version,
                    FileName = fileName,
                    MimeType = mimeType,
                    Stream = new MemoryStream(data)
                });

                Response.StatusCode = 200;
                Response.ContentType = "application/octet-stream";
                using (var writer = writerFactory.Invoke(new BinaryWriter(Response.Body)))
                using (var dataStream = new MemoryStream())
                {
                    writer.Write(result.ID);
                    result.BlobInstance.CopyAllTo(dataStream);
                    writer.Write(dataStream.ToArray());
                }
            }
        }
        [HttpPost]
        public async Task InvokeServerMethod()
        {
            await CheckUser();
            using (var reader = readerFactory.Invoke(new BinaryReader(Request.Body)))
            {
                var version = reader.ReadGuid();

                var type = reader.ReadSerializableType();
                int ID = reader.ReadInt32();
                string method = reader.ReadString();

                SerializableType[] parameterTypes = reader.ReadSerializableTypeArray();
                byte[] parameter = reader.ReadByteArray();
                byte[] changedObjects = reader.ReadByteArray();

                ObjectNotificationRequest[] notificationRequests;
                reader.Read(out notificationRequests);

                Log.DebugFormat("InvokeServerMethod(type=[{0}], ID={1}, method=[{2}], SerializableType[{3}], byte[{4}], byte[{5}], ObjectNotificationRequest[{6}])",
                    type,
                    ID,
                    method,
                    parameterTypes.Length,
                    parameter.Length,
                    changedObjects.Length,
                    notificationRequests.Length);
                var response = await service.InvokeServerMethod(version, type, ID, method, parameterTypes, parameter, changedObjects, notificationRequests);
                var result = response.Item1;
                var retChangedObjects = response.Item2;

                Log.DebugFormat("InvokeServerMethod received {0}B retChangedObjects", retChangedObjects.Length);

                Response.StatusCode = 200;
                Response.ContentType = "application/octet-stream";
                using (var writer = writerFactory.Invoke(new BinaryWriter(Response.Body)))
                {
                    writer.Write(retChangedObjects);
                    writer.Write(result);
                }
            }
        }

        #region Helper
        private void SerializeArray<T>(Stream reqStream, T[] array)
        {
            var haveArray = array != null && array.Length > 0;
            _formatter.Serialize(reqStream, haveArray);
            if (haveArray)
                _formatter.Serialize(reqStream, array);
        }

        private void SerializeArray(Stream reqStream, byte[] array)
        {
            var haveArray = array != null && array.Length > 0;
            var binWriter = new BinaryWriter(reqStream);
            if (haveArray)
            {
                binWriter.Write(array.Length);
                binWriter.Write(array);
            }
            else
            {
                binWriter.Write(0);
            }
            // canary value
            binWriter.Write(0xaffeaffeL);
        }

        private T[] DeserializeArray<T>(Stream reqStream)
        {
            return (bool)_formatter.Deserialize(reqStream)
                ? (T[])_formatter.Deserialize(reqStream)
                : new T[0];
        }

        private void SerializeException(HttpResponse response, ZetboxContextExceptionMessage exContainer)
        {
            response.StatusCode = (int)HttpStatusCode.Conflict;
            response.ContentType = "text/xml";
            exContainer.ToXmlStream(response.Body);
            response.Body.Flush();
        }

        private void SendByteArray(HttpResponse response, byte[] result, ZetboxStreamWriter.Factory writerFactory)
        {
            response.StatusCode = (int)HttpStatusCode.OK;
            response.ContentType = "application/octet-stream";
            using (var writer = writerFactory.Invoke(new BinaryWriter(response.Body)))
            {
                writer.Write(result);
                writer.Flush();
            }
        }

        private async Task CheckUser()
        {
            string username;
            try
            {
                var id = await scope.Resolve<IPrincipalResolver>().GetCurrent();
                if (id != null)
                {
                    username = id.DisplayName;
                }
                else
                {
                    Log.Error("Error while trying to resolve user - not found");
                    username = "(unknown)";
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while trying to resolve user", ex);
                username = "(unknown)";
            }
        }
        #endregion
    }
}