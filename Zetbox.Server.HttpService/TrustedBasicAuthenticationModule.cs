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

namespace Zetbox.Server.HttpService
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
                //string password = up[1]; // as the name says, trusted

                app.Context.User = new GenericPrincipal(new GenericIdentity(username, authenticationMethod), new string[0]);
            }
        }
    }
}