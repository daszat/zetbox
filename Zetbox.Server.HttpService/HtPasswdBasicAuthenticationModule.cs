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
    using CryptSharp;
    using log4net;

    public class HtPasswdBasicAuthenticationModule : IHttpModule
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(HtPasswdBasicAuthenticationModule));

        #region Read HtPasswd
        private static readonly Regex regEx = new Regex("(?<user>.*?):(?<password>.*)", RegexOptions.Singleline);
        private static object _lock = new object();
        private static ILookup<string, Entry> _cache = null;
        private static FileSystemWatcher _fso = null;

        private class Entry
        {
            public string User { get; set; }
            public string CryptedPassword { get; set; }
        }

        private ILookup<string, Entry> User
        {
            get
            {
                lock (_lock)
                {
                    if (_cache != null) return _cache;

                    var fileName = System.Configuration.ConfigurationManager.AppSettings["htpasswdpath"];
                    if (string.IsNullOrWhiteSpace(fileName))
                    {
                        // Try the default place
                        var baseSearchPath = Path.GetFullPath(Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~"), "..", "Configs"));
                        fileName = Path.Combine(baseSearchPath, ".htpasswd");
                        if (!File.Exists(fileName))
                        {
                            fileName = Path.Combine(baseSearchPath, "_htpasswd");
                        }
                    }

                    if (!File.Exists(fileName))
                    {
                        throw new InvalidOperationException("Could not find a htpasswd file, neither defined found you config, nor found on the configurated path, nor found in ../Configs/.htpasswd or ../Configs/_htpasswd");
                    }

                    _log.InfoFormat("Using htpasswd '{0}'", fileName);

                    _fso = new FileSystemWatcher(Path.GetDirectoryName(fileName), Path.GetFileName(fileName));
                    _fso.Changed += (s, e) =>
                    {
                        lock (_lock)
                        {
                            _cache = null;
                            _log.Info("htpasswd file changed, cache cleard");
                        }
                    };
                    _fso.EnableRaisingEvents = true;

                    var result = new List<Entry>();
                    using (var sr = new StreamReader(fileName))
                    {
                        while (true)
                        {
                            var line = sr.ReadLine();
                            if (line == null) break;

                            var match = regEx.Match(line);
                            if (match.Success)
                            {
                                result.Add(new Entry()
                                {
                                    User = match.Groups["user"].Value,
                                    CryptedPassword = match.Groups["password"].Value,
                                });
                            }
                        }
                    }

                    _cache = result.ToLookup(k => k.User);
                    return _cache;
                }
            }
        }
        #endregion

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
            return User[userName].Any(usr => Crypter.CheckPassword(password, usr.CryptedPassword));
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
            _log.Info("Initializing HtPasswdBasicAuthenticationModule");
            context.AuthenticateRequest += this.AuthenticateUser;
            context.EndRequest += this.IssueAuthenticationChallenge;
        }
        public void Dispose()
        {
        }
    }
}