using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Test;
using Zetbox.DalProvider.Ef;

using NUnit.Framework;

namespace Zetbox.DalProvider.Ef.Tests.BinarySerializers
{
    public interface CompoundObjectMock : ICompoundObject
    {
    }

    public class CompoundObjectMockImpl : BaseServerCompoundObject_EntityFramework
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
    [TestFixture(typeof(TestPhoneCompoundObjectEfImpl))]
    public class should_work_with_EFCompoundObjects<T>
        : Zetbox.API.AbstractConsumerTests.BinarySerializers.should_work_with_ICompoundObjects<T>
        where T : class, ICompoundObject, new()
    {
    }
}
