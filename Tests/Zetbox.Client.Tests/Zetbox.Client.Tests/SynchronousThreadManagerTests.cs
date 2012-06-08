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
using NUnit.Framework;
using Zetbox.Client.Presentables;
using System.Threading;

namespace Zetbox.Client.Tests
{
    [TestFixture]
    public class SynchronousThreadManagerTests
    {
        [SetUp]
        public void SetUp()
        {
            _m = new SynchronousThreadManager();
        }

        [Test]
        public void TestVerifyInThreadPool()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object o)
            {
                try
                {
                    _m.Verify();
                }
                catch
                {
                    Assert.Fail("Unexpected Exception");
                }
            }));
        }

        [Test]
        public void TestVerify()
        {
            _m.Verify();
        }

        [Test]
        public void TestQueue()
        {
            bool run = false;
            _m.Queue(null, () => { Thread.Sleep(100); run = true; });
            Assert.IsTrue(run, "Task didn't run");
        }
        
        [Test]
        public void TestQueueWithParameter()
        {
            bool run = false;
            string param = "test";
            _m.Queue(null, passed => { Thread.Sleep(100); run = true; Assert.AreEqual(param, passed, "Wrong data passed into action"); }, param);
            Assert.IsTrue(run, "Task didn't run");
        }

        private SynchronousThreadManager _m;
    }
}
