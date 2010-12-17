using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Test;
using Kistl.DalProvider.Ef;

using NUnit.Framework;

namespace Kistl.DalProvider.Ef.Tests.BinarySerializers
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
    }

    [TestFixture(typeof(CompoundObjectMockImpl))]
    [TestFixture(typeof(TestPhoneCompoundObjectEfImpl))]
    public class should_work_with_EFCompoundObjects<T>
        : Kistl.API.AbstractConsumerTests.BinarySerializers.should_work_with_ICompoundObjects<T>
        where T : class, ICompoundObject, new()
    {
    }
}
