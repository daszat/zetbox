using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.DalProvider.Frozen.Mocks;

using NUnit.Framework;

[SetUpFixture]
public class SetUp
{
    [SetUp]
    public void InitAppCtx()
    {
        var appCtx = new FrozenApplicationContextMock();
    }
}
