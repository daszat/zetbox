using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Test;

using NUnit.Framework;

namespace Kistl.DalProvider.EF.Tests.BinarySerializers
{
    [TestFixture(typeof(BaseServerStructObject))]
    [TestFixture(typeof(TestPhoneStruct__Implementation__))]
    public class should_work_with_EFStructs<T>
        : Kistl.API.Tests.BinarySerializers.should_work_with_IStructs<T>
        where T : class, IStruct, new()
    {
    }
}
