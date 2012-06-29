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

    public abstract class when_updating_calcprop : AbstractUITest
    {
        protected Identity identity1;
        protected Identity identity2;
        protected IZetboxContext ctx;

        protected SecurityTestParent parent;
        protected SecurityTestChild child1;
        protected SecurityTestChild child2;

        public override void SetUp()
        {
            base.SetUp();

            ctx = GetContext();
            var idResolver = scope.Resolve<IIdentityResolver>();

            var currentIdentity = idResolver.GetCurrent();

            Assert.That(currentIdentity, Is.Not.Null, "No current identity found - try syncidentities or setup the current identity correctly");

            identity1 = ctx.Find<Identity>(currentIdentity.ID);
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
        public virtual void viemodel_should_report_no_error()
        {
            var ws = mdlFactory.CreateViewModel<Zetbox.Client.Presentables.ObjectEditor.WorkspaceViewModel.Factory>().Invoke(ctx, null);
            var parentVmdl = mdlFactory.CreateViewModel<DataObjectViewModel.Factory>().Invoke(ctx, null, parent);
            var child1Vmdl = mdlFactory.CreateViewModel<DataObjectViewModel.Factory>().Invoke(ctx, null, child1);
            var child2Vmdl = mdlFactory.CreateViewModel<DataObjectViewModel.Factory>().Invoke(ctx, null, child2);
            ws.ShowModel(parentVmdl);
            ws.ShowModel(child1Vmdl);
            ws.ShowModel(child2Vmdl);

            parent.Name = "MyParentChanged";
            ws.UpdateErrors();

            Assert.That(string.IsNullOrEmpty(child2Vmdl.Error), Is.True, child2Vmdl.Error);
            Assert.That(ws.CanSave(), Is.True, string.Join("\n", ws.GetErrors().Select(e => e.Error).ToArray()));
            Assert.That(ws.GetErrors(), Is.Empty);
        }

        public class in_same_context : when_updating_calcprop
        {
            [Test]
            public void should_have_full_rights()
            {
                Assert.That(parent.CurrentAccessRights, Is.EqualTo(API.AccessRights.Full));
                Assert.That(child1.CurrentAccessRights, Is.EqualTo(API.AccessRights.Full));
                Assert.That(child2.CurrentAccessRights, Is.EqualTo(API.AccessRights.Full));
            }

            [Test]
            public void parent_should_change_name()
            {
                parent.Name = "MyParentChanged";
                Assert.That(parent.Name, Is.EqualTo("MyParentChanged"));
                Assert.That(child1.ParentName, Is.EqualTo("MyParentChanged"));
                Assert.That(child2.ParentName, Is.EqualTo("MyParentChanged"));
            }

            [Test]
            public void parent_should_change_name_and_remember()
            {
                parent.Name = "MyParentChanged";
                ctx.SubmitChanges();

                Assert.That(parent.Name, Is.EqualTo("MyParentChanged"));
                Assert.That(child1.ParentName, Is.EqualTo("MyParentChanged"));
                Assert.That(child2.ParentName, Is.EqualTo("MyParentChanged"));
            }
        }

        public class when_reloading : when_updating_calcprop
        {
            public override void SetUp()
            {
                base.SetUp();

                ctx = GetContext();
                identity1 = ctx.Find<Identity>(identity1.ID);
                identity2 = ctx.Find<Identity>(identity2.ID);

                parent = ctx.Find<SecurityTestParent>(parent.ID);
                child1 = ctx.Find<SecurityTestChild>(child1.ID);
                child2 = ctx.Find<SecurityTestChild>(child2.ID);
            }

            [Test]
            public void should_have_correct_rights()
            {
                Assert.That(parent.CurrentAccessRights, Is.EqualTo(API.AccessRights.Change | API.AccessRights.Delete));
                Assert.That(child1.CurrentAccessRights, Is.EqualTo(API.AccessRights.Full));
                Assert.That(child2.CurrentAccessRights, Is.EqualTo(API.AccessRights.None));
            }

            [Test]
            public void should_have_correct_rights_navigator()
            {
                bool foundFull = false;
                bool foundNone = false;

                foreach (var child in parent.Children)
                {
                    if (child.CurrentAccessRights == API.AccessRights.Full)
                        foundFull = true;
                    if (child.CurrentAccessRights == API.AccessRights.None)
                        foundNone = true;
                }

                Assert.That(foundFull, Is.True, "Did not found a child object with full rights");
                Assert.That(foundNone, Is.True, "Did not found a child object with none rights");
            }

            [Test]
            public void parent_should_change_name()
            {
                parent.Name = "MyParentChanged";
                Assert.That(parent.Name, Is.EqualTo("MyParentChanged"));
                Assert.That(child1.ParentName, Is.EqualTo("MyParentChanged"));
            }

            [Test]
            public void parent_should_change_name_and_remember()
            {
                parent.Name = "MyParentChanged";
                ctx.SubmitChanges();

                Assert.That(parent.Name, Is.EqualTo("MyParentChanged"));
                Assert.That(child1.ParentName, Is.EqualTo("MyParentChanged"));
            }
        }
    }
}
