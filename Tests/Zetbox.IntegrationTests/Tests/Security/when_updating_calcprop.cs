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

namespace Zetbox.IntegrationTests.Security
{
    using Autofac;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API.AbstractConsumerTests;
    using Zetbox.App.Base;
    using Zetbox.API;
    using Zetbox.App.Test;
    using NUnit.Framework;
    using Zetbox.API.Common;
    using Zetbox.Client.Presentables;

    public class when_updating_calcprop : AbstractUITest
    {
        Identity identity1;
        Identity identity2;
        IZetboxContext ctx;

        SecurityTestParent parent;
        SecurityTestChild child1;
        SecurityTestChild child2;

        public override void SetUp()
        {
            base.SetUp();

            ctx = GetContext();
            var idResolver = scope.Resolve<IIdentityResolver>();


            identity1 = ctx.Find<Identity>(idResolver.GetCurrent().ID);
            identity2 = ctx.GetQuery<Identity>().Where(i => i.ID != identity1.ID).First();

            parent = ctx.Create<SecurityTestParent>();
            parent.Name = "MyParent";

            child1 = ctx.Create<SecurityTestChild>();
            child1.Name = "Child1";
            child1.Identity = identity1;
            child1.Parent = parent;

            child2 = ctx.Create<SecurityTestChild>();
            child2.Name = "Child2";
            child2.Identity = identity2;
            child2.Parent = parent;

            ctx.SubmitChanges();
        }

        [Test]
        public void parent_should_change_name()
        {
            parent.Name = "MyParentChanged";
            Assert.That(parent.Name, Is.EqualTo("MyParentChanged"));
            Assert.That(child1.ParentName, Is.EqualTo("MyParentChanged"));
            //Assert.That(child2.CurrentAccessRights, Is.EqualTo(API.AccessRights.None));

            var vmdl = mdlFactory.CreateViewModel<DataObjectViewModel.Factory>().Invoke(ctx, null, child2);
            Assert.That(vmdl.Error, Is.Empty);
            ctx.SubmitChanges();
        }
    }
}
