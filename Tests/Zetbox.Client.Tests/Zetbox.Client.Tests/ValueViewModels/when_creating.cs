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

namespace Zetbox.Client.Tests.ValueViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class when_creating
        : ViewModelTestFixture
    {
        [Test]
        public void should_have_B_UV_state()
        {
            Assert.That(obj.GetCurrentState(), Is.EqualTo(ValueViewModelState.Blurred_UnmodifiedValue));
        }

        [Test]
        public void should_remember_ValueModel()
        {
            Assert.That(obj.ValueModel, Is.SameAs(valueModelMock.Object));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void should_reflect_AllowNullInput(bool allow)
        {
            valueModelMock.SetupGet<bool>(o => o.AllowNullInput).Returns(allow).Verifiable();

            Assert.That(obj.AllowNullInput, Is.EqualTo(allow));

            valueModelMock.Verify();
        }
    }
}
