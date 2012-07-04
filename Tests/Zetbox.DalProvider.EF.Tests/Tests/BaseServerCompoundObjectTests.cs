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

namespace Zetbox.DalProvider.Ef.Tests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Tests.Skeletons;
    using Zetbox.App.Test;
    using Zetbox.DalProvider.Ef;
    using Zetbox.DalProvider.Ef.Mocks;
    using NUnit.Framework;

    [TestFixture]
    public class BaseServerCompoundObjectTests
        : IStreamableTests<TestPhoneCompoundObjectEfImpl>
    {
        TestCustomObjectEfImpl parent;
        TestPhoneCompoundObjectEfImpl attachedObj;

        [SetUp]
        public void SetUpTestObject()
        {
            obj = new TestPhoneCompoundObjectEfImpl(null, null) { AreaCode = "ABC", Number = "123456" };

            parent = new TestCustomObjectEfImpl(null);
            attachedObj = (TestPhoneCompoundObjectEfImpl)parent.PhoneNumberOffice;
            attachedObj.AreaCode = "attachedAreaCode";
            attachedObj.Number = "attachedNumber";
        }

        [Test]
        public void should_roudtrip_members_correctly()
        {
            var result = this.SerializationRoundtrip(obj);

            Assert.That(result.AreaCode, Is.EqualTo(obj.AreaCode));
            Assert.That(result.Number, Is.EqualTo(obj.Number));
        }
    }
}
