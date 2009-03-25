using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Client;
using Kistl.App.Test;

using NUnit.Framework;

namespace Kistl.DalProvider.ClientObjects.Tests.BinarySerializers
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
    [TestFixture(typeof(TestPhoneStruct__Implementation__))]
    public class should_work_with_ClientStructs<T>
        : Kistl.API.Tests.BinarySerializers.should_work_with_IStructs<T>
        where T : class, IStruct, new()
    {
    }
}
