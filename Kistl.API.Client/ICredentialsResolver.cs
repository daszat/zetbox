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
        /// <summary>
        /// Initializes the given ClientCredentials
        /// </summary>
        /// <param name="c">ClientCredentials to initialize</param>
        void InitCredentials(ClientCredentials c);
        /// <summary>
        /// Initializes the given WebRequest
        /// </summary>
        /// <param name="req">WebRequest to initialize</param>
        void InitWebRequest(WebRequest req);

        /// <summary>
        /// Called by the using class to report invalid credentials.
        /// Implementors should reset their internal state and rerequest credentials from the user.
        /// </summary>
        void InvalidCredentials();
    }
}
