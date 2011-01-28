
namespace Kistl.API.AbstractConsumerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API.Configuration;
    using Kistl.API.Utils;
    using NUnit.Framework;
    using System.ComponentModel;

    public abstract class AbstractTestFixture
    {
        protected ILifetimeScope scope;

        [SetUp]
        public virtual void SetUp()
        {
            scope = AbstractSetUpFixture.BeginLifetimeScope();
        }

        [TearDown]
        public virtual void TearDown()
        {
            if (scope != null)
                scope.Dispose();
        }

        protected IKistlContext GetContext()
        {
            return scope.Resolve<IKistlContext>();
        }

        protected void TestChangeNotification<TNOTIFIER>(TNOTIFIER notifier, string expectedPropertyName, Action doChange, Action changingAsserts, Action changedAsserts)
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
                    if (changingAsserts != null)
                        changingAsserts();
                    hasChanging = true;
                }
            });
            var changedHandler = new PropertyChangedEventHandler(delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == expectedPropertyName)
                {
                    Assert.That(hasChanged, Is.False, "changed event should be only triggered once");
                    Assert.That(sender, Is.SameAs(notifier), "sender should be the notifying object (OnChanged)");
                    if (changedAsserts != null)
                        changedAsserts(); 
                    hasChanged = true;
                }
            });

            Assert.DoesNotThrow(() =>
            {
                notifier.PropertyChanging += changingHandler;
                notifier.PropertyChanged += changedHandler;
            }, "Error when adding event handlers");

            doChange();

            Assert.That(hasChanging, Is.True, "should be notified about changing");
            Assert.That(hasChanged, Is.True, "should be notified about change");

            Assert.DoesNotThrow(() =>
            {
                notifier.PropertyChanging -= changingHandler;
                notifier.PropertyChanged -= changedHandler;
            }, "Error when removing event handlers");
        }
    }
}
