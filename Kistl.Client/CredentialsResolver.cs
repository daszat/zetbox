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
                    .Register<BasicAuthCredentialsResolver>(c => new BasicAuthCredentialsResolver(c.Resolve<IViewModelFactory>(), c.Resolve<Func<IKistlContext>>(), c.Resolve<IFrozenContext>()))
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
        private Func<IKistlContext> _ctxFactory;
        private IFrozenContext _frozenCtx;
        private string UserName = null;
        private string Password = null;

        public BasicAuthCredentialsResolver(IViewModelFactory vmf, Func<IKistlContext> ctxFactory, IFrozenContext frozenCtx)
        {
            if (vmf == null) throw new ArgumentNullException("vmf");
            if (ctxFactory == null) throw new ArgumentNullException("ctxFactory");
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");

            _vmf = vmf;
            _ctxFactory = ctxFactory;
            _frozenCtx = frozenCtx;
        }

        public void InitCredentials(System.ServiceModel.Description.ClientCredentials c)
        {
            if (c == null) throw new ArgumentNullException("c");

            EnsureUsername();
            c.UserName.UserName = UserName;
            c.UserName.Password = Password;
        }

        private void EnsureUsername()
        {
            lock (_lock) // singelton, once is enougth
            {
                if (string.IsNullOrEmpty(UserName))
                {
                    using (var ctx = _ctxFactory())
                    {
                        var valueModels = new List<BaseValueViewModel>();

                        var userName = new ClassValueModel<string>("Username", "", false, false);
                        valueModels.Add(_vmf.CreateViewModel<ClassValueViewModel<string>.Factory>().Invoke(ctx, userName));

                        var pwd = new ClassValueModel<string>("Password", "", false, false);
                        var pwdvm = _vmf.CreateViewModel<ClassValueViewModel<string>.Factory>().Invoke(ctx, pwd);
                        pwdvm.RequestedKind = _frozenCtx.FindPersistenceObject<ControlKind>(NamedObjects.ControlKind_Kistl_App_GUI_PasswordKind);
                        valueModels.Add(pwdvm);

                        var dlgOK = false;

                        var dlg = _vmf.CreateViewModel<ValueInputTaskViewModel.Factory>().Invoke(ctx, "Enter Credentials", valueModels, (p) =>
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
        }

        public void InitWebRequest(WebRequest req)
        {
            if (req == null) throw new ArgumentNullException("req");

            EnsureUsername();
            req.Credentials = new NetworkCredential(UserName, Password);
        }

        public void InvalidCredentials()
        {
            // nothing to do
            UserName = null;
            Password = null;
        }

        internal string GetUsername()
        {
            EnsureUsername();
            return UserName;
        }
    }

    public class BasicAuthIdentityResolver
        : BaseIdentityResolver
    {
        private readonly BasicAuthCredentialsResolver _credentialResolver;

        public BasicAuthIdentityResolver(IReadOnlyKistlContext resolverCtx, BasicAuthCredentialsResolver credentialResolver)
            : base(resolverCtx)
        {
            if (credentialResolver == null) throw new ArgumentNullException("credentialResolver");

            _credentialResolver = credentialResolver;
        }

        public override Identity GetCurrent()
        {
            return Resolve(_credentialResolver.GetUsername());
        }
    }
}
