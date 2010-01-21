using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Test;

using NUnit.Framework;

namespace Kistl.IntegrationTests.BinarySerializers
{

    [TestFixture(typeof(TestPhoneCompoundObject))]
    public sealed class should_work_with_ClientCompoundObjects<T>
        : Kistl.API.AbstractConsumerTests.BinarySerializers.should_work_with_ICompoundObjects<T>
        where T : class, ICompoundObject, new()
    {
    }

}
