using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.API.Mocks;

using NUnit.Framework;

namespace Kistl.API.Tests.BinarySerializers
{
    [TestFixture(typeof(TestCompoundObject__Implementation__))]
    public sealed class should_work_with_MockedCompoundObjects<T>
        : Kistl.API.AbstractConsumerTests.BinarySerializers.should_work_with_ICompoundObjects<T>
        where T : class, ICompoundObject, new()
    {
    }
}
