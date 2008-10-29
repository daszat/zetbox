using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NMock2;
using Kistl.API;
using Kistl.App.GUI;
using Kistl.Client.Mocks;

namespace Kistl.GUI.Tests
{
    [TestFixture]
    public class PresenterTests
    {
        class PresenterMock : Presenter<IValueControl<int>>
        {

            Action init;
            Action disposeManaged;
            Action disposeUnmanaged;

            public PresenterMock(Action initAction, Action disposeManagedAction, Action disposeUnmanagedAction)
            {
                this.init = initAction ?? new Action(() => Assert.Fail());
                this.disposeManaged = disposeManagedAction ?? delegate { };
                this.disposeUnmanaged = disposeUnmanagedAction ?? delegate { };
            }

            protected override void InitializeComponent()
            {
                init();
            }

            protected override void DisposeManagedResources()
            {
                base.DisposeManagedResources();
                disposeManaged();
            }

            protected override void DisposeNativeResources()
            {
                base.DisposeNativeResources();
                disposeUnmanaged();
            }
        }

        // TODO: refactor using something like XunitAssert.Throws<> from 
        // http://jamesnewkirk.typepad.com/posts/2008/06/replacing-expec.html
        // this would allow to check that the Presenter doesn't call 
        // InitializeComponent() when given invalid arguments
        [Test]
        [ExpectedException(ExceptionType = typeof(ArgumentNullException))]
        public void InitializeFirstArgNull()
        {
            using (Mockery m = new Mockery())
            {
                IValueControl<int> ibc = m.NewMock<IValueControl<int>>();
                Visual v = m.NewMock<Visual>();

                using (var presenter = new PresenterMock(null, null, null))
                {
                    presenter.InitializeComponent(null, v, ibc);
                }
            }
        }
        [Test]
        [ExpectedException(ExceptionType = typeof(ArgumentNullException))]
        public void InitializeSecondArgNull()
        {
            using (Mockery m = new Mockery())
            {
                TestObject ido = MockFactory.CreateTestObject(m);
                IValueControl<int> ibc = m.NewMock<IValueControl<int>>();

                using (var presenter = new PresenterMock(null, null, null))
                {
                    presenter.InitializeComponent(ido, null, ibc);
                }
            }
        }
        [Test]
        [ExpectedException(ExceptionType = typeof(ArgumentNullException))]
        public void InitializeThirdArgNull()
        {
            using (Mockery m = new Mockery())
            {
                TestObject ido = MockFactory.CreateTestObject(m);
                Visual v = m.NewMock<Visual>();

                using (var presenter = new PresenterMock(null, null, null))
                {
                    presenter.InitializeComponent(ido, v, null);
                }
            }
        }

        [Test]
        public void InitializeComponent()
        {
            using (Mockery m = new Mockery())
            {
                TestObject ido = MockFactory.CreateTestObject(m);
                Visual v = m.NewMock<Visual>();
                IValueControl<int> ibc = m.NewMock<IValueControl<int>>();

                int initCalls = 0;
                using (var presenter = new PresenterMock(() => initCalls++, null, null))
                {
                    presenter.InitializeComponent(ido, v, ibc);

                    Assert.AreEqual(1, initCalls, "InitializeComponent should be called exactly once");
                    Assert.AreSame(ido, presenter.Object);
                    Assert.AreSame(v, presenter.Preferences);
                    Assert.AreSame(ibc, presenter.Control);
                }
            }
        }

        [Test]
        [ExpectedException(ExceptionType = typeof(InvalidOperationException))]
        public void InitializeComponentOnlyOnce()
        {
            using (Mockery m = new Mockery())
            {
                TestObject ido = MockFactory.CreateTestObject(m);
                Visual v = m.NewMock<Visual>();
                IValueControl<int> ibc = m.NewMock<IValueControl<int>>();

                using (var presenter = new PresenterMock(delegate { }, null, null))
                {
                    presenter.InitializeComponent(ido, v, ibc);
                    presenter.InitializeComponent(ido, v, ibc);
                }
            }
        }

        [Test]
        [ExpectedException(ExceptionType = typeof(InvalidCastException))]
        public void IPresenter_InitializeComponentFailFast()
        {
            using (Mockery m = new Mockery())
            {
                TestObject ido = MockFactory.CreateTestObject(m);
                Visual v = m.NewMock<Visual>();
                IValueControl<string> ivc = m.NewMock<IValueControl<string>>();

                using (var presenter = new PresenterMock(null, null, null))
                {
                    ((IPresenter)presenter).InitializeComponent(ido, v, ivc);
                }
            }
        }

        [Test]
        public void IPresenter_InitializeComponent()
        {
            using (Mockery m = new Mockery())
            {
                TestObject ido = MockFactory.CreateTestObject(m);
                Visual v = m.NewMock<Visual>();
                IValueControl<int> ivc = m.NewMock<IValueControl<int>>();

                int initCalls = 0;
                using (var presenter = new PresenterMock(() => initCalls++, null, null))
                {
                    ((IPresenter)presenter).InitializeComponent(ido, v, ivc);
                }
            }
        }

        [Test]
        public void IDisposable_Dispose()
        {
            using (Mockery m = new Mockery())
            {
                TestObject ido = MockFactory.CreateTestObject(m);
                Visual v = m.NewMock<Visual>();
                IValueControl<int> ivc = m.NewMock<IValueControl<int>>();

                int managedCalls = 0;
                int unmanagedCalls = 0;
                var presenter = new PresenterMock(null, () => managedCalls++, () => unmanagedCalls++);

                presenter.Dispose();

                Assert.AreEqual(1, managedCalls, "DisposeManagedResources should have been called exactly once");
                Assert.AreEqual(1, unmanagedCalls, "DisposeUnmanagedResources should have been called exactly once");
            }
        }

        [Test]
        public void IDisposable_Finalizer()
        {
            using (Mockery m = new Mockery())
            {
                TestObject ido = MockFactory.CreateTestObject(m);
                Visual v = m.NewMock<Visual>();
                IValueControl<int> ivc = m.NewMock<IValueControl<int>>();

                int unmanagedCalls = 0;
                var presenter = new PresenterMock(null, 
                    () => Assert.Fail("DisposeManagedResources MUST not be called from finalizer"), 
                    () => unmanagedCalls++);

                presenter = null;
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();

                Assert.AreEqual(1, unmanagedCalls, "DisposeUnmanagedResources should have been called exactly once");
            }
        }
    }
}
