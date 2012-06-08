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
using NUnit.Framework;

// This is a template to test the recommended default IDisposable implementation
// Use this as "mixin" for the normal TestFicture by replacing NAMESPACE,
// TEMPLATE and IDISPOSABLE_IMPLEMENTOR as neccessary

namespace NAMESPACE
{
    public partial class TEMPLATETests
    {

        partial class TEMPLATEMock : IDISPOSABLE_IMPLEMENTOR
        {
            Action disposeManaged;
            Action disposeUnmanaged;

            public TEMPLATEMock(Action disposeManagedAction, Action disposeUnmanagedAction)
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
            var presenter = new TEMPLATEMock(null, () => managedCalls++, () => unmanagedCalls++);

            presenter.Dispose();

            Assert.AreEqual(1, managedCalls, "DisposeManagedResources should have been called exactly once");
            Assert.AreEqual(1, unmanagedCalls, "DisposeUnmanagedResources should have been called exactly once");
        }

        [Test]
        public void IDisposable_Finalizer()
        {
            int unmanagedCalls = 0;
            var presenter = new TEMPLATEMock(null,
                () => Assert.Fail("DisposeManagedResources MUST not be called from finalizer"),
                () => unmanagedCalls++);

            presenter = null;
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();

            Assert.AreEqual(1, unmanagedCalls, "DisposeUnmanagedResources should have been called exactly once");
        }
    }
}