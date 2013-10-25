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

namespace Zetbox.IntegrationTests.Client
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Autofac;
    using NUnit.Framework;
    using Zetbox.API;
    using Zetbox.API.AbstractConsumerTests;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.App.Test;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ZetboxBase;

    public class InstanceListTests : ViewModelFactoryTests
    {
        protected InstanceListViewModel ilvm;
        protected IZetboxContext ctx;
        protected One_to_N_relations_N obj;

        public override void SetUp()
        {
            base.SetUp();
            ctx = scope.Resolve<IZetboxContext>();
            ilvm = vmf.CreateViewModel<InstanceListViewModel.Factory>().Invoke(ctx, null, (ObjectClass)NamedObjects.Base.Classes.Zetbox.App.Test.One_to_N_relations_N.Find(ctx), null);
            obj = ctx.Create<One_to_N_relations_N>();
            ctx.SubmitChanges();
        }

        public override void TearDown()
        {
            obj = null;
            ilvm = null;
            ctx = null;
            base.TearDown();
        }

        [Test]
        public void should_sort_strings_across_navigators_with_nulls()
        {
            Assert.That(obj.OneSide, Is.Null, "should be null to create fixture");

            ilvm.Sort("it.OneSide == null ? null : it.OneSide.Name", System.ComponentModel.ListSortDirection.Ascending);
            Assert.That(ilvm.Instances.Select(vm => vm.Object).ToArray(), Has.Member(obj));
        }

        [Test]
        public void should_sort_ints_across_navigators_with_nulls()
        {
            Assert.That(obj.OneSide, Is.Null, "should be null to create fixture");
            ilvm.Sort(string.Format(CultureInfo.InvariantCulture, "it.OneSide == null ? {0} : it.OneSide.ID", int.MinValue), System.ComponentModel.ListSortDirection.Ascending);
            Assert.That(ilvm.Instances.Select(vm => vm.Object).ToArray(), Has.Member(obj));
        }
    }
}
