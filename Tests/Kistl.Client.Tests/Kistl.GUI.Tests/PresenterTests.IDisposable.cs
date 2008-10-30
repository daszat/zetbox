using System;
using NUnit.Framework;

// This is a Presenter to test the recommended default IDisposable implementation
// Use this as "mixin" for the normal TestFicture by replacing NAMESPACE, Presenter and IDISPOSABLE_IMPLEMENTOR as neccessary

namespace Kistl.GUI.Tests
{
    public partial class PresenterTests
    {

        partial class PresenterMock : Presenter<IValueControl<int>>
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

        [Test]
        public void IDisposable_Dispose()
        {
            int managedCalls = 0;
            int unmanagedCalls = 0;
            var presenter = new PresenterMock(null, () => managedCalls++, () => unmanagedCalls++);

            presenter.Dispose();

            Assert.AreEqual(1, managedCalls, "DisposeManagedResources should have been called exactly once");
            Assert.AreEqual(1, unmanagedCalls, "DisposeUnmanagedResources should have been called exactly once");
        }

        [Test]
        public void IDisposable_Finalizer()
        {
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