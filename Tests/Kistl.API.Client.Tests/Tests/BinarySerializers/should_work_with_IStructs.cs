using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.API;

using NUnit.Framework;

namespace Kistl.API.Client.Tests.BinarySerializers
{
    public interface StructMock : IStruct
    {
    }

    public class StructMock__Implementation__ : BaseClientStructObject
    {
        public override InterfaceType GetInterfaceType()
        {
            return new InterfaceType(typeof(StructMock));
        }
    }

    [TestFixture(typeof(StructMock__Implementation__))]
    public class should_work_with_BaseClientStructObjects<T>
        : Kistl.API.AbstractConsumerTests.BinarySerializers.should_work_with_IStructs<T>
        where T : class, IStruct, new()
    {
    }
}
