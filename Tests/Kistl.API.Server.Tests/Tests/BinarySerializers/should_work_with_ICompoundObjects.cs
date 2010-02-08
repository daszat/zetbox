using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Kistl.API.Server.Tests.BinarySerializers
{

    public interface CompoundObjectMock : ICompoundObject
    {
    }

    public class CompoundObjectMock__Implementation__ : BaseServerCompoundObject
    {
        public override InterfaceType GetInterfaceType()
        {
            return new InterfaceType(typeof(CompoundObjectMock));
        }
    }

    [TestFixture]
    public class should_work_with_BaseServerCompoundObjects
        : Kistl.API.AbstractConsumerTests.BinarySerializers.should_work_with_ICompoundObjects<CompoundObjectMock__Implementation__>
    {
    }
}
