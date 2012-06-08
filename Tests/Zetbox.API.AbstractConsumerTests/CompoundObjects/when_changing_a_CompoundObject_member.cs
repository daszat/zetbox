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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

using Zetbox.API;
using Zetbox.App.Test;

using NUnit.Framework;

namespace Zetbox.API.AbstractConsumerTests.CompoundObjects
{
    public abstract class when_changing_a_CompoundObject_member
        : CompoundObjectFixture
    {
        private void TestChangeNotification<TNOTIFIER>(TNOTIFIER notifier, string expectedPropertyName)
            where TNOTIFIER : INotifyPropertyChanging, INotifyPropertyChanged
        {
            TestChangeNotification(notifier, expectedPropertyName,
                () => { obj.PhoneNumberOffice.Number = testNumber; },
                () => { Assert.That(obj.PhoneNumberOffice.Number, Is.Not.EqualTo(testNumber), "changing event should be triggered before the value has changed"); },
                () => { Assert.That(obj.PhoneNumberOffice.Number, Is.EqualTo(testNumber), "changed event should be triggered after the value has changed"); }
                );
        }

        [Test]
        public void should_notify_self()
        {
            TestChangeNotification(obj.PhoneNumberOffice, "Number");
        }

        [Test]
        public void should_notify_parent()
        {
            TestChangeNotification(obj, "PhoneNumberOffice.Number");
        }

        [Test]
        public void should_commit_changes()
        {
            obj.PhoneNumberOffice.Number = testNumber;

            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Modified), "parent object didn't notice change");
            Assert.That(ctx.SubmitChanges(), Is.EqualTo(1), "no changes were submitted");
            Assert.That(obj.PhoneNumberOffice.Number, Is.EqualTo(testNumber), "object lost changes on SubmitChanges()");
            using (var ctx2 = GetContext())
            {
                var obj2 = ctx2.Find<TestCustomObject>(obj.ID);
                Assert.That(obj2.PhoneNumberOffice.Number, Is.EqualTo(testNumber), "changes were not written to database");
            }
        }
    }
}
