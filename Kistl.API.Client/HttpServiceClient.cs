
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

        private byte[] MakeRequest(Uri destination, Action<BinaryWriter> sendRequest)
        {
            var req = InitializeRequest(destination);

            using (var reqStream = req.GetRequestStream())
            using (var reqWriter = new BinaryWriter(reqStream))
            {
                sendRequest(reqWriter);
            }
            try
            {
                using (var response = req.GetResponse())
                using (var input = new BinaryReader(response.GetResponseStream()))
                {
                    var length = input.ReadInt32();
                    var result = input.ReadBytes(length);
                    var canary = input.ReadInt64();
                    Log.ErrorFormat("Read canary: {0:X}", canary);
                    return result;
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
                // must not be set for apache/mono server!
                //httpWebRequest.SendChunked = true;
                httpWebRequest.Pipelined = false;
            }

#if DEBUG
            Log.Warn("Using debug credentials");
            req.Credentials = new NetworkCredential("david", "plok");
#else
            _credentialsResolver.InitWebRequest(req);
#endif
            return req;
        }

        public byte[] SetObjects(byte[] msg, ObjectNotificationRequest[] notificationRequests)
        {
            return MakeRequest(SetObjectsUri,
                reqStream =>
                {
                    BinarySerializer.ToStream(msg, reqStream);
                    BinarySerializer.ToStream(notificationRequests, reqStream);
                });
        }

        public byte[] GetList(SerializableType type, int maxListCount, bool eagerLoadLists, SerializableExpression[] filter, OrderByContract[] orderBy)
        {
            if (type == null) throw new ArgumentNullException("type");

            return MakeRequest(GetListUri,
                reqStream =>
                {
                    BinarySerializer.ToStream(type, reqStream);
                    BinarySerializer.ToStream(maxListCount, reqStream);
                    BinarySerializer.ToStream(eagerLoadLists, reqStream);
                    BinarySerializer.ToStream(filter, reqStream);
                    BinarySerializer.ToStream(orderBy, reqStream);
                });
        }

        public byte[] GetListOf(SerializableType type, int ID, string property)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (String.IsNullOrEmpty(property)) throw new ArgumentNullException("property");

            return MakeRequest(GetListOfUri,
                reqStream =>
                {
                    BinarySerializer.ToStream(type, reqStream);
                    BinarySerializer.ToStream(ID, reqStream);
                    BinarySerializer.ToStream(property, reqStream);
                });
        }

        public byte[] FetchRelation(Guid relId, int role, int ID)
        {
            return MakeRequest(FetchRelationUri,
                reqStream =>
                {
                    BinarySerializer.ToStream(relId, reqStream);
                    BinarySerializer.ToStream(role, reqStream);
                    BinarySerializer.ToStream(ID, reqStream);
                });
        }

        public Stream GetBlobStream(int ID)
        {
            var req = InitializeRequest(new Uri(String.Format("{0}?id={1}", GetBlobStreamUri.AbsoluteUri, ID)));
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
            using (var reqWriter = new BinaryWriter(reqStream))
            using (var upload = new MemoryStream())
            {
                BinarySerializer.ToStream(request.FileName, reqWriter);
                BinarySerializer.ToStream(request.MimeType, reqWriter);
                request.Stream.CopyTo(upload);
                var bytes = upload.ToArray();
                BinarySerializer.ToStream(bytes, reqWriter);
            }
            try
            {
                using (var response = req.GetResponse())
                using (var input = response.GetResponseStream())
                using (var reader = new BinaryReader(input))
                {
                    int id;
                    BinarySerializer.FromStream(out id, reader);

                    byte[] data;
                    BinarySerializer.FromStream(out data, reader);
                    return new KistlService.BlobResponse()
                    {
                        ID = id,
                        BlobInstance = new MemoryStream(data)
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
            using (var reqWriter = new BinaryWriter(reqStream))
            {
                BinarySerializer.ToStream(type, reqWriter);
                BinarySerializer.ToStream(ID, reqWriter);
                BinarySerializer.ToStream(method, reqWriter);
                BinarySerializer.ToStream(parameterTypes, reqWriter);
                BinarySerializer.ToStream(parameter, reqWriter);
                BinarySerializer.ToStream(changedObjects, reqWriter);
                BinarySerializer.ToStream(notificationRequests, reqWriter);
            }
            try
            {
                using (var response = req.GetResponse())
                using (var input = response.GetResponseStream())
                using (var reader = new BinaryReader(input))
                {
                    BinarySerializer.FromStream(out retChangedObjects, reader);
                    byte[] result;
                    BinarySerializer.FromStream(out result, reader);
                    return result;
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