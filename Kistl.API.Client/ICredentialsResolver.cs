using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.Net;

namespace Kistl.API.Client
{
    public interface ICredentialsResolver
    {
        void InitCredentials(ClientCredentials c);
        void InitWebRequest(WebRequest req);
    }
}
