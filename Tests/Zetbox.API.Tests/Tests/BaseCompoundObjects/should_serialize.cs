
namespace Zetbox.API.Tests.BaseCompoundObjects
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API.AbstractConsumerTests;
    using Zetbox.API.Mocks;
    using NUnit.Framework;
    
    [TestFixture]
    public class should_serialize : SerializerTestFixture
    {
        TestCompoundObjectImpl test;

        public override void SetUp()
        {
            base.SetUp();
            test = new TestCompoundObjectImpl();
        }

        /// <summary>
        /// Rewinds all streams to their start
        /// </summary>
        private void RewindStreams()
        {
            ms.Seek(0, SeekOrigin.Begin);
        }

        [Test]
        public void without_exceptions()
        {
            test.ToStream(sw, null, false);
            RewindStreams();

            Assert.DoesNotThrow(() =>
            {
                test.FromStream(sr);
            });
        }

        // just test the mock
        [Test]
        public void and_keep_TestProperty()
        {
            const string val = "muh";
            test.TestProperty = val;
            test.ToStream(sw, null, false);
            test.TestProperty = null;
            
            RewindStreams();
            test.FromStream(sr);

            Assert.That(test.TestProperty, Is.EqualTo(val), "To/FromStream of the mock didn't transport TestProperty");
        }
    }
}
