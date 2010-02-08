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
    [TestFixture]
    public sealed class should_work_with_MockedCompoundObjects
        : Kistl.API.AbstractConsumerTests.BinarySerializers.should_work_with_ICompoundObjects<TestCompoundObject__Implementation__>
    {
    }
}
