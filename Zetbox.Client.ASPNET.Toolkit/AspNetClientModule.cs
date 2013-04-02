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

namespace Zetbox.Client.ASPNET
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel;
    using Autofac;
    using Autofac.Integration.Mvc;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.API.Common;
    using Zetbox.API.Common.GUI;
    using Zetbox.API.Client;

    public class ZetboxContextHttpScope
    {
        public ZetboxContextHttpScope(IZetboxContext ctx)
        {
            Context = ctx;
        }

        public IZetboxContext Context { get; private set; }
    }

    [Description("The ASP.NET MVC Client Module")]
    public class AspNetClientModule : Module
    {
        #region ViewModelDependencies
        private class ViewModelDependencies : IViewModelDependencies
        {
            public ViewModelDependencies(IViewModelFactory f, IFrozenContext frozenCtx, IIdentityResolver idResolver, IIconConverter iconConverter)
            {
                Factory = f;
                FrozenContext = frozenCtx;
                IdentityResolver = idResolver;
                IconConverter = iconConverter;
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

            public IIdentityResolver IdentityResolver
            {
                get;
                private set;
            }

            #endregion


            public IIconConverter IconConverter
            {
                get;
                private set;
            }
        }
        #endregion

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder.RegisterModule<Zetbox.API.Common.ApiCommonModule>();
            moduleBuilder.RegisterModule<Zetbox.API.Client.ClientApiModule>();

            moduleBuilder
                .Register<ViewModelDependencies>(c => new ViewModelDependencies(
                    c.Resolve<IViewModelFactory>(),
                    c.Resolve<IFrozenContext>(),
                    c.Resolve<IIdentityResolver>(),
                    c.Resolve<IIconConverter>()))
                .As<IViewModelDependencies>();

            moduleBuilder
                .Register<LoggingProblemReporter>(c => new LoggingProblemReporter())
                .As<IProblemReporter>()
                .SingleInstance();

            moduleBuilder
                .Register<ZetboxContextHttpScope>(c => new ZetboxContextHttpScope(c.Resolve<IZetboxContext>()))
                .InstancePerHttpRequest();

            moduleBuilder
                .RegisterType<AspNetViewModelFactory>()
                .As<IViewModelFactory>()
                .As<IToolkit>()
                .SingleInstance();

            moduleBuilder
                .RegisterType<Zetbox.Client.GUI.DialogCreator>()
                .AsSelf()
                .InstancePerDependency();

            moduleBuilder.RegisterViewModels(typeof(ClientModule).Assembly);
        }
    }
}
