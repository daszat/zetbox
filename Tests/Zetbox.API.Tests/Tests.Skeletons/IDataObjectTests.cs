using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Zetbox.API.Tests.Skeletons
{
    public class IDataObjectTests<T> 
        : IPersistenceObjectTests<T>
        where T : IDataObject, new()
    {

    }
}
