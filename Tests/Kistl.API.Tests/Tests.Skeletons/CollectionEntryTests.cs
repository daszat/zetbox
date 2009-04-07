using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Kistl.API.Tests.Skeletons
{

    public abstract class CollectionEntryTests<T>
        : IPersistenceObjectTests<T>
        where T : IValueCollectionEntry, new()
    {
    }

}
