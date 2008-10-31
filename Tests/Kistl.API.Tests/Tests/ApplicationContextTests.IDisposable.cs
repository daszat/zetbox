using System;

using Kistl.API.Configuration;

using NUnit.Framework;

// This is a ApplicationContext to test the recommended default IDisposable implementation

namespace Kistl.API.Tests
{
    public partial class ApplicationContextTests
    {

        partial class ApplicationContextMock : ApplicationContext
        {

            Action disposeManaged;
            Action disposeUnmanaged;

            public ApplicationContextMock(Action disposeManagedAction, Action disposeUnmanagedAction)
                : base(HostType.Server, KistlConfig.FromFile(""))
            {
                this.disposeManaged = disposeManagedAction ?? delegate { };
                this.disposeUnmanaged = disposeUnmanagedAction ?? delegate { };
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
            var mock = new ApplicationContextMock(() => managedCalls++, () => unmanagedCalls++);

            mock.Dispose();

            Assert.AreEqual(1, managedCalls, "DisposeManagedResources should have been called exactly once");
            Assert.AreEqual(1, unmanagedCalls, "DisposeUnmanagedResources should have been called exactly once");
        }

        [Test]
        public void IDisposable_Finalizer()
        {
            int unmanagedCalls = 0;
            var mock = new ApplicationContextMock(
                () => Assert.Fail("DisposeManagedResources MUST not be called from finalizer"),
                () => unmanagedCalls++);

            // clear ApplicationContext.Current too!
            mock = new ApplicationContextMock(null,null); 

            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();

            Assert.AreEqual(1, unmanagedCalls, "DisposeUnmanagedResources should have been called exactly once");
        }
    }
}