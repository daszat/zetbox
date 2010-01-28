using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Test;

using NUnit.Framework;

namespace Kistl.API.AbstractConsumerTests.CompoundObjects
{

    public abstract class when_changing_a_compoundobject_member
        : CompoundObjectFixture
    {
        private void TestChangeNotification<TNOTIFIER>(TNOTIFIER notifier, string expectedPropertyName)
            where TNOTIFIER : INotifyPropertyChanging, INotifyPropertyChanged
        {
            bool hasChanged = false;
            bool hasChanging = false;

            var changingHandler = new PropertyChangingEventHandler(delegate(object sender, PropertyChangingEventArgs e)
            {
                if (e.PropertyName == expectedPropertyName)
                {
                    Assert.That(hasChanging, Is.False, "changing event should be only triggered once");
                    Assert.That(sender, Is.SameAs(notifier), "sender should be the notifying object (OnChanging)");
                    Assert.That(obj.PhoneNumberOffice.Number, Is.Not.EqualTo(testNumber), "changing event should be triggered before the value has changed");
                    hasChanging = true;
                }
            });
            var changedHandler = new PropertyChangedEventHandler(delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == expectedPropertyName)
                {
                    Assert.That(hasChanged, Is.False, "changed event should be only triggered once");
                    Assert.That(sender, Is.SameAs(notifier), "sender should be the notifying object (OnChanged)");
                    Assert.That(obj.PhoneNumberOffice.Number, Is.EqualTo(testNumber), "changed event should be triggered after the value has changed");
                    hasChanged = true;
                }
            });

            Assert.DoesNotThrow(() =>
            {
                notifier.PropertyChanging += changingHandler;
                notifier.PropertyChanged += changedHandler;
            });

            obj.PhoneNumberOffice.Number = testNumber;

            Assert.That(hasChanging, Is.True, "should be notified about changing");
            Assert.That(hasChanged, Is.True, "should be notified about change");

            Assert.DoesNotThrow(() =>
            {
                notifier.PropertyChanging -= changingHandler;
                notifier.PropertyChanged -= changedHandler;
            });
        }

        [Test]
        public void should_notify_self()
        {
            TestChangeNotification(obj.PhoneNumberOffice, "Number");
        }

        [Test]
        public void should_notify_parent()
        {
            TestChangeNotification(obj, "PhoneNumberOffice");
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
