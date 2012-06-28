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

namespace Zetbox.API.AbstractConsumerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using NUnit.Framework;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.App.GUI;
    using Zetbox.Client.Presentables.GUI;
    using Zetbox.Client.Presentables.ZetboxBase;
    
    public abstract class AbstractUITest : AbstractTestFixture
    {
        protected MockedViewModelFactory mdlFactory;
        protected IFrozenContext frozenContext;
        protected Func<IZetboxContext> ctxFactory;
        protected Func<ClientIsolationLevel, IZetboxContext> ctxClientFactory;

        public override void SetUp()
        {
            base.SetUp();
            mdlFactory = scope.Resolve<MockedViewModelFactory>();
            frozenContext = scope.Resolve<IFrozenContext>();
            ctxFactory = scope.Resolve<Func<IZetboxContext>>();
            ctxClientFactory = scope.Resolve<Func<ClientIsolationLevel, IZetboxContext>>();
        }

        protected NavigatorViewModel NavigateTo(IZetboxContext ctx, params Guid[] path)
        {
            var app = frozenContext.FindPersistenceObject<Application>(new Guid("6be0ba52-4589-48f1-832f-6cd463ba319a"));
            var appMdl = mdlFactory.CreateViewModel<ApplicationViewModel.Factory>().Invoke(ctx, null, app);
            var navigator = mdlFactory.CreateViewModel<NavigatorViewModel.Factory>().Invoke(ctx, null, appMdl.RootScreen);

            foreach (var screenGuid in path)
            {
                var screen = navigator.CurrentScreen.Children.Single(s => s.ExportGuid == screenGuid);
                navigator.NavigateTo(screen);
                Assert.That(navigator.CurrentScreen, Is.SameAs(screen));
            }
            return navigator;
        }

        protected IZetboxContext GetClientContext()
        {
            return ctxClientFactory(ClientIsolationLevel.MergeServerData);
        }
    }
}
