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
namespace Zetbox.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Security.Authentication;
    using System.Security.Principal;
    using System.Text;
    using System.Threading;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.API.Common;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.GUI;
    using Zetbox.Client.GUI;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ValueViewModels;

    public class DefaultCredentialsResolver : ICredentialsResolver
    {
        public class Module : Autofac.Module
        {
            protected override void Load(Autofac.ContainerBuilder builder)
            {
                base.Load(builder);

                builder
                    .Register<DefaultCredentialsResolver>(c => new DefaultCredentialsResolver())
                    .As<ICredentialsResolver>()
                    .SingleInstance();
            }
        }

        public void EnsureCredentials()
        {
            // Gracefully do nothing
            // Using Windows Credentials, they are already set by the operating system
        }

        public void SetCredentialsTo(HttpClient req)
        {
            if (req == null) throw new ArgumentNullException("req");

            // req.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue( CredentialCache.DefaultCredentials;
        }

        public void InvalidCredentials()
        {
            string name;

            if (!string.IsNullOrEmpty(Thread.CurrentPrincipal?.Identity?.Name))
                name = Thread.CurrentPrincipal.Identity.Name;
            else
                name = WindowsIdentity.GetCurrent()?.Name ?? string.Empty;

            throw new AuthenticationException(string.Format("You are not authorized to access this application. (username={0})",
                !string.IsNullOrWhiteSpace(name) ? name : "<empty>"));
        }

        public void Freeze()
        {
            // doesn't need to do anything.
        }
    }

    public interface IPasswordDialog
    {
        string Username { get; }
        string Password { get; }

        bool QueryUser();
    }

    public sealed class BasicAuthCredentialsResolver : ICredentialsResolver
    {
        [Feature]
        [Description("Credential resolver for basic authentication")]
        public class Module : Autofac.Module
        {
            protected override void Load(Autofac.ContainerBuilder builder)
            {
                base.Load(builder);

                builder
                    .Register<BasicAuthCredentialsResolver>(c => new BasicAuthCredentialsResolver(c.Resolve<IPasswordDialog>()))
                    .As<BasicAuthCredentialsResolver>() // for local use
                    .As<ICredentialsResolver>() // for publication
                    .SingleInstance();

                // since the user has entered credentials, now we also have a different Identity
                builder
                    .RegisterType<BasicAuthPrincipalResolver>()
                    .As<IPrincipalResolver>()
                    .SingleInstance();
            }
        }

        private static object _lock = new object();
        private string UserName = null;
        private string Password = null;
        private IPasswordDialog _pwDlg;

        internal BasicAuthCredentialsResolver(IPasswordDialog pwDlg)
        {
            if (pwDlg == null) throw new ArgumentNullException("pwDlg");

            _pwDlg = pwDlg;
        }

        private bool _isEnsuringCredentials = false;
        public void EnsureCredentials()
        {
            lock (_lock)
            {
                if (_isEnsuringCredentials)
                {
                    Logging.Client.Warn("Nested credentials resolving detected");
                    // singleton, once is enough
                    return;
                }
                _isEnsuringCredentials = true;
            }
            try
            {
                if (string.IsNullOrEmpty(UserName))
                {
                    if (_pwDlg.QueryUser())
                    {
                        this.UserName = _pwDlg.Username;
                        this.Password = _pwDlg.Password;
                    }
                    else
                    {
                        // No credentials? User pressed cancel? exit application
                        Environment.Exit(1);
                    }
                }
            }
            finally
            {
                lock (_lock) _isEnsuringCredentials = false;
            }
        }

        public void SetCredentialsTo(HttpClient req)
        {
            if (req == null) throw new ArgumentNullException("req");

            EnsureCredentials();
            req.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(UserName + ":" + Password)));
        }

        public void InvalidCredentials()
        {
            if (_isFrozen)
            {
                Logging.Client.Fatal("Exiting due to invalid credentials after freezing.");
                Environment.Exit(1);
            }
            // Reset username/password
            UserName = null;
            Password = null;
        }

        private bool _isFrozen = false;
        public void Freeze()
        {
            _isFrozen = true;
        }

        internal string GetUsername()
        {
            EnsureCredentials();
            return UserName;
        }
    }

    public class BasicAuthPrincipalResolver
        : BasePrincipalResolver
    {
        private readonly BasicAuthCredentialsResolver _credentialResolver;

        public BasicAuthPrincipalResolver(ILifetimeScope parentScope, BasicAuthCredentialsResolver credentialResolver)
            : base(parentScope)
        {
            if (credentialResolver == null) throw new ArgumentNullException("credentialResolver");

            _credentialResolver = credentialResolver;
        }

        public override ZetboxPrincipal GetCurrent()
        {
            ZetboxPrincipal result = null;
            while (result == null)
            {
                var userName = _credentialResolver.GetUsername();
                result = Resolve(userName);
                if (result == null)
                {
                    _credentialResolver.InvalidCredentials();
                }
            }
            return result;
        }
    }
}
