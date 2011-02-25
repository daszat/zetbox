
namespace Kistl.Server.HttpService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Text;
    using System.Web;

    /// <summary>
    /// This class trusts the provided Basic authentication response without checking the password. This is very useful when being hosted by apache/mod_mono which only passes on the Authorization header.
    /// </summary>
    public class TrustedBasicAuthenticationModule
        : IHttpModule
    {
        private static readonly char[] separator = { ':' };
        private static readonly string authenticationMethod = "Basic";

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += new EventHandler(this.OnAuthenticateRequest);
        }

        public virtual void OnAuthenticateRequest(object source, EventArgs eventArgs)
        {
            HttpApplication app = (HttpApplication)source;
            string autz = app.Request.Headers["Authorization"];
            if ((autz == null) || (autz.Length == 0))
            {
                // No credentials; anonymous request
                return;
            }

            if (autz.ToUpperInvariant().StartsWith(authenticationMethod.ToUpperInvariant()))
            {
                var authentication = autz.Substring(authenticationMethod.Length + 1);
                byte[] userpass = Convert.FromBase64String(authentication);
                string[] up = Encoding.UTF8.GetString(userpass).Split(separator);
                string username = up[0];
                string password = up[1];

                app.Context.User = new GenericPrincipal(new GenericIdentity(username, authenticationMethod), new string[0]);
            }
        }
    }
}