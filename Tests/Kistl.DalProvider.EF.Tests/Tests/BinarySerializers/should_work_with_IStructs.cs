using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Test;
using Kistl.DALProvider.EF;

using NUnit.Framework;

namespace Kistl.DalProvider.EF.Tests.BinarySerializers
{
    public interface StructMock : IStruct
    {
    }

    public class StructMock__Implementation__ : BaseServerStructObject_EntityFramework
    {
        public override InterfaceType GetInterfaceType()
        {
            return new InterfaceType(typeof(StructMock));
        }
    }

    [TestFixture(typeof(StructMock__Implementation__))]
    [TestFixture(typeof(TestPhoneStruct__Implementation__))]
    public class should_work_with_EFStructs<T>
        : Kistl.API.AbstractConsumerTests.BinarySerializers.should_work_with_IStructs<T>
        where T : class, IStruct, new()
    {
    }
}
