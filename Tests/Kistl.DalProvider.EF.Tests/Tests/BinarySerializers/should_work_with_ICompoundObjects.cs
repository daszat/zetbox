using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Test;
using Kistl.DalProvider.EF;

using NUnit.Framework;

namespace Kistl.DalProvider.EF.Tests.BinarySerializers
{
    public interface CompoundObjectMock : ICompoundObject
    {
    }

    public class CompoundObjectMock__Implementation__ : BaseServerCompoundObject_EntityFramework
    {
        public override InterfaceType GetInterfaceType()
        {
            return new InterfaceType(typeof(CompoundObjectMock));
        }
    }

    [TestFixture(typeof(CompoundObjectMock__Implementation__))]
    [TestFixture(typeof(TestPhoneCompoundObject__Implementation__))]
    public class should_work_with_EFCompoundObjects<T>
        : Kistl.API.AbstractConsumerTests.BinarySerializers.should_work_with_ICompoundObjects<T>
        where T : class, ICompoundObject, new()
    {
    }
}
