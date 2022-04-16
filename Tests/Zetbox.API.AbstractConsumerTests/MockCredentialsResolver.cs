
namespace Zetbox.API.AbstractConsumerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using Zetbox.API.Client;

    /// <summary>
    /// uses jenkins/jenkins as username/password
    /// </summary>
    public class MockCredentialsResolver : ICredentialsResolver
    {
        public void EnsureCredentials()
        {
        }

        public void SetCredentialsTo(HttpClient req)
        {
            req.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes("jenkins:jenkins")));
        }

        public void InvalidCredentials()
        {
        }

        public void Freeze()
        {
        }
    }
}
