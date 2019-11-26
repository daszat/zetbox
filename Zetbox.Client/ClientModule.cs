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
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.API.Common;
    using Zetbox.API.Configuration;
    using Zetbox.App.Extensions;
    using Zetbox.Client.Presentables;
    using Zetbox.API.Common.GUI;
    using System.ComponentModel;
    using Zetbox.API.Common.Reporting;
using Zetbox.API.Client.PerfCounter;

    // [Feature]
    // Not a feature, default
    [Description("The Client Module")]
    public sealed class ClientModule : Module
    {
        private class ViewModelDependencies : IViewModelDependencies
        {
            public ViewModelDependencies(IViewModelFactory f, IFrozenContext frozenCtx, IPrincipalResolver principalResolver, IIconConverter iconConverter, IAssetsManager assetMgr, IValidationManager validationManager, IPerfCounter perfCounter)
            {
                Factory = f;
                FrozenContext = frozenCtx;
                PrincipalResolver = principalResolver;
                IconConverter = iconConverter;
                Assets = assetMgr;
                ValidationManager = validationManager;
                PerfCounter = perfCounter;
            }

            #region IViewModelDependencies Members

            public IViewModelFactory Factory
            {
                get;
                private set;
            }

            public IFrozenContext FrozenContext
            {
                get;
                private set;
            }

            public IPrincipalResolver PrincipalResolver
            {
                get;
                private set;
            }

            public IIconConverter IconConverter
            {
                get;
                private set;
            }

            public IAssetsManager Assets
            {
                get;
                private set;
            }

            public IValidationManager ValidationManager
            {
                get;
                private set;
            }

            public IPerfCounter PerfCounter
            {
                get;
                private set;
            }

            #endregion
        }

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder.RegisterModule<Zetbox.API.Common.ApiCommonModule>();
            moduleBuilder.RegisterModule<Zetbox.API.Client.ClientApiModule>();

            moduleBuilder
                .Register<ViewModelDependencies>(c => new ViewModelDependencies(
                    c.Resolve<IViewModelFactory>(),
                    c.Resolve<IFrozenContext>(),
                    c.Resolve<IPrincipalResolver>(),
                    c.Resolve<IIconConverter>(),
                    c.Resolve<IAssetsManager>(),
                    c.Resolve<IValidationManager>(),
                    c.Resolve<IPerfCounter>()))
                .As<IViewModelDependencies>();

            moduleBuilder
                .Register<LifetimeScopeFactory>(c => new LifetimeScopeFactory(c.Resolve<ILifetimeScope>()))
                .As<ILifetimeScopeFactory>()
                .SingleInstance();

            moduleBuilder
                .Register<ThreadPrincipalResolver>(c => new ThreadPrincipalResolver(c.Resolve<ILifetimeScope>()))
                .As<IPrincipalResolver>()
                .SingleInstance();

            moduleBuilder
                .Register<LoggingProblemReporter>(c => new LoggingProblemReporter())
                .As<IProblemReporter>()
                .SingleInstance();

            moduleBuilder
                .Register<DefaultCredentialsResolver>(c => new DefaultCredentialsResolver())
                .As<ICredentialsResolver>()
                .SingleInstance();

            moduleBuilder
                .RegisterType<Zetbox.Client.GUI.DialogCreator>()
                .AsSelf()
                .InstancePerDependency();

            moduleBuilder
                .RegisterType<ZetboxContextExceptionHandler>()
                .As<IZetboxContextExceptionHandler>()
                .SingleInstance();

            moduleBuilder
                .RegisterType<Zetbox.Client.Reporting.ReportingErrorDialog>()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            moduleBuilder
               .RegisterType<ValidationManager>()
               .As<IValidationManager>()
               .InstancePerLifetimeScope();

            moduleBuilder
                .Register<Zetbox.Client.Reporting.ReportingHost>(c => new Zetbox.Client.Reporting.ReportingHost(
                        "Zetbox.App.Projekte.Client.DerivedReportTest",
                        typeof(ClientModule).Assembly,
                        c.Resolve<IFileOpener>(),
                        c.Resolve<ITempFileService>(),
                        c.Resolve<IReportingErrorReporter>()
                    )
                )
                .InstancePerDependency();

            moduleBuilder
                .RegisterType<UIExceptionReporter>()
                .As<IUIExceptionReporter>()
                .SingleInstance();

            moduleBuilder.RegisterViewModels(typeof(ClientModule).Assembly);

            moduleBuilder.RegisterModule((Module)Activator.CreateInstance(Type.GetType("Zetbox.DalProvider.Client.ClientProvider, Zetbox.DalProvider.ClientObjects", true)));
            moduleBuilder.RegisterModule((Module)Activator.CreateInstance(Type.GetType("Zetbox.App.Projekte.Client.CustomClientActionsModule, Zetbox.App.Projekte.Client", true)));
        }
    }
}
