using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Test;
using Kistl.DalProvider.NHibernate;

using NUnit.Framework;

namespace Kistl.DalProvider.NHibernate.Tests.BinarySerializers
{
    public interface CompoundObjectMock : ICompoundObject
    {
    }

    public class CompoundObjectMockImpl : CompoundObjectNHibernateImpl
    {
        public CompoundObjectMockImpl() : base(null) { }
        public override Type GetImplementedInterface()
        {
            return typeof(CompoundObjectMock);
        }
    }

    [TestFixture(typeof(CompoundObjectMockImpl))]
    [TestFixture(typeof(TestPhoneCompoundObjectNHibernateImpl))]
    public class should_work_with_NHibernateCompoundObjects<T>
        : Kistl.API.AbstractConsumerTests.BinarySerializers.should_work_with_ICompoundObjects<T>
        where T : class, ICompoundObject, new()
    {
    }
}
