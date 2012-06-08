using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Test;
using Zetbox.DalProvider.Base;
using Zetbox.DalProvider.NHibernate;
using NUnit.Framework;

namespace Zetbox.DalProvider.NHibernate.Tests.BinarySerializers
{
    public interface CompoundObjectMock : ICompoundObject
    {
    }

    public class CompoundObjectMockImpl : CompoundObjectDefaultImpl
    {
        public CompoundObjectMockImpl() : base(null) { }
        public override Type GetImplementedInterface()
        {
            return typeof(CompoundObjectMock);
        }

        public override Guid CompoundObjectID
        {
            get { throw new NotImplementedException(); }
        }
    }

    [TestFixture(typeof(CompoundObjectMockImpl))]
    [TestFixture(typeof(TestPhoneCompoundObjectNHibernateImpl))]
    public class should_work_with_NHibernateCompoundObjects<T>
        : Zetbox.API.AbstractConsumerTests.BinarySerializers.should_work_with_ICompoundObjects<T>
        where T : class, ICompoundObject, new()
    {
    }
}
