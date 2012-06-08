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
    using Zetbox.API.Configuration;
    using Zetbox.App.Extensions;
    using Zetbox.Client.Presentables;
    using Zetbox.API.Common;

    public sealed class ClientModule : Module
    {
        private class ViewModelDependencies : IViewModelDependencies
        {
            public ViewModelDependencies(IViewModelFactory f, IUiThreadManager ui, IAsyncThreadManager async, IFrozenContext frozenCtx, IIdentityResolver idResolver)
            {
                Factory = f;
                UiThread = ui;
                AsyncThread = async;
                FrozenContext = frozenCtx;
                IdentityResolver = idResolver;
            }

            #region IViewModelDependencies Members

            public IViewModelFactory Factory
            {
                get;
                private set;
            }

            public IUiThreadManager UiThread
            {
                get;
                private set;
            }

            public IAsyncThreadManager AsyncThread
            {
                get;
                private set;
            }

            public IFrozenContext FrozenContext
            {
                get;
                private set;
            }

            public IIdentityResolver IdentityResolver
            {
                get;
                private set;
            }

            #endregion
        }

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder
                .Register<SynchronousThreadManager>(c => new SynchronousThreadManager())
                .As<IAsyncThreadManager>()
                .As<IUiThreadManager>();

            moduleBuilder
                .Register<ViewModelDependencies>(c => new ViewModelDependencies(
                    c.Resolve<IViewModelFactory>(), 
                    c.Resolve<IUiThreadManager>(), 
                    c.Resolve<IAsyncThreadManager>(), 
                    c.Resolve<IFrozenContext>(), 
                    c.Resolve<IIdentityResolver>()))
                .As<IViewModelDependencies>();

            moduleBuilder
                .Register<ThreadPrincipalResolver>(c=> new ThreadPrincipalResolver(c.Resolve<Func<IReadOnlyZetboxContext>>()))
                .As<IIdentityResolver>()
                .InstancePerLifetimeScope();

            moduleBuilder
                .Register<LoggingProblemReporter>(c => new LoggingProblemReporter())
                .As<IProblemReporter>()
                .SingleInstance();

            moduleBuilder
                .Register<DefaultCredentialsResolver>(c => new DefaultCredentialsResolver())
                .As<ICredentialsResolver>()
                .SingleInstance();
            
            moduleBuilder.RegisterViewModels(typeof(ClientModule).Assembly);
        }
    }
}
