using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Kistl.API.Tests.Skeletons
{
    [TestFixture]
    public class IDataObjectTests<T>
        : IPersistenceObjectTests<T>
        where T : IDataObject, new()
    {

        [Test]
        [Ignore("not implemented")]
        public void NotifyChange()
        {
            obj.NotifyChange();
        }

    }
}
