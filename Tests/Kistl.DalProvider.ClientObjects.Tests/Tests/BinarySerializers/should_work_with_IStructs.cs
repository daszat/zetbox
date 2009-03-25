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

    [TestFixture(typeof(TestPhoneStruct__Implementation__))]
    public class should_work_with_ClientStructs<T>
        : Kistl.API.AbstractConsumerTests.BinarySerializers.should_work_with_IStructs<T>
        where T : class, IStruct, new()
    {
    }

}
