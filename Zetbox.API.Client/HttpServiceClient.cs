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
    using System.Net.Http;
    using System.Security.Authentication;
    using System.Text;
    using System.Threading.Tasks;
    using Zetbox.API.Configuration;

    public sealed class HttpServiceClient
        : ZetboxService.IZetboxService
    {
        private const int MAX_RETRY_COUNT = 2;

        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger(typeof(HttpServiceClient));

        private readonly Uri SetObjectsUri;
        private readonly Uri GetObjectsUri;
        private readonly Uri GetListOfUri;
        private readonly Uri FetchRelationUri;
        private readonly Uri GetBlobStreamUri;
        private readonly Uri SetBlobStreamUri;
        private readonly Uri InvokeServerMethodUri;
        private readonly ICredentialsResolver _credentialsResolver;
        private readonly ZetboxStreamReader.Factory _readerFactory;
        private readonly ZetboxStreamWriter.Factory _writerFactory;

        public HttpServiceClient(ZetboxConfig config, ICredentialsResolver credentialsResolver, ZetboxStreamReader.Factory readerFactory, ZetboxStreamWriter.Factory writerFactory)
        {
            if (credentialsResolver == null) throw new ArgumentNullException("credentialsResolver");
            if (readerFactory == null) throw new ArgumentNullException("readerFactory");
            if (writerFactory == null) throw new ArgumentNullException("writerFactory");

            if (string.IsNullOrWhiteSpace(config.Client?.ServiceUri))
                throw new InvalidOperationException("ServiceUri is not configured");

            SetObjectsUri = new Uri(config.Client?.ServiceUri + "/SetObjects");
            GetObjectsUri = new Uri(config.Client?.ServiceUri + "/GetObjects");
            GetListOfUri = new Uri(config.Client?.ServiceUri + "/GetListOf");
            FetchRelationUri = new Uri(config.Client?.ServiceUri + "/FetchRelation");
            GetBlobStreamUri = new Uri(config.Client?.ServiceUri + "/GetBlobStream");
            SetBlobStreamUri = new Uri(config.Client?.ServiceUri + "/SetBlobStream");
            InvokeServerMethodUri = new Uri(config.Client?.ServiceUri + "/InvokeServerMethod");

            _credentialsResolver = credentialsResolver;
            _readerFactory = readerFactory;
            _writerFactory = writerFactory;
        }

        private async Task<byte[]> MakeRequest(Uri destination, Action<ZetboxStreamWriter> sendRequest)
        {
            using var httpClient = InitializeRequest();
            HttpResponseMessage response = null;

            using (var reqStream = new MemoryStream())
            using (var reqWriter = _writerFactory.Invoke(new BinaryWriter(reqStream)))
            {
                sendRequest(reqWriter);
                reqStream.Seek(0, SeekOrigin.Begin);
                var content = new StreamContent(reqStream);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                response = await httpClient.PostAsync(destination, content);
            }
            try
            {
                using (var input = _readerFactory.Invoke(new BinaryReader(response.Content.ReadAsStream())))
                {
                    return input.ReadByteArray();
                }
            }
            catch (HttpRequestException ex)
            {
                var errorMsg = String.Format("Error when accessing server({0}): {1}", destination, ex.StatusCode);
                Log.Error(errorMsg);

                switch (ex.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        _credentialsResolver.InvalidCredentials();
                        throw new AuthenticationException("Authentication failed", ex);
                    case HttpStatusCode.Conflict:
                        var exToThrow = response
                            .Content.ReadAsStream()
                            .FromXmlStream<ZetboxContextExceptionMessage>()
                            .Exception
                            .ToException();
                        Log.Debug("Received an exception", exToThrow);
                        throw exToThrow;
                    case HttpStatusCode.PreconditionFailed:
                        throw new InvalidZetboxGeneratedVersionException();
                    case HttpStatusCode.InternalServerError:
                        throw new WebException(ex.Message, ex);
                }
                throw new ZetboxServerIOException(errorMsg, ex);
            }
        }

        private HttpClient InitializeRequest()
        {
            var req = new HttpClient();
            // req.BaseAddress = destination;

            //req.Method = WebRequestMethods.Http.Post;
            //req.ContentType = "application/octet-stream";
            //req.PreAuthenticate = true;
            //var httpWebRequest = req as HttpWebRequest;
            //if (httpWebRequest != null)
            //{
            //    // must not be set for apache/mono server!
            //    //httpWebRequest.SendChunked = true;
            //    httpWebRequest.Pipelined = false;
            //    httpWebRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            //}

            _credentialsResolver.SetCredentialsTo(req);
            return req;
        }

        public async Task<byte[]> SetObjects(Guid version, byte[] msg, ObjectNotificationRequest[] notificationRequests)
        {
            return await MakeRequest(SetObjectsUri,
                reqStream =>
                {
                    reqStream.Write(version);
                    reqStream.Write(msg);
                    reqStream.Write(notificationRequests);
                });
        }

        public async Task<byte[]> GetObjects(Guid version, SerializableExpression query)
        {
            return await MakeRequest(GetObjectsUri,
              reqStream =>
              {
                  reqStream.Write(version);
                  reqStream.Write(query);
                  reqStream.WriteRaw(Encoding.ASCII.GetBytes("\n"));// required for basic.authenticated POST to apache
              });
        }

        public async Task<byte[]> GetListOf(Guid version, SerializableType type, int ID, string property)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (String.IsNullOrEmpty(property)) throw new ArgumentNullException("property");

            return await MakeRequest(GetListOfUri,
                reqStream =>
                {
                    reqStream.Write(version);
                    reqStream.Write(type);
                    reqStream.Write(ID);
                    reqStream.Write(property);
                    reqStream.WriteRaw(Encoding.ASCII.GetBytes("\n"));// required for basic.authenticated POST to apache
                });
        }

        public async Task<byte[]> FetchRelation(Guid version, Guid relId, int role, int ID)
        {
            return await MakeRequest(FetchRelationUri,
                reqStream =>
                {
                    reqStream.Write(version);
                    reqStream.Write(relId);
                    reqStream.Write(role);
                    reqStream.Write(ID);
                    reqStream.WriteRaw(Encoding.ASCII.GetBytes("\n"));// required for basic.authenticated POST to apache
                });
        }

        public async Task<Stream> GetBlobStream(Guid version, int ID)
        {
            using var req = InitializeRequest();
            try
            {
                HttpResponseMessage response;

                using (var reqStream = new MemoryStream())
                using (var reqWriter = _writerFactory.Invoke(new BinaryWriter(reqStream)))
                {
                    reqWriter.Write(version);
                    reqStream.Seek(0, SeekOrigin.Begin);
                    var content = new StreamContent(reqStream);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                    response = await req.PostAsync(new Uri(String.Format("{0}?id={1}", GetBlobStreamUri.AbsoluteUri, ID)), content);
                }
                using (var stream = response.Content.ReadAsStream())
                {
                    var result = new MemoryStream();
                    stream.CopyAllTo(result);
                    return result;
                }
            }
            catch (HttpRequestException ex)
            {
                var errorMsg = String.Format("Error when accessing server({0}): {1}", GetBlobStreamUri, ex.StatusCode);
                Log.Error(errorMsg);
                throw new InvalidOperationException(errorMsg, ex);
            }
        }

        public async Task<ZetboxService.BlobResponse> SetBlobStream(ZetboxService.BlobMessage request)
        {
            if (request == null) throw new ArgumentNullException("request");

            using var req = InitializeRequest();
            HttpResponseMessage response;

            using (var reqStream = new MemoryStream())
            using (var reqWriter = _writerFactory.Invoke(new BinaryWriter(reqStream)))
            using (var upload = new MemoryStream())
            {
                reqWriter.Write(request.Version);
                reqWriter.Write(request.FileName);
                reqWriter.Write(request.MimeType);
                request.Stream.CopyAllTo(upload);
                reqWriter.Write(upload.ToArray());
                reqWriter.WriteRaw(Encoding.ASCII.GetBytes("\n"));// required for basic.authenticated POST to apache

                reqStream.Seek(0, SeekOrigin.Begin);
                var content = new StreamContent(reqStream);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                response = await req.PostAsync(SetBlobStreamUri, content);
            }
            try
            {
                using (var input = response.Content.ReadAsStream())
                using (var reader = _readerFactory.Invoke(new BinaryReader(input)))
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
            catch (HttpRequestException ex)
            {
                var errorMsg = String.Format("Error when accessing server({0}): {1}", SetBlobStreamUri, ex.StatusCode);
                Log.Error(errorMsg);
                throw new ApplicationException(errorMsg, ex);
            }
        }

        public async Task<Tuple<byte[], byte[]>> InvokeServerMethod(Guid version, SerializableType type, int ID, string method, SerializableType[] parameterTypes, byte[] parameter, byte[] changedObjects, ObjectNotificationRequest[] notificationRequests)
        {
            if (type == null) throw new ArgumentNullException("type");

            using var req = InitializeRequest();
            HttpResponseMessage response;

            using (var reqStream = new MemoryStream())
            using (var reqWriter = _writerFactory.Invoke(new BinaryWriter(reqStream)))
            {
                reqWriter.Write(version);
                reqWriter.Write(type);
                reqWriter.Write(ID);
                reqWriter.Write(method);
                reqWriter.Write(parameterTypes);
                reqWriter.Write(parameter);
                reqWriter.Write(changedObjects);
                reqWriter.Write(notificationRequests);

                reqStream.Seek(0, SeekOrigin.Begin);
                var content = new StreamContent(reqStream);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                response = await req.PostAsync(InvokeServerMethodUri, content);
            }
            try
            {
                using (var input = response.Content.ReadAsStream())
                using (var reader = _readerFactory.Invoke(new BinaryReader(input)))
                {
                    byte[] retChangedObjects;
                    reader.Read(out retChangedObjects);
                    return new Tuple<byte[], byte[]>(reader.ReadByteArray(), retChangedObjects);
                }
            }
            catch (HttpRequestException ex)
            {
                var errorMsg = String.Format("Error when accessing server({0}): {1}", InvokeServerMethodUri, ex.StatusCode);
                Log.Error(errorMsg);
                throw new ApplicationException(errorMsg, ex);
            }
        }
    }
}