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

namespace Zetbox.Server.HttpService
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
    using Zetbox.API;
    using Zetbox.API.Common;

    /// <summary>
    /// trivial HTTP-based facade implementation of the IZetboxService for hosting in non-WCF environments (like apache+mono)
    /// </summary>
    public sealed class ZetboxServiceFacade : IHttpHandler
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Server.Service.ZetboxServiceFacade");

        private readonly BinaryFormatter _formatter = new BinaryFormatter();

        /// <summary>
        /// This class is instantiated by ASP.NET and thus needs an empty constructor.
        /// </summary>
        public ZetboxServiceFacade() { }

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
            IContainerProviderAccessor cpa = null;
            try
            {
                cpa = (IContainerProviderAccessor)HttpContext.Current.ApplicationInstance;
                var scope = cpa.ContainerProvider.RequestLifetime;
                var readerFactory = scope.Resolve<ZetboxStreamReader.Factory>();
                var writerFactory = scope.Resolve<ZetboxStreamWriter.Factory>();

                var service = scope.Resolve<IZetboxService>();
                string username;
                try
                {
                    var id = scope.Resolve<IIdentityResolver>().GetCurrent();
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
                Log.DebugFormat("Processing {0}-request for [url={1}], as [user={2}]",
                    context.Request.HttpMethod,
                    context.Request.Url,
                    username);
                var reader = readerFactory(new BinaryReader(context.Request.InputStream));

                var version = reader.ReadGuid();
                switch (context.Request.Url.Segments.Last())
                {
                    case "SetObjects": // byte[] SetObjects(byte[] msg, ObjectNotificationRequest[] notificationRequests);
                        {
                            byte[] msg = reader.ReadByteArray();
                            ObjectNotificationRequest[] notificationRequests;
                            reader.Read(out notificationRequests);
                            Log.DebugFormat("SetObjects(byte[{0}], ObjectNotificationRequest[{1}])", msg.Length, notificationRequests.Length);
                            var result = service.SetObjects(version, msg, notificationRequests);
                            SendByteArray(context, result, writerFactory);
                            break;
                        }
                    case "GetList": // byte[] GetList(SerializableType type, int maxListCount, bool eagerLoadLists, SerializableExpression[] filter, OrderByContract[] orderBy);
                        {
                            var type = reader.ReadSerializableType();
                            int maxListCount = reader.ReadInt32();
                            bool eagerLoadLists = reader.ReadBoolean();

                            var iftFactory = scope.Resolve<InterfaceType.Factory>();

                            SerializableExpression[] filter;
                            reader.Read(out filter, iftFactory);

                            OrderByContract[] orderBy;
                            reader.Read(out orderBy, iftFactory);

                            Log.DebugFormat("GetList(type=[{0}], maxListCount={1}, eagerLoadLists={2}, SerializableExpression[{3}], OrderByContract[{4}])", type, maxListCount, eagerLoadLists, filter != null ? filter.Length : -1, orderBy != null ? orderBy.Length : -1);
                            var result = service.GetList(version, type, maxListCount, eagerLoadLists, filter, orderBy);
                            SendByteArray(context, result, writerFactory);
                            break;
                        }
                    case "GetListOf": // byte[] GetListOf(SerializableType type, int ID, string property);
                        {
                            var type = reader.ReadSerializableType();
                            int ID = reader.ReadInt32();
                            string property = reader.ReadString();

                            Log.DebugFormat("GetListOf(type=[{0}], ID={1}, property=[{2}])", type, ID, property);
                            var result = service.GetListOf(version, type, ID, property);
                            SendByteArray(context, result, writerFactory);
                            break;
                        }
                    case "FetchRelation": // byte[] FetchRelation(Guid relId, int role, int ID)
                        {
                            Guid relId = reader.ReadGuid();
                            int role = reader.ReadInt32();
                            int ID = reader.ReadInt32();

                            Log.DebugFormat("FetchRelation(relId=[{0}], role={1}, ID=[{2}])", relId, role, ID);
                            var result = service.FetchRelation(version, relId, role, ID);
                            SendByteArray(context, result, writerFactory);
                            break;
                        }
                    case "GetBlobStream": // Stream GetBlobStream(int ID)
                        {
                            var ID = Int32.Parse(context.Request.QueryString["id"]);

                            Log.DebugFormat("GetBlobStream(ID={0})", ID);
                            var result = service.GetBlobStream(version, ID);
                            context.Response.StatusCode = 200;
                            context.Response.ContentType = "application/octet-stream";
                            result.CopyAllTo(context.Response.OutputStream);
                            break;
                        }
                    case "SetBlobStream": // BlobResponse SetBlobStream(BlobMessage blob)
                        {
                            string fileName = reader.ReadString();
                            string mimeType = reader.ReadString();
                            byte[] data = reader.ReadByteArray();

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
                            using (var writer = writerFactory(new BinaryWriter(context.Response.OutputStream)))
                            using (var dataStream = new MemoryStream())
                            {
                                writer.Write(result.ID);
                                result.BlobInstance.CopyAllTo(dataStream);
                                writer.Write(dataStream.ToArray());
                            }
                            break;
                        }
                    case "InvokeServerMethod": // byte[] InvokeServerMethod(SerializableType type, int ID, string method, SerializableType[] parameterTypes, byte[] parameter, byte[] changedObjects, ObjectNotificationRequest[] notificationRequests, out byte[] retChangedObjects)
                        {
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
                            byte[] retChangedObjects;
                            var result = service.InvokeServerMethod(version, type, ID, method, parameterTypes, parameter, changedObjects, notificationRequests, out retChangedObjects);
                            Log.DebugFormat("InvokeServerMethod received {0}B retChangedObjects", retChangedObjects.Length);

                            context.Response.StatusCode = 200;
                            context.Response.ContentType = "application/octet-stream";
                            using (var writer = writerFactory(new BinaryWriter(context.Response.OutputStream)))
                            {
                                writer.Write(retChangedObjects);
                                writer.Write(result);
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
            catch (FaultException<ConcurrencyException> ex)
            {
                SerializeException(context, ex.Detail);
                Log.Info("Concurrency error while processing request", ex);
            }
            catch (FaultException<FKViolationException> ex)
            {
                SerializeException(context, ex.Detail);
                Log.Info("FK violation error while processing request", ex);
            }
            catch (FaultException<UniqueConstraintViolationException> ex)
            {
                SerializeException(context, ex.Detail);
                Log.Info("unique constraint violation error while processing request", ex);
            }
            catch (FaultException<InvalidZetboxGeneratedVersionException> ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.PreconditionFailed;
                Log.Info("InvalidZetboxGeneratedVersion error while processing request", ex);
            }
            catch (FaultException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                // Already logged in ThrowFaultException, only push to Debug here
                Log.Debug("Error while processing request", ex);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                Log.Error("Error while processing request", ex);
            }
            finally
            {
                if (cpa != null && cpa.ContainerProvider != null)
                {
                    cpa.ContainerProvider.EndRequestLifetime();
                }
            }
        }

        private void SerializeException(HttpContext context, ZetboxContextException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Conflict;
            context.Response.ContentType = "text/xml";
            ex.ToXmlStream(context.Response.OutputStream);
            context.Response.OutputStream.Flush();            
        }

        private void SendByteArray(HttpContext context, byte[] result, ZetboxStreamWriter.Factory writerFactory)
        {
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.ContentType = "application/octet-stream";
            using (var writer = writerFactory(new BinaryWriter(context.Response.OutputStream)))
            {
                writer.Write(result);
                writer.Flush();
            }
        }
    }
}