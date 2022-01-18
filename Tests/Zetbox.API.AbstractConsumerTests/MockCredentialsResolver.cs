
namespace Zetbox.API.AbstractConsumerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
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

        public void SetCredentialsTo(WebRequest req)
        {
            req.PreAuthenticate = true; // always send credentials, reduces startup and testing overhead
            req.Credentials = new NetworkCredential("jenkins", "jenkins");
        }

        public void InvalidCredentials()
        {
        }

        public void Freeze()
        {
        }
    }
}
