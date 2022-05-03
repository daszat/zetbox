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
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Autofac;
    using NUnit.Framework;
    using Zetbox.API.AbstractConsumerTests;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client.Presentables;

    public class GuiExtensionTests : AbstractTestFixture
    {
        [Test]
        [Ignore("Test fails on linux due to trying to reflect on WPF classes which do not exist")]
        public async Task should_resolve_ViewDescriptor()
        {
            var ctx = GetContext();

            var vmd = ctx.GetQuery<ViewModelDescriptor>().First(v => v.ViewModelTypeRef == "Zetbox.Client.Presentables.DataObjectViewModel, Zetbox.Client");
            Assert.That(vmd, Is.Not.Null, "ViewModelDescriptor");

            var vd = await vmd.GetViewDescriptor(Toolkit.WPF);
            Assert.That(vd, Is.Not.Null, "ViewDescriptor");

            Assert.That(vd.ControlTypeRef, Is.EqualTo("Zetbox.Client.WPF.View.ObjectEditor.DataObjectEditor, Zetbox.Client.WPF"));
        }
    }
}
