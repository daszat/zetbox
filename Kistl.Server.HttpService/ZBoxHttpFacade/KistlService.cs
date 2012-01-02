
namespace Kistl.Server.HttpService
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.ServiceModel;
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

        private readonly BinaryFormatter _formatter = new BinaryFormatter();

        public bool IsReusable
        {
            get { return false; }
        }

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

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var cpa = (IContainerProviderAccessor)HttpContext.Current.ApplicationInstance;
                var scope = cpa.ContainerProvider.RequestLifetime;
                var service = scope.Resolve<IKistlService>();
                string username;
                try
                {
                    username = scope.Resolve<IIdentityResolver>().GetCurrent().DisplayName;
                }
                catch (Exception ex)
                {
                    Log.Error("Error while trying to resolve user", ex);
                    username = "(unknown)";
                }
                Log.DebugFormat("Processing {0}-request for [url={1}], as [user={2}]",
                    context.Request.HttpMethod,
                    context.Request.Url,
                    username);
                var reader = new BinaryReader(context.Request.InputStream);
                Guid version;
                BinarySerializer.FromStream(out version, reader);

                switch (context.Request.Url.Segments.Last())
                {
                    case "SetObjects": // byte[] SetObjects(byte[] msg, ObjectNotificationRequest[] notificationRequests);
                        {
                            byte[] msg;
                            BinarySerializer.FromStream(out msg, reader);
                            ObjectNotificationRequest[] notificationRequests;
                            BinarySerializer.FromStream(out notificationRequests, reader);
                            Log.DebugFormat("SetObjects(byte[{0}], ObjectNotificationRequest[{1}])", msg.Length, notificationRequests.Length);
                            var result = service.SetObjects(version, msg, notificationRequests);
                            SendByteArray(context, result);
                            break;
                        }
                    case "GetList": // byte[] GetList(SerializableType type, int maxListCount, bool eagerLoadLists, SerializableExpression[] filter, OrderByContract[] orderBy);
                        {
                            SerializableType type;
                            BinarySerializer.FromStream(out type, reader);

                            int maxListCount;
                            BinarySerializer.FromStream(out maxListCount, reader);

                            bool eagerLoadLists;
                            BinarySerializer.FromStream(out eagerLoadLists, reader);

                            var iftFactory = scope.Resolve<InterfaceType.Factory>();

                            SerializableExpression[] filter;
                            BinarySerializer.FromStream(out filter, reader, iftFactory);

                            OrderByContract[] orderBy;
                            BinarySerializer.FromStream(out orderBy, reader, iftFactory);

                            Log.DebugFormat("GetList(type=[{0}], maxListCount={1}, eagerLoadLists={2}, SerializableExpression[{3}], OrderByContract[{4}])", type, maxListCount, eagerLoadLists, filter != null ? filter.Length : -1, orderBy != null ? orderBy.Length : -1);
                            var result = service.GetList(version, type, maxListCount, eagerLoadLists, filter, orderBy);
                            SendByteArray(context, result);
                            break;
                        }
                    case "GetListOf": // byte[] GetListOf(SerializableType type, int ID, string property);
                        {
                            SerializableType type;
                            BinarySerializer.FromStream(out type, reader);

                            int ID;
                            BinarySerializer.FromStream(out ID, reader);

                            string property;
                            BinarySerializer.FromStream(out property, reader);

                            Log.DebugFormat("GetListOf(type=[{0}], ID={1}, property=[{2}])", type, ID, property);
                            var result = service.GetListOf(version, type, ID, property);
                            SendByteArray(context, result);
                            break;
                        }
                    case "FetchRelation": // byte[] FetchRelation(Guid relId, int role, int ID)
                        {
                            Guid relId;
                            BinarySerializer.FromStream(out relId, reader);

                            int role;
                            BinarySerializer.FromStream(out role, reader);

                            int ID;
                            BinarySerializer.FromStream(out ID, reader);

                            Log.DebugFormat("FetchRelation(relId=[{0}], role={1}, ID=[{2}])", relId, role, ID);
                            var result = service.FetchRelation(version, relId, role, ID);
                            SendByteArray(context, result);
                            break;
                        }
                    case "GetBlobStream": // Stream GetBlobStream(int ID)
                        {
                            var ID = Int32.Parse(context.Request.QueryString["id"]);

                            Log.DebugFormat("GetBlobStream(ID={0})", ID);
                            var result = service.GetBlobStream(version, ID);
                            context.Response.StatusCode = 200;
                            context.Response.ContentType = "application/octet-stream";
                            result.CopyTo(context.Response.OutputStream);
                            break;
                        }
                    case "SetBlobStream": // BlobResponse SetBlobStream(BlobMessage blob)
                        {
                            string fileName;
                            BinarySerializer.FromStream(out fileName, reader);

                            string mimeType;
                            BinarySerializer.FromStream(out mimeType, reader);

                            byte[] data;
                            BinarySerializer.FromStream(out data, reader);

                            Log.DebugFormat("SetBlobStream(fileName=[{0}], mimeType=[{1}], Stream of {2} bytes)", fileName, mimeType, data.Length);
                            var result = service.SetBlobStream(new BlobMessage()
                            {
                                Version = version,
                                FileName = fileName,
                                MimeType = mimeType,
                                Stream = new MemoryStream(data)
                            });

                            context.Response.StatusCode = 200;
                            context.Response.ContentType = "application/octet-stream";
                            using (var writer = new BinaryWriter(context.Response.OutputStream))
                            using (var dataStream = new MemoryStream())
                            {
                                BinarySerializer.ToStream(result.ID, writer);
                                result.BlobInstance.CopyTo(dataStream);
                                var bytes = dataStream.ToArray();
                                BinarySerializer.ToStream(bytes, writer);
                            }
                            break;
                        }
                    case "InvokeServerMethod": // byte[] InvokeServerMethod(SerializableType type, int ID, string method, SerializableType[] parameterTypes, byte[] parameter, byte[] changedObjects, ObjectNotificationRequest[] notificationRequests, out byte[] retChangedObjects)
                        {
                            SerializableType type;
                            BinarySerializer.FromStream(out type, reader);

                            int ID;
                            BinarySerializer.FromStream(out ID, reader);

                            string method;
                            BinarySerializer.FromStream(out method, reader);

                            SerializableType[] parameterTypes;
                            BinarySerializer.FromStream(out parameterTypes, reader);

                            byte[] parameter;
                            BinarySerializer.FromStream(out parameter, reader);

                            byte[] changedObjects;
                            BinarySerializer.FromStream(out changedObjects, reader);

                            ObjectNotificationRequest[] notificationRequests;
                            BinarySerializer.FromStream(out notificationRequests, reader);

                            Log.DebugFormat("InvokeServerMethod(type=[{0}], ID={1}, method=[{2}], SerializableType[{3}], byte[{4}], byte[{5}], ObjectNotificationRequest[{6}])",
                                type,
                                ID,
                                method,
                                parameterTypes.Length,
                                parameter.Length,
                                changedObjects.Length,
                                notificationRequests.Length);
                            byte[] retChangedObjects;
                            var result = service.InvokeServerMethod(version, type, ID, method, parameterTypes, parameter, changedObjects, notificationRequests, out retChangedObjects);
                            Log.DebugFormat("InvokeServerMethod received {0}B retChangedObjects", retChangedObjects.Length);

                            context.Response.StatusCode = 200;
                            context.Response.ContentType = "application/octet-stream";
                            using (var writer = new BinaryWriter(context.Response.OutputStream))
                            {
                                BinarySerializer.ToStream(retChangedObjects, writer);
                                BinarySerializer.ToStream(result, writer);
                            }
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
            catch (FaultException<ConcurrencyException> cex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                Log.Info("Concurrency error while processing request", cex);
            }
            catch (FaultException<InvalidKistlGeneratedVersionException> vex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.PreconditionFailed;
                Log.Info("InvalidKistlGeneratedVersion error while processing request", vex);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                Log.Error("Error while processing request", ex);
            }
        }

        private void SendByteArray(HttpContext context, byte[] result)
        {
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.ContentType = "application/octet-stream";
            using (var writer = new BinaryWriter(context.Response.OutputStream))
            {
                BinarySerializer.ToStream(result, writer);
            }
        }
    }
}