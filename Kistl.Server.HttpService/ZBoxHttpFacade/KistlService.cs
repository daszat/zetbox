
namespace Kistl.Server.HttpService
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Web;
    using Autofac;
    using Autofac.Integration.Web;
    using Kistl.API;
    using Kistl.API.Common;

    /// <summary>
    /// trivial HTTP-based facade implementation of the IKistlService for hosting in non-WCF environments (like apache+mono)
    /// </summary>
    public sealed class KistlServiceFacade : IHttpHandler
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server.Service.KistlServiceFacade");
        private readonly static byte[] Empty = new byte[0];

        public bool IsReusable
        {
            get { return false; }
        }

        private void SerializeArray<T>(Stream reqStream, T[] array)
        {
            var haveArray = array != null && array.Length > 0;
            new BinaryFormatter().Serialize(reqStream, haveArray);
            if (haveArray)
                new BinaryFormatter().Serialize(reqStream, array);
        }

        private T[] DeserializeArray<T>(Stream reqStream)
        {
            return (bool)new BinaryFormatter().Deserialize(reqStream)
                ? (T[])new BinaryFormatter().Deserialize(reqStream)
                : new T[0];
        }

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var cpa = (IContainerProviderAccessor)HttpContext.Current.ApplicationInstance;
                var scope = cpa.ContainerProvider.RequestLifetime;
                var service = scope.Resolve<IKistlService>();

                Log.DebugFormat("Processing {0}-request for [url={1}], as [user={2}]",
                    context.Request.HttpMethod,
                    context.Request.Url,
                    scope.Resolve<IIdentityResolver>().GetCurrent().DisplayName);
                switch (context.Request.Url.Segments.Last())
                {
                    case "SetObjects": // byte[] SetObjects(byte[] msg, ObjectNotificationRequest[] notificationRequests);
                        {
                            var msg = DeserializeArray<byte>(context.Request.InputStream);
                            var notificationRequests = DeserializeArray<ObjectNotificationRequest>(context.Request.InputStream);
                            Log.DebugFormat("SetObjects(byte[{0}], ObjectNotificationRequest[{1}])", msg.Length, notificationRequests.Length);
                            var result = service.SetObjects(msg, notificationRequests);
                            SendByteArray(context, result);
                            break;
                        }
                    case "GetList": // byte[] GetList(SerializableType type, int maxListCount, bool eagerLoadLists, SerializableExpression[] filter, OrderByContract[] orderBy);
                        {
                            var type = (SerializableType)new BinaryFormatter().Deserialize(context.Request.InputStream);
                            var maxListCount = (int)new BinaryFormatter().Deserialize(context.Request.InputStream);
                            var eagerLoadLists = (bool)new BinaryFormatter().Deserialize(context.Request.InputStream);
                            var filter = DeserializeArray<SerializableExpression>(context.Request.InputStream);
                            var orderBy = DeserializeArray<OrderByContract>(context.Request.InputStream);
                            Log.DebugFormat("GetList(type=[{0}], maxListCount={1}, eagerLoadLists={2}, SerializableExpression[{3}], OrderByContract[{4}])", type, maxListCount, eagerLoadLists, filter != null ? filter.Length : -1, orderBy != null ? orderBy.Length : -1);
                            var result = service.GetList(type, maxListCount, eagerLoadLists, filter, orderBy);
                            SendByteArray(context, result);
                            break;
                        }
                    case "GetListOf": // byte[] GetListOf(SerializableType type, int ID, string property);
                        {
                            var type = (SerializableType)new BinaryFormatter().Deserialize(context.Request.InputStream);
                            var ID = (int)new BinaryFormatter().Deserialize(context.Request.InputStream);
                            var property = (string)new BinaryFormatter().Deserialize(context.Request.InputStream);
                            Log.DebugFormat("GetListOf(type=[{0}], ID={1}, property=[{2}])", type, ID, property);
                            var result = service.GetListOf(type, ID, property);
                            SendByteArray(context, result);
                            break;
                        }
                    case "FetchRelation": // byte[] FetchRelation(Guid relId, int role, int ID)
                        {
                            var relId = (Guid)new BinaryFormatter().Deserialize(context.Request.InputStream);
                            var role = (int)new BinaryFormatter().Deserialize(context.Request.InputStream);
                            var ID = (int)new BinaryFormatter().Deserialize(context.Request.InputStream);
                            Log.DebugFormat("FetchRelation(relId=[{0}], role={1}, ID=[{2}])", relId, role, ID);
                            var result = service.FetchRelation(relId, role, ID);
                            SendByteArray(context, result);
                            break;
                        }
                    case "GetBlobStream": // Stream GetBlobStream(int ID)
                        {
                            var ID = (int)new BinaryFormatter().Deserialize(context.Request.InputStream);
                            Log.DebugFormat("GetBlobStream(ID={0})", ID);
                            var result = service.GetBlobStream(ID);
                            context.Response.StatusCode = 200;
                            context.Response.ContentType = "application/octet-stream";
                            result.CopyTo(context.Response.OutputStream);
                            break;
                        }
                    case "SetBlobStream": // BlobResponse SetBlobStream(BlobMessage blob)
                        {
                            var fileName = (string)new BinaryFormatter().Deserialize(context.Request.InputStream);
                            var mimeType = (string)new BinaryFormatter().Deserialize(context.Request.InputStream);
                            Log.DebugFormat("SetBlobStream(fileName=[{0}], mimeType=[{1}], Stream)", fileName, mimeType);
                            var result = service.SetBlobStream(new BlobMessage() { FileName = fileName, MimeType = mimeType, Stream = context.Request.InputStream });
                            context.Response.StatusCode = 200;
                            context.Response.ContentType = "application/octet-stream";
                            new BinaryFormatter().Serialize(context.Response.OutputStream, result.ID);
                            result.BlobInstance.CopyTo(context.Response.OutputStream);
                            break;
                        }
                    case "InvokeServerMethod": // byte[] InvokeServerMethod(SerializableType type, int ID, string method, SerializableType[] parameterTypes, byte[] parameter, byte[] changedObjects, ObjectNotificationRequest[] notificationRequests, out byte[] retChangedObjects)
                        {
                            var type = (SerializableType)new BinaryFormatter().Deserialize(context.Request.InputStream);
                            var ID = (int)new BinaryFormatter().Deserialize(context.Request.InputStream);
                            var method = (string)new BinaryFormatter().Deserialize(context.Request.InputStream);
                            var parameterTypes = DeserializeArray<SerializableType>(context.Request.InputStream);
                            var parameter = DeserializeArray<byte>(context.Request.InputStream);
                            var changedObjects = DeserializeArray<byte>(context.Request.InputStream);
                            var notificationRequests = DeserializeArray<ObjectNotificationRequest>(context.Request.InputStream);

                            Log.DebugFormat("InvokeServerMethod(type=[{0}], ID={1}, method=[{2}], SerializableType[{3}], byte[{4}], byte[{5}], ObjectNotificationRequest[{6}])", type, ID, method, parameterTypes.Length, parameter.Length, changedObjects.Length);
                            byte[] retChangedObjects;
                            var result = service.InvokeServerMethod(type, ID, method, parameterTypes, parameter, changedObjects, notificationRequests, out retChangedObjects);

                            SendByteArray(context, retChangedObjects);
                            SerializeArray(context.Response.OutputStream, result);

                            break;
                        }
                    default:
                        {
                            context.Response.StatusCode = 404;
                            context.Response.ContentType = "text/plain";
                            using (var outStream = new StreamWriter(context.Response.OutputStream))
                            {
                                Log.WarnFormat("Unknown operation: {0}", context.Request.Url);
                                outStream.WriteLine("Unknown operation, more details available in the server log");
                            }
                            break;
                        }
                }
                Log.DebugFormat("Sending response [{0}]", context.Response.StatusCode);
            }
            catch (Exception ex)
            {
                Log.Error("Error while processing request", ex);
            }
        }

        private void SendByteArray(HttpContext context, byte[] result)
        {
            context.Response.StatusCode = 200;
            context.Response.ContentType = "application/octet-stream";
            SerializeArray(context.Response.OutputStream, result);
        }
    }
}