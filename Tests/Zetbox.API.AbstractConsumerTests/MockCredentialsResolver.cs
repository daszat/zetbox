
namespace Zetbox.API.AbstractConsumerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.ServiceModel.Description;
    using System.Text;
    using Zetbox.API.Client;

    /// <summary>
    /// uses zetbox/zetbox as username/password
    /// </summary>
    public class MockCredentialsResolver : ICredentialsResolver
    {
        public void EnsureCredentials()
        {
        }

        public void InitCredentials(ClientCredentials c)
        {
            c.UserName.UserName = "zetbox";
            c.UserName.Password = "zetbox";
        }

        public void InitWebRequest(WebRequest req)
        {
            req.PreAuthenticate = true; // always send credentials, reduces startup and testing overhead
            req.Credentials = new NetworkCredential("zetbox", "zetbox");
        }

        public void InvalidCredentials()
        {
        }
    }
}
