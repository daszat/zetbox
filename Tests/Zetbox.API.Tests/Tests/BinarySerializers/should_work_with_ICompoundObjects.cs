using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

using Zetbox.API.Mocks;

using NUnit.Framework;

namespace Zetbox.API.Tests.BinarySerializers
{
    [TestFixture]
    public sealed class should_work_with_MockedCompoundObjects
        : Zetbox.API.AbstractConsumerTests.BinarySerializers.should_work_with_ICompoundObjects<TestCompoundObjectImpl>
    {
    }
}
