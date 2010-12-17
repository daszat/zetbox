namespace Kistl.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API.Client;
    using Autofac;
    using Kistl.Client.Presentables;
    using Kistl.API;
    using Kistl.Client.Presentables.ValueViewModels;
    using Kistl.Client.Models;

    public class DefaultCredentialsResolver : ICredentialsResolver
    {
        public class Module : Autofac.Module
        {
            protected override void Load(Autofac.ContainerBuilder builder)
            {
                base.Load(builder);

                builder
                    .RegisterType<DefaultCredentialsResolver>()
                    .As<ICredentialsResolver>()
                    .SingleInstance();
            }
        }

        public void InitCredentials(System.ServiceModel.Description.ClientCredentials c)
        {
            // Gracefully do nothing
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
                    .RegisterType<BasicAuthCredentialsResolver>()
                    .As<ICredentialsResolver>()
                    .SingleInstance();
            }
        }

        private static object _lock = new object();

        private IViewModelFactory _vmf;
        private Func<IKistlContext> _ctxFactory;
        private string UserName = null;
        private string Password = null;

        public BasicAuthCredentialsResolver(IViewModelFactory vmf, Func<IKistlContext> ctxFactory)
        {
            _vmf = vmf;
            _ctxFactory = ctxFactory;
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
                        valueModels.Add(_vmf.CreateViewModel<ClassValueViewModel<string>.Factory>().Invoke(ctx, pwd));

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
    }
}
