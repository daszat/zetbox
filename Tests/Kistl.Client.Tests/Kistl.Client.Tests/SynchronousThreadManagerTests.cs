using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Kistl.Client.PresenterModel;
using System.Threading;

namespace Kistl.Client.Tests
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
