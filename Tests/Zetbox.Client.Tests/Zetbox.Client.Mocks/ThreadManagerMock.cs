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
