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

namespace Zetbox.API.Client
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Security.Authentication;
    using System.Text;

    public sealed class HttpServiceClient
        : Zetbox.API.Client.ZetboxService.IZetboxService
    {
        private const int MAX_RETRY_COUNT = 2;

        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.API.Client.HttpServiceClient");

        private readonly Uri SetObjectsUri;
        private readonly Uri GetListUri;
        private readonly Uri GetListOfUri;
        private readonly Uri FetchRelationUri;
        private readonly Uri GetBlobStreamUri;
        private readonly Uri SetBlobStreamUri;
        private readonly Uri InvokeServerMethodUri;
        private readonly ICredentialsResolver _credentialsResolver;
        private readonly ZetboxStreamReader.Factory _readerFactory;
        private readonly ZetboxStreamWriter.Factory _writerFactory;

        public HttpServiceClient(ICredentialsResolver credentialsResolver, ZetboxStreamReader.Factory readerFactory, ZetboxStreamWriter.Factory writerFactory)
        {
            if (credentialsResolver == null) throw new ArgumentNullException("credentialsResolver");
            if (readerFactory == null) throw new ArgumentNullException("readerFactory");
            if (writerFactory == null) throw new ArgumentNullException("writerFactory");

            SetObjectsUri = new Uri(ConfigurationManager.AppSettings["serviceUri"] + "/SetObjects");
            GetListUri = new Uri(ConfigurationManager.AppSettings["serviceUri"] + "/GetList");
            GetListOfUri = new Uri(ConfigurationManager.AppSettings["serviceUri"] + "/GetListOf");
            FetchRelationUri = new Uri(ConfigurationManager.AppSettings["serviceUri"] + "/FetchRelation");
            GetBlobStreamUri = new Uri(ConfigurationManager.AppSettings["serviceUri"] + "/GetBlobStream");
            SetBlobStreamUri = new Uri(ConfigurationManager.AppSettings["serviceUri"] + "/SetBlobStream");
            InvokeServerMethodUri = new Uri(ConfigurationManager.AppSettings["serviceUri"] + "/InvokeServerMethod");

            _credentialsResolver = credentialsResolver;
            _readerFactory = readerFactory;
            _writerFactory = writerFactory;
        }

        private byte[] MakeRequest(Uri destination, Action<ZetboxStreamWriter> sendRequest)
        {
            var req = InitializeRequest(destination);

            using (var reqStream = req.GetRequestStream())
            using (var reqWriter = _writerFactory(new BinaryWriter(reqStream)))
            {
                sendRequest(reqWriter);
            }
            try
            {
                using (var response = req.GetResponse())
                using (var input = _readerFactory(new BinaryReader(response.GetResponseStream())))
                {
                    return input.ReadByteArray();
                }
            }
            catch (WebException ex)
            {
                var errorMsg = String.Format("Error when accessing server({0}): {1}", destination, ex.Status);
                Log.Error(errorMsg);
                var httpResponse = ex.Response as HttpWebResponse;
                if (httpResponse != null)
                {
                    if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        _credentialsResolver.InvalidCredentials();
                        throw new AuthenticationException("Authentication failed", ex);
                    }
                    else if (httpResponse.StatusCode == HttpStatusCode.Conflict)
                    {
                        var exToThrow = httpResponse
                            .GetResponseStream()
                            .FromXmlStream<ExceptionSerializationHelperContainer>()
                            .Exception
                            .ToException();
                        Log.Debug("Received an exception", exToThrow);
                        throw exToThrow;
                    }
                    else if (httpResponse.StatusCode == HttpStatusCode.PreconditionFailed)
                    {
                        throw new InvalidZetboxGeneratedVersionException();
                    }

                    Log.ErrorFormat("HTTP Error: {0}: {1}", httpResponse.StatusCode, httpResponse.StatusDescription);
                    foreach (var header in ex.Response.Headers)
                    {
                        var headerString = header.ToString();
                        Log.ErrorFormat("{0}: {1}", headerString, ex.Response.Headers[headerString]);
                    }
                }
                else
                {
                    Log.Error("No headers");
                }
                throw new IOException(errorMsg, ex);
            }
        }

        private WebRequest InitializeRequest(Uri destination)
        {
            var req = WebRequest.Create(destination);

            req.Method = WebRequestMethods.Http.Post;
            req.ContentType = "application/octet-stream";
            req.PreAuthenticate = true;
            var httpWebRequest = req as HttpWebRequest;
            if (httpWebRequest != null)
            {
                // must not be set for apache/mono server!
                //httpWebRequest.SendChunked = true;
                httpWebRequest.Pipelined = false;
                httpWebRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            }

            _credentialsResolver.SetCredentialsTo(req);
            return req;
        }

        public byte[] SetObjects(Guid version, byte[] msg, ObjectNotificationRequest[] notificationRequests)
        {
            return MakeRequest(SetObjectsUri,
                reqStream =>
                {
                    reqStream.Write(version);
                    reqStream.Write(msg);
                    reqStream.Write(notificationRequests);
                });
        }

        public byte[] GetList(Guid version, SerializableType type, int maxListCount, bool eagerLoadLists, SerializableExpression[] filter, OrderByContract[] orderBy)
        {
            if (type == null) throw new ArgumentNullException("type");

            return MakeRequest(GetListUri,
                reqStream =>
                {
                    reqStream.Write(version);
                    reqStream.Write(type);
                    reqStream.Write(maxListCount);
                    reqStream.Write(eagerLoadLists);
                    reqStream.Write(filter);
                    reqStream.Write(orderBy);
                    reqStream.WriteRaw(Encoding.ASCII.GetBytes("\n"));// required for basic.authenticated POST to apache
                });
        }

        public byte[] GetListOf(Guid version, SerializableType type, int ID, string property)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (String.IsNullOrEmpty(property)) throw new ArgumentNullException("property");

            return MakeRequest(GetListOfUri,
                reqStream =>
                {
                    reqStream.Write(version);
                    reqStream.Write(type);
                    reqStream.Write(ID);
                    reqStream.Write(property);
                    reqStream.WriteRaw(Encoding.ASCII.GetBytes("\n"));// required for basic.authenticated POST to apache
                });
        }

        public byte[] FetchRelation(Guid version, Guid relId, int role, int ID)
        {
            return MakeRequest(FetchRelationUri,
                reqStream =>
                {
                    reqStream.Write(version);
                    reqStream.Write(relId);
                    reqStream.Write(role);
                    reqStream.Write(ID);
                    reqStream.WriteRaw(Encoding.ASCII.GetBytes("\n"));// required for basic.authenticated POST to apache
                });
        }

        public Stream GetBlobStream(Guid version, int ID)
        {
            var req = InitializeRequest(new Uri(String.Format("{0}?id={1}", GetBlobStreamUri.AbsoluteUri, ID)));
            try
            {
                using (var reqStream = req.GetRequestStream())
                using (var reqWriter = _writerFactory(new BinaryWriter(reqStream)))
                {
                    reqWriter.Write(version);
                }
                using (var response = req.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    var result = new MemoryStream();
                    stream.CopyAllTo(result);
                    return result;
                }
            }
            catch (WebException ex)
            {
                var errorMsg = String.Format("Error when accessing server({0}): {1}: {2}", GetBlobStreamUri, ex.Status, ex.Response);
                Log.Error(errorMsg);
                throw new ApplicationException(errorMsg, ex);
            }
        }

        public ZetboxService.BlobResponse SetBlobStream(ZetboxService.BlobMessage request)
        {
            if (request == null) throw new ArgumentNullException("request");

            var req = InitializeRequest(SetBlobStreamUri);
            using (var reqStream = req.GetRequestStream())
            using (var reqWriter = _writerFactory(new BinaryWriter(reqStream)))
            using (var upload = new MemoryStream())
            {
                reqWriter.Write(request.Version);
                reqWriter.Write(request.FileName);
                reqWriter.Write(request.MimeType);
                request.Stream.CopyAllTo(upload);
                reqWriter.Write(upload.ToArray());
                reqWriter.WriteRaw(Encoding.ASCII.GetBytes("\n"));// required for basic.authenticated POST to apache
            }
            try
            {
                using (var response = req.GetResponse())
                using (var input = response.GetResponseStream())
                using (var reader = _readerFactory(new BinaryReader(input)))
                {
                    int id;
                    reader.Read(out id);

                    byte[] data;
                    reader.Read(out data);

                    return new ZetboxService.BlobResponse()
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

        public byte[] InvokeServerMethod(out byte[] retChangedObjects, Guid version, SerializableType type, int ID, string method, SerializableType[] parameterTypes, byte[] parameter, byte[] changedObjects, ObjectNotificationRequest[] notificationRequests)
        {
            if (type == null) throw new ArgumentNullException("type");

            var req = InitializeRequest(InvokeServerMethodUri);
            using (var reqStream = req.GetRequestStream())
            using (var reqWriter = _writerFactory(new BinaryWriter(reqStream)))
            {
                reqWriter.Write(version);
                reqWriter.Write(type);
                reqWriter.Write(ID);
                reqWriter.Write(method);
                reqWriter.Write(parameterTypes);
                reqWriter.Write(parameter);
                reqWriter.Write(changedObjects);
                reqWriter.Write(notificationRequests);
            }
            try
            {
                using (var response = req.GetResponse())
                using (var input = response.GetResponseStream())
                using (var reader = _readerFactory(new BinaryReader(input)))
                {
                    reader.Read(out retChangedObjects);
                    return reader.ReadByteArray();
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