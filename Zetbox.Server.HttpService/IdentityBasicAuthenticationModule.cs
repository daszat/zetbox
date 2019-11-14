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
    using System.IO;
    using System.Linq;
    using System.Security.Principal;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using Autofac;
    using Autofac.Integration.Web;
    using CryptSharp;
    using log4net;
    using Zetbox.API;
    using Zetbox.API.Server;

    public class IdentityBasicAuthenticationModule : IHttpModule
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(IdentityBasicAuthenticationModule));

        #region Basic Auth
        private static readonly char[] separator = { ':' };
        private static readonly string authenticationMethod = "Basic";

        public void AuthenticateUser(object source, EventArgs e)
        {
            var application = (HttpApplication)source;
            var context = application.Context;
            var authorizationHeader = context.Request.Headers["Authorization"];
            string userName = null;
            string password = null;

            if (!ExtractBasicCredentials(authorizationHeader, ref userName, ref password))
                return;

            if (!ValidateCredentials(userName, password))
                return;

            context.User = new GenericPrincipal(new GenericIdentity(userName), null);
        }

        public void IssueAuthenticationChallenge(object source, EventArgs e)
        {
            var application = (HttpApplication)source;
            var context = application.Context;

            if (context.Response.StatusCode == 401)
            {
                context.Response.AddHeader("WWW-Authenticate", "Basic realm =\"zetbox\"");
            }
        }

        protected virtual bool ValidateCredentials(String userName, String password)
        {
            userName = userName.ToLower();
            var cpa = (IContainerProviderAccessor)HttpContext.Current.ApplicationInstance;
            var scope = cpa.ContainerProvider.RequestLifetime;
            using(var ctx = scope.Resolve<IZetboxServerContext>())
            {
                var user = ctx.GetQuery<App.Base.Identity>().Where(i => i.UserName.ToLower() == userName).FirstOrDefault();
                if (user == null) return false;
                if (string.IsNullOrWhiteSpace(user.Password)) return false;

                return Crypter.CheckPassword(password, user.Password);
            }
        }

        protected virtual bool ExtractBasicCredentials(string authorizationHeader, ref string username, ref string password)
        {
            if (string.IsNullOrWhiteSpace(authorizationHeader))
            {
                // No credentials; anonymous request
                return false;
            }

            if (authorizationHeader.ToUpperInvariant().StartsWith(authenticationMethod.ToUpperInvariant()))
            {
                var authentication = authorizationHeader.Substring(authenticationMethod.Length + 1);
                byte[] userpass = Convert.FromBase64String(authentication);
                string[] up = Encoding.UTF8.GetString(userpass).Split(separator);
                if (up.Length != 2) return false;

                username = up[0];
                password = up[1];

                return true;
            }

            return false;
        }
        #endregion

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += this.AuthenticateUser;
            context.EndRequest += this.IssueAuthenticationChallenge;
        }
        public void Dispose()
        {
        }
    }
}