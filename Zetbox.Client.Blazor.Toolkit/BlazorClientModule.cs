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

namespace Zetbox.Client.Blazor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel;
    using Autofac;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.API.Common;
    using Zetbox.API.Common.GUI;
    using Zetbox.API.Client;
    using Zetbox.API.Client.PerfCounter;

    [Description("The Blazor Client Module. It replaces the Client Module.")]
    public class BlazorClientModule : Module
    {
        #region ViewModelDependencies
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
        #endregion

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder.RegisterModule<Zetbox.API.Common.ApiCommonModule>();
            moduleBuilder.RegisterModule<Zetbox.API.Client.ClientApiModule>();
            moduleBuilder.RegisterModule<Zetbox.Client.ClientModule>();

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
                .Register<LoggingProblemReporter>(c => new LoggingProblemReporter())
                .As<IProblemReporter>()
                .SingleInstance();

            moduleBuilder
                .RegisterType<BlazorViewModelFactory>()
                .As<IViewModelFactory>()
                .As<IToolkit>()
                .AsSelf()
                .InstancePerLifetimeScope();

            moduleBuilder
                .RegisterType<Zetbox.Client.GUI.DialogCreator>()
                .AsSelf()
                .InstancePerDependency();

            moduleBuilder.RegisterViewModels(typeof(ClientModule).Assembly);
            moduleBuilder.RegisterViewModels(typeof(BlazorClientModule).Assembly);
            moduleBuilder.RegisterModule((Module)Activator.CreateInstance(Type.GetType("Zetbox.App.Projekte.Client.CustomClientActionsModule, Zetbox.App.Projekte.Client", true)));
        }
    }
}
