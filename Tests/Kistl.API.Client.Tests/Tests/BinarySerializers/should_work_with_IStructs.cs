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
    [TestFixture(typeof(BaseClientStructObject))]
    public class should_work_with_EFStructs<T>
        : Kistl.API.Tests.BinarySerializers.should_work_with_IStructs<T>
        where T : class, IStruct, new()
    {
    }
}
