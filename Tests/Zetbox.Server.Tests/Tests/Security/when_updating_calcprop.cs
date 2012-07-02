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

namespace Zetbox.Server.Tests.Security
{
    using Autofac;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using Zetbox.App.Test;
    using Zetbox.App.Base;
    using Zetbox.API;
    using Zetbox.API.Common;

    public abstract class when_updating_calcprop : AbstractServerTestFixture
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

        protected virtual void Reload()
        {
            ctx = GetContext();
            identity1 = ctx.Find<Identity>(identity1.ID);
            identity2 = ctx.Find<Identity>(identity2.ID);

            parent = ctx.Find<SecurityTestParent>(parent.ID);
            child1 = ctx.Find<SecurityTestChild>(child1.ID);
            child2 = ctx.Find<SecurityTestChild>(child2.ID);
        }

        [Test]
        public void should_have_correct_names()
        {
            Assert.That(parent.Name, Is.EqualTo("MyParent"));
            Assert.That(child1.ParentName, Is.EqualTo("MyParent"));
            Assert.That(child2.ParentName, Is.EqualTo("MyParent")); // Big diff. to client impl. -> on the server, there are always read rights
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

        public class in_same_context : when_updating_calcprop
        {
            [Test]
            public void should_have_full_rights()
            {
                Assert.That(parent.CurrentAccessRights, Is.EqualTo(API.AccessRights.Full));
                Assert.That(child1.CurrentAccessRights, Is.EqualTo(API.AccessRights.Full));
                Assert.That(child2.CurrentAccessRights, Is.EqualTo(API.AccessRights.Full));
            }
        }

        public class when_reloading : when_updating_calcprop
        {
            public override void SetUp()
            {
                base.SetUp();
                base.Reload();
            }

            [Test]
            public void should_have_correct_rights()
            {
                Assert.That(parent.CurrentAccessRights, Is.EqualTo(API.AccessRights.Change | API.AccessRights.Delete));
                Assert.That(child1.CurrentAccessRights, Is.EqualTo(API.AccessRights.Full));
                Assert.That(child2.CurrentAccessRights, Is.EqualTo(API.AccessRights.None));
            }

            [Test]
            public void should_read_with_no_rights()
            {
                Assert.That(child2.CurrentAccessRights, Is.EqualTo(API.AccessRights.None));
                Assert.That(child2.ParentName, Is.EqualTo("MyParent")); // Big diff. to client impl. -> on the server, there are always read rights
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
        }
    }
}
