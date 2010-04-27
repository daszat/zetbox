using System;

using Kistl.API.Configuration;

using NUnit.Framework;

// This is a ApplicationContext to test the recommended default IDisposable implementation

namespace Kistl.API.Tests
{
    public partial class ApplicationContextTests
    {

        partial class ApplicationContextMock : ApplicationContext
        {
            public ApplicationContextMock()
                : base(HostType.Server)
            {
            }

        }
    }
}