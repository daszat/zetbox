
namespace Kistl.API.Client
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;

    public sealed class HttpServiceClient
        : Kistl.API.Client.KistlService.IKistlService
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.API.Client.HttpServiceClient");
        private readonly static byte[] Empty = new byte[0];

        private readonly BinaryFormatter _formatter = new BinaryFormatter();

        private readonly ICredentialsResolver _credentialsResolver;

        private readonly Uri SetObjectsUri;
        private readonly Uri GetListUri;
        private readonly Uri GetListOfUri;
        private readonly Uri FetchRelationUri;
        private readonly Uri GetBlobStreamUri;
        private readonly Uri SetBlobStreamUri;
        private readonly Uri InvokeServerMethodUri;

        public HttpServiceClient(ICredentialsResolver credentialsResolver)
        {
            _credentialsResolver = credentialsResolver;

            SetObjectsUri = new Uri(ConfigurationManager.AppSettings["serviceUri"] + "/SetObjects");
            GetListUri = new Uri(ConfigurationManager.AppSettings["serviceUri"] + "/GetList");
            GetListOfUri = new Uri(ConfigurationManager.AppSettings["serviceUri"] + "/GetListOf");
            FetchRelationUri = new Uri(ConfigurationManager.AppSettings["serviceUri"] + "/FetchRelation");
            GetBlobStreamUri = new Uri(ConfigurationManager.AppSettings["serviceUri"] + "/GetBlobStream");
            SetBlobStreamUri = new Uri(ConfigurationManager.AppSettings["serviceUri"] + "/SetBlobStream");
            InvokeServerMethodUri = new Uri(ConfigurationManager.AppSettings["serviceUri"] + "/InvokeServerMethod");
        }

        private byte[] MakeRequest(Uri destination, Action<Stream> sendRequest)
        {
            var req = InitializeRequest(destination);

            using (var reqStream = req.GetRequestStream())
            {
                sendRequest(reqStream);
                reqStream.Flush();
            }
            try
            {
                using (var response = req.GetResponse())
                using (var input = response.GetResponseStream())
                {
                    return (bool)_formatter.Deserialize(input)
                        ? (byte[])_formatter.Deserialize(input)
                        : Empty;
                }
            }
            catch (WebException ex)
            {
                var errorMsg = String.Format("Error when accessing server({0}): {1}", destination, ex.Status);
                Log.Error(errorMsg);
                var httpResponse = ex.Response as HttpWebResponse;
                if (httpResponse != null)
                {
                    Log.ErrorFormat("HTTP Error: {0}: {1}", httpResponse.StatusCode, httpResponse.StatusDescription);
                    foreach (var header in ex.Response.Headers)
                    {
                        Log.Error(header.ToString());
                    }
                }
                else
                {
                    Log.Error("No headers");
                }
                throw new ApplicationException(errorMsg, ex);
            }
        }

        private WebRequest InitializeRequest(Uri destination)
        {
            var req = WebRequest.Create(destination);

            req.Method = "POST";
            req.ContentType = "application/octet-stream";
            req.PreAuthenticate = true;
            var httpWebRequest = req as HttpWebRequest;
            if (httpWebRequest != null)
            {
                //httpWebRequest.SendChunked = true;
                httpWebRequest.Pipelined = false;
            }

#if DEBUG
            req.Credentials = new NetworkCredential("david", "plok");
#else
            _credentialsResolver.InitWebRequest(req);
#endif
            return req;
        }

        private void SerializeArray<T>(Stream reqStream, T[] array)
        {
            using (var ms = new MemoryStream())
            {
                var haveArray = array != null && array.Length > 0;
                _formatter.Serialize(ms, haveArray);
                if (haveArray)
                    _formatter.Serialize(ms, array);

                ms.Seek(0, SeekOrigin.Begin);
                ms.CopyTo(reqStream);
                reqStream.Flush();
            }
        }

        public byte[] SetObjects(byte[] msg, ObjectNotificationRequest[] notificationRequests)
        {
            return MakeRequest(SetObjectsUri,
                reqStream =>
                {
                    SerializeArray(reqStream, msg);
                    SerializeArray(reqStream, notificationRequests);
                });
        }

        public byte[] GetList(SerializableType type, int maxListCount, bool eagerLoadLists, SerializableExpression[] filter, OrderByContract[] orderBy)
        {
            if (type == null) throw new ArgumentNullException("type");

            return MakeRequest(GetListUri,
                reqStream =>
                {
                    var writer = new BinaryWriter(reqStream);
                    type.ToStream(writer);
                    writer.Write(0xdeadbeefL);
                    writer.Flush();
                    //_formatter.Serialize(reqStream, type);
                    _formatter.Serialize(reqStream, maxListCount);
                    _formatter.Serialize(reqStream, eagerLoadLists);
                    SerializeArray(reqStream, filter);
                    SerializeArray(reqStream, orderBy);
                });
        }

        public byte[] GetListOf(SerializableType type, int ID, string property)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (String.IsNullOrEmpty(property)) throw new ArgumentNullException("property");

            return MakeRequest(GetListOfUri,
                reqStream =>
                {
                    _formatter.Serialize(reqStream, type);
                    _formatter.Serialize(reqStream, ID);
                    _formatter.Serialize(reqStream, property);
                });
        }

        public byte[] FetchRelation(Guid relId, int role, int ID)
        {
            return MakeRequest(FetchRelationUri,
                reqStream =>
                {
                    _formatter.Serialize(reqStream, relId);
                    _formatter.Serialize(reqStream, role);
                    _formatter.Serialize(reqStream, ID);
                });
        }

        public Stream GetBlobStream(int ID)
        {
            var req = InitializeRequest(GetBlobStreamUri);
            using (var reqStream = req.GetRequestStream())
            {
                _formatter.Serialize(reqStream, ID);
            }
            try
            {
                using (var response = req.GetResponse())
                {
                    return response.GetResponseStream();
                }
            }
            catch (WebException ex)
            {
                var errorMsg = String.Format("Error when accessing server({0}): {1}: {2}", GetBlobStreamUri, ex.Status, ex.Response);
                Log.Error(errorMsg);
                throw new ApplicationException(errorMsg, ex);
            }
        }

        public KistlService.BlobResponse SetBlobStream(KistlService.BlobMessage request)
        {
            if (request == null) throw new ArgumentNullException("request");
            
            var req = InitializeRequest(SetBlobStreamUri);
            using (var reqStream = req.GetRequestStream())
            {
                _formatter.Serialize(reqStream, request.FileName);
                _formatter.Serialize(reqStream, request.MimeType);
                request.Stream.CopyTo(reqStream);
            }
            try
            {
                using (var response = req.GetResponse())
                {
                    var input = response.GetResponseStream();
                    return new KistlService.BlobResponse()
                    {
                        ID = (int)_formatter.Deserialize(input),
                        BlobInstance = input
                    };
                }
            }
            catch (WebException ex)
            {
                var errorMsg = String.Format("Error when accessing server({0}): {1}: {2}", SetBlobStreamUri, ex.Status, ex.Response);
                Log.Error(errorMsg);
                throw new ApplicationException(errorMsg, ex);
            }
        }

        public byte[] InvokeServerMethod(out byte[] retChangedObjects, SerializableType type, int ID, string method, SerializableType[] parameterTypes, byte[] parameter, byte[] changedObjects, ObjectNotificationRequest[] notificationRequests)
        {
            if (type == null) throw new ArgumentNullException("type");

            var req = InitializeRequest(InvokeServerMethodUri);
            using (var reqStream = req.GetRequestStream())
            {
                _formatter.Serialize(reqStream, type);
                _formatter.Serialize(reqStream, ID);
                _formatter.Serialize(reqStream, method);
                SerializeArray(reqStream, parameterTypes);
                SerializeArray(reqStream, parameter);
                SerializeArray(reqStream, changedObjects);
                SerializeArray(reqStream, notificationRequests);
            }
            try
            {
                using (var response = req.GetResponse())
                using (var input = response.GetResponseStream())
                {
                    retChangedObjects = (bool)_formatter.Deserialize(input)
                        ? (byte[])_formatter.Deserialize(input)
                        : retChangedObjects = Empty;

                    return (bool)_formatter.Deserialize(input)
                        ? (byte[])_formatter.Deserialize(input)
                        : Empty;
                }
            }
            catch (WebException ex)
            {
                var errorMsg = String.Format("Error when accessing server({0}): {1}: {2}", InvokeServerMethodUri, ex.Status, ex.Response);
                Log.Error(errorMsg);
                throw new ApplicationException(errorMsg, ex);
            }
        }
    }
}