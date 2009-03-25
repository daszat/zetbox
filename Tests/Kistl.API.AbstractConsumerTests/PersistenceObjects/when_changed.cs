using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Kistl.API;

using NUnit.Framework;

namespace Kistl.API.AbstractConsumerTests.PersistenceObjects
{
    
    public abstract class when_changed
        : ObjectLoadFixture
    {

        Random rnd = new Random();
        string testName;

        [SetUp]
        public void InitTestValue()
        {
            testName = rnd.NextDouble().ToString(CultureInfo.InvariantCulture);
            if (testName == obj.PersonName)
            {
                testName += rnd.NextDouble().ToString(CultureInfo.InvariantCulture);
            }
        }


        [Test]
        public void should_be_modified()
        {
            Assume.That(obj.PersonName, Is.Not.EqualTo(testName));
            obj.PersonName = testName;
            Assert.That(obj.ObjectState, Is.EqualTo(DataObjectState.Modified), "object didn't notice change to property");
        }

    }

}
