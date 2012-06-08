namespace Kistl.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Security.Principal;
    using System.Text;
    using Autofac;
    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.API.Common;
    using Kistl.App.Base;
    using Kistl.App.GUI;
    using Kistl.Client.Models;
    using Kistl.Client.Presentables;
    using Kistl.Client.Presentables.ValueViewModels;
    using Kistl.API.Utils;

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
                    .Register<BasicAuthCredentialsResolver>(c => new BasicAuthCredentialsResolver(c.Resolve<IViewModelFactory>(), c.Resolve<Func<BaseMemoryContext>>(), c.Resolve<IFrozenContext>()))
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
        private IFrozenContext _frozenCtx;
        private string UserName = null;
        private string Password = null;

        public BasicAuthCredentialsResolver(IViewModelFactory vmf, Func<BaseMemoryContext> ctxFactory, IFrozenContext frozenCtx)
        {
            if (vmf == null) throw new ArgumentNullException("vmf");
            if (ctxFactory == null) throw new ArgumentNullException("ctxFactory");
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");

            _vmf = vmf;
            _ctxFactory = ctxFactory;
            _frozenCtx = frozenCtx;
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
                            var valueModels = new List<BaseValueViewModel>();

                            var userName = new ClassValueModel<string>(CredentialsResolverResources.UserNameLabel, "", false, false);
                            valueModels.Add(_vmf.CreateViewModel<ClassValueViewModel<string>.Factory>().Invoke(ctx, null, userName));

                            var pwd = new ClassValueModel<string>(CredentialsResolverResources.PasswordLabel, "", false, false);
                            var pwdvm = _vmf.CreateViewModel<ClassValueViewModel<string>.Factory>().Invoke(ctx, null, pwd);
                            pwdvm.RequestedKind = Kistl.NamedObjects.Gui.ControlKinds.Kistl_App_GUI_PasswordKind.Find(_frozenCtx);
                            valueModels.Add(pwdvm);

                            var dlgOK = false;

                            var dlg = _vmf.CreateViewModel<ValueInputTaskViewModel.Factory>().Invoke(ctx, null, CredentialsResolverResources.DialogTitle, valueModels, (p) =>
                            {
                                this.UserName = userName.Value;
                                this.Password = pwd.Value;
                                dlgOK = true;
                            });

                            _vmf.ShowDialog(dlg);

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

        public BasicAuthIdentityResolver(Func<IReadOnlyKistlContext> resolverCtxFactory, BasicAuthCredentialsResolver credentialResolver)
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
