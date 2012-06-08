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
using System.Linq;
using System.Text;

using Zetbox.Client.Presentables;

using NUnit.Framework;

namespace Zetbox.Client.Mocks
{
    /// <summary>
    /// executes task synchronously, but checks that all Verify() asserts hold
    /// </summary>
    internal class ThreadManagerMock : IThreadManager
    {
        internal static void SetDefaultThread(ThreadManagerMock tm)
        {
            _currentThreadName = tm._threadName;
        }

        internal ThreadManagerMock(string threadName)
        {
            _threadName = threadName;
        }

        #region IThreadManager Members

        public void Verify() { Assert.AreEqual(_currentThreadName, _threadName, "should be on right thread"); }

        public void Queue(object lck, Action asyncTask)
        {
            if (_currentThreadName == _threadName)
                Assert.AreEqual(this, lck, "when queueing on the default thread, the default thread manager must be passed in");
            string oldThreadName = _currentThreadName;
            _currentThreadName = _threadName;
            try
            {
                asyncTask();
            }
            finally
            {
                _currentThreadName = oldThreadName;
            }
        }

        public void Queue(object lck, Action<object> asyncTask, object data)
        {
            if (_currentThreadName == _threadName)
                Assert.AreEqual(this, lck, "when queueing on the default thread, the default thread manager must be passed in");
            string oldThreadName = _currentThreadName;
            _currentThreadName = _threadName;
            try
            {
                asyncTask(data);
            }
            finally
            {
                _currentThreadName = oldThreadName;
            }
        }

        #endregion

        private string _threadName;
        private static string _currentThreadName;
    }
}
