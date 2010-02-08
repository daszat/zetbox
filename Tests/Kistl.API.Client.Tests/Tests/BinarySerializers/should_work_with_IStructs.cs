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
    public interface CompoundObjectMock : ICompoundObject
    {
    }

    public class CompoundObjectMock__Implementation__ : BaseClientCompoundObject
    {
        public override InterfaceType GetInterfaceType()
        {
            return new InterfaceType(typeof(CompoundObjectMock));
        }
    }

    [TestFixture]
    public class should_work_with_BaseClientCompoundObjects
        : Kistl.API.AbstractConsumerTests.BinarySerializers.should_work_with_ICompoundObjects<CompoundObjectMock__Implementation__>
    {
    }
}
