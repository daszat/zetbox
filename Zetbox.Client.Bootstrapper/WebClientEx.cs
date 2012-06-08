using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Kistl.Client.Bootstrapper
{
    public class WebClientEx : WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            var result = base.GetWebRequest(address);
            result.PreAuthenticate = true;
            return result;
        }
    }
}
