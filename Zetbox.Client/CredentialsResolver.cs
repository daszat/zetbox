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
    using System.Linq;
    using System.Net;
    using System.Security.Principal;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.API.Common;
    using Zetbox.App.Base;
    using Zetbox.App.GUI;
    using Zetbox.Client.GUI;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.API.Utils;

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

        public void InitCredentials(System.ServiceModel.Description.ClientCredentials c)
        {
            // Gracefully do nothing
            // Set implicity by WindowsAuthentication
        }


        public void InitWebRequest(WebRequest req)
        {
            if (req == null) throw new ArgumentNullException("req");

            req.Credentials = CredentialCache.DefaultCredentials;
        }

        public void InvalidCredentials()
        {
            // Exit application, server won't talk to us
            Environment.Exit(1);
        }
    }

    public class BasicAuthCredentialsResolver : ICredentialsResolver
    {
        public class Module : Autofac.Module
        {
            protected override void Load(Autofac.ContainerBuilder builder)
            {
                base.Load(builder);

                builder
                    .Register<BasicAuthCredentialsResolver>(c => new BasicAuthCredentialsResolver(c.Resolve<IViewModelFactory>(), c.Resolve<Func<BaseMemoryContext>>()))
                    .As<BasicAuthCredentialsResolver>() // for local use
                    .As<ICredentialsResolver>() // for publication
                    .SingleInstance();

                // since the user has entered credentials, now we also have a different Identity
                builder
                    .RegisterType<BasicAuthIdentityResolver>()
                    .As<IIdentityResolver>()
                    .InstancePerLifetimeScope();
            }
        }

        private static object _lock = new object();

        private IViewModelFactory _vmf;
        private Func<BaseMemoryContext> _ctxFactory;
        private string UserName = null;
        private string Password = null;

        public BasicAuthCredentialsResolver(IViewModelFactory vmf, Func<BaseMemoryContext> ctxFactory)
        {
            if (vmf == null) throw new ArgumentNullException("vmf");
            if (ctxFactory == null) throw new ArgumentNullException("ctxFactory");

            _vmf = vmf;
            _ctxFactory = ctxFactory;
        }

        private bool _isEnsuringCredentials = false;
        public void EnsureCredentials()
        {
            lock (_lock) // singelton, once is enougth
            {
                if (_isEnsuringCredentials)
                {
                    Logging.Client.Warn("Nested credentials resolving detected");
                    return;
                }
                _isEnsuringCredentials = true;
                try
                {
                    if (string.IsNullOrEmpty(UserName))
                    {
                        using (var ctx = _ctxFactory())
                        {
                            var dlgOK = false;
                            _vmf.CreateDialog(CredentialsResolverResources.DialogTitle)
                                .AddString(CredentialsResolverResources.UserNameLabel)
                                .AddPassword(CredentialsResolverResources.PasswordLabel)
                                .Show((p) =>
                                {
                                    this.UserName = (string)p[0];
                                    this.Password = (string)p[1];
                                    dlgOK = true;
                                });

                            if (!dlgOK)
                            {
                                // No credentials? User pressed cancel? exit application
                                Environment.Exit(1);
                            }
                        }
                    }
                }
                finally
                {
                    _isEnsuringCredentials = false;
                }
            }
        }

        public void InitCredentials(System.ServiceModel.Description.ClientCredentials c)
        {
            if (c == null) throw new ArgumentNullException("c");

            EnsureCredentials();
            c.UserName.UserName = UserName;
            c.UserName.Password = Password;
        }

        public void InitWebRequest(WebRequest req)
        {
            if (req == null) throw new ArgumentNullException("req");

            EnsureCredentials();
            req.PreAuthenticate = true; // always send credentials, reduces startup and testing overhead
            req.Credentials = new NetworkCredential(UserName, Password);
        }

        public void InvalidCredentials()
        {
            // Reset username/password
            UserName = null;
            Password = null;
        }

        internal string GetUsername()
        {
            EnsureCredentials();
            return UserName;
        }
    }

    public class BasicAuthIdentityResolver
        : BaseIdentityResolver
    {
        private readonly BasicAuthCredentialsResolver _credentialResolver;

        public BasicAuthIdentityResolver(Func<IReadOnlyZetboxContext> resolverCtxFactory, BasicAuthCredentialsResolver credentialResolver)
            : base(resolverCtxFactory)
        {
            if (credentialResolver == null) throw new ArgumentNullException("credentialResolver");

            _credentialResolver = credentialResolver;
        }

        public override Identity GetCurrent()
        {
            Identity result = null;
            while (result == null)
            {
                var userName = _credentialResolver.GetUsername();
                result = Resolve(userName);
            }
            return result;
        }
    }
}
