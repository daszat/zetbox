namespace Kistl.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
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
    using System.Security.Principal;

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

                        var dlg = _vmf.CreateViewModel<ValueInputTaskViewModel.Factory>().Invoke(ctx, "Enter Credentials", valueModels, (p) =>
                        {
                            this.UserName = userName.Value;
                            this.Password = pwd.Value;
                        });

                        _vmf.ShowDialog(dlg);
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

        internal string GetUsername()
        {
            EnsureUsername();
            return UserName;
        }
    }

    public class BasicAuthIdentityResolver
        : IIdentityResolver
    {
        private readonly IReadOnlyKistlContext _resolverCtx;
        private readonly BasicAuthCredentialsResolver _credentialResolver;

        public BasicAuthIdentityResolver(IReadOnlyKistlContext resolverCtx, BasicAuthCredentialsResolver credentialResolver)
        {
            _resolverCtx = resolverCtx;
            _credentialResolver = credentialResolver;
        }

        public Identity GetCurrent()
        {
            return Resolve(_credentialResolver.GetUsername());
        }

        public Identity Resolve(IIdentity identity)
        {
            if (identity == null) throw new ArgumentNullException("identity");

            return Resolve(identity.Name);
        }

        private Identity Resolve(string name)
        {
            string id = name.ToLower();
            return _resolverCtx.GetQuery<Identity>().Where(i => i.UserName.ToLower() == id).FirstOrDefault();
        }
    }
}
